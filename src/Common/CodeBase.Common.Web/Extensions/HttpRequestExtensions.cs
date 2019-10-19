namespace CodeBase.Common.Web.Extensions {
    using System.Collections.Generic;
    using System.Net;
    using System.Web;

    using CodeBase.Common.Infrastructure.Application;

    public static class HttpRequestExtensions {
        public static HttpRequestBase AsBase(this HttpRequest request) {
            return new HttpRequestWrapper(request);
        }

        public static string RemoteIp(this HttpRequest request) {
            var ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ip == null)
                ip = request.ServerVariables["REMOTE_ADDR"];

            return ip;
        }

        public static string LocalIp(this HttpRequest request) {
            var hostName = Dns.GetHostName();

            var hostEntry = Dns.GetHostEntry(hostName);

            var addressList = hostEntry.AddressList;

            var ip = addressList[addressList.Length - 1].ToString();

            return ip;
        }

        public static IDictionary<string, string> LoggableParams(this HttpRequest request) {
            var @params = new Dictionary<string, string>();

            foreach (string param in request.Headers) {
                if (request.Params[param].AsNullIfWhiteSpace() != null)
                    @params.TryToAdd(param, request.Params[param]);
            }

            foreach (string param in request.Cookies) {
                if (request.Params[param].AsNullIfWhiteSpace() != null)
                    @params.TryToAdd(param, request.Params[param]);
            }

            foreach (string param in request.QueryString) {
                if (request.Params[param].AsNullIfWhiteSpace() != null)
                    @params.TryToAdd(param, request.Params[param]);
            }

            foreach (string param in request.Form) {
                if (request.Params[param].AsNullIfWhiteSpace() != null)
                    @params.TryToAdd(param, request.Params[param]);
            }

            foreach (string param in request.Files) {
                if (request.Params[param].AsNullIfWhiteSpace() != null)
                    @params.TryToAdd(param, request.Params[param]);
            }

            return @params;
        }
    }
}