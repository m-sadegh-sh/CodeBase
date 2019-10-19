namespace CodeBase.Off.Website.Helpers {
    using System;
    using System.Collections.Generic;

    using CodeBase.Off.Website.Models;

    public static class ModelRouteValuesProvider {
        private static readonly IDictionary<Type, Func<dynamic, object>> _modelRouteValues;

        static ModelRouteValuesProvider() {
            _modelRouteValues = new Dictionary<Type, Func<dynamic, object>> {
                    {typeof (AttributeEditModel), m => new {
                            m.Owner,
                            m.Key
                    }},
                    {typeof (CarouselEditModel), m => new {
                            m.Id
                    }},
                    {typeof (ConfigEditModel), m => new {
                            m.Key
                    }},
                    {typeof (EntryEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (FriendLinkEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (PortfolioEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (ServiceEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (SocialNetworkEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (SubscriptionEditModel), m => new {
                            m.Guid
                    }},
                    {typeof (TagEditModel), m => new {
                            m.Id
                    }},
                    {typeof (TeamMemberEditModel), m => new {
                            m.User.Id
                    }},
                    {typeof (TestimonialEditModel), m => new {
                            m.Slug
                    }},
                    {typeof (UserEditModel), m => new {
                            m.Id
                    }},
                    {typeof (EntrySummaryModel), m => new {
                            m.CreateDate.Year,
                            m.CreateDate.Month,
                            m.Slug
                    }},
                    {typeof (EntryShowModel), m => new {
                            m.CreateDate.Year,
                            m.CreateDate.Month,
                            m.Slug
                    }},
                    {typeof (FriendLinkShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (LogShowModel), m => new {
                            m.Id
                    }},
                    {typeof (PortfolioSummaryModel), m => new {
                            m.Slug
                    }},
                    {typeof (PortfolioShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (ServiceSummaryModel), m => new {
                            m.Slug
                    }},
                    {typeof (ServiceShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (SocialNetworkShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (TagShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (TeamMemberSummaryModel), m => new {
                            m.User.UserName
                    }},
                    {typeof (TeamMemberShowModel), m => new {
                            m.User.UserName
                    }},
                    {typeof (TestimonialShowModel), m => new {
                            m.Slug
                    }},
                    {typeof (UserSummaryModel), m => new {
                            m.UserName
                    }},
                    {typeof (UserShowModel), m => new {
                            m.UserName
                    }}
            };
        }

        public static object GetRouteValues<TModel>(TModel model) {
            var modelType = typeof (TModel);

            if (!_modelRouteValues.ContainsKey(modelType)) {
                throw new ArgumentException(string.Format("No route value(s) was defined for model \"{0}\".",
                                                          modelType.Name));
            }

            return _modelRouteValues[modelType](model);
        }
    }
}