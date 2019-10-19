namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;

    using CodeBase.Common.Web.Mvc;
    using CodeBase.Common.Web.Routing;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using DotNetOpenAuth.OpenId.RelyingParty;

    public sealed partial class AuthenticationController : ControllerBase
    {
        [HttpGet]
        [Then]
        public virtual ActionResult Login()
        {
            var model = new OpenIdLoginModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Then]
        public virtual ActionResult OpenId(OpenIdLoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Identifier id;
            if (Identifier.TryParse(model.OpenIdIdentifier, out id)) {
                try {
                    var openId = new OpenIdRelyingParty();

                    var returnUrl = new Uri(Url.Action("OpenIdCallback", "Authentication",
                                                       RouteParameter.Add(ThenAttribute.ParameterName,
                                                                          ViewBag.Then[ThenAttribute.ParameterName]),
                                                       Request.Url.Scheme), UriKind.Absolute);

                    var request = openId.CreateRequest(id, Realm.AutoDetect, returnUrl);

                    request.AddExtension(new ClaimsRequest {
                        Email = DemandLevel.Require,
                        FullName = DemandLevel.Require,
                        Nickname = DemandLevel.Require
                    });

                    var fetchRequest = new FetchRequest();
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    request.AddExtension(fetchRequest);

                    return request.RedirectingResponse.AsActionResult();
                } catch (ProtocolException) {
                    ErrorNotification(Resources.Messages_OpenIdConnectionFailure, false);

                    return View("Login", model);
                }
            }

            ErrorNotification(Resources.Messages_InvalidOpenIdIdentifier, false);

            return View("Login", model);
        }

        [HttpGet]
        [ValidateInput(false)]
        [Then]
        public virtual ActionResult OpenIdCallback()
        {
            var model = new OpenIdLoginModel();

            var openId = new OpenIdRelyingParty();
            var authenticationResponse = openId.GetResponse();

            if (authenticationResponse.Status == AuthenticationStatus.Authenticated) {
                var friendlyName = GetFriendlyName(authenticationResponse);

                SetAuthCookie(authenticationResponse.ClaimedIdentifier, true, friendlyName);

                SuccessNotification(Resources.Messages_OpenIdLoginSuccess);

                return RedirectToThen();
            }

            ErrorNotification(Resources.Messages_OpenIdLoginFailure, false);

            return View("Login", model);
        }

        private void SetAuthCookie(string username, bool createPersistentCookie, string userData) {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            var authenticationConfig =
                (AuthenticationSection) WebConfigurationManager.GetWebApplicationSection("system.web/authentication");

            var timeout = (int) authenticationConfig.Forms.Timeout.TotalMinutes;
            var expiry = DateTime.Now.AddMinutes(timeout);

            var ticket = new FormsAuthenticationTicket(2,
                                                       username,
                                                       DateTime.Now,
                                                       expiry,
                                                       createPersistentCookie,
                                                       userData,
                                                       FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName) {
                Value = encryptedTicket,
                HttpOnly = true,
                Secure = authenticationConfig.Forms.RequireSSL
            };

            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }

        private static string GetFriendlyName(IAuthenticationResponse response) {
            var claimsResponse = response.GetExtension<ClaimsResponse>();

            if (claimsResponse != null) {
                if (!string.IsNullOrEmpty(claimsResponse.FullName))
                    return claimsResponse.FullName;

                if (!string.IsNullOrEmpty(claimsResponse.Nickname))
                    return claimsResponse.Email;

                if (!string.IsNullOrEmpty(claimsResponse.Email))
                    return claimsResponse.Email;
            }

            var fetchResponse = response.GetExtension<FetchResponse>();

            if (fetchResponse != null) {
                var fullName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.FullName);

                if (!string.IsNullOrEmpty(fullName))
                    return fullName;

                var firstName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.First);
                var lastName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.Last);

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    return fullName + " " + lastName;

                var email = fetchResponse.GetAttributeValue(WellKnownAttributes.Contact.Email);

                if (!string.IsNullOrEmpty(email))
                    return email;
            }

            return response.FriendlyIdentifierForDisplay;
        }

        [HttpGet]
        [Auth]
        [Then]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            SuccessNotification(Resources.Messages_OpenIdLogoutSuccess);

            return RedirectToThen();
        }
    }
}