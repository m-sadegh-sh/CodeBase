namespace CodeBase.Off.Website.Configurators {
    using System.Net;
    using System.Web;
    using System.Web.Routing;

    using CodeBase.Common.Web.Routing;

    public static class RouteRegisterar {
        public static void RegisterRoutes() {
            var routes = RouteTable.Routes;

            routes.RouteExistingFiles = false;

            routes.MapRouteLowerCase("Admin", "Admin", new {
                Controller = "Admin",
                Action = "Index"
            });

            routes.MapRouteLowerCase("AdminBackup", "Admin/Backup", new {
                Controller = "Admin",
                Action = "Backup"
            });

            routes.MapRouteLowerCase("Home", "", new {
                Controller = "Home",
                Action = "Index"
            });

            routes.MapRouteLowerCase("Services", "Services", new {
                Controller = "Services",
                Action = "Index"
            });

            routes.MapRouteLowerCase("ServicesShow", "Services/{Slug}", new {
                Controller = "Services",
                Action = "Show"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("Portfolio", "Portfolio", new {
                Controller = "Portfolio",
                Action = "Index"
            });

            routes.MapRouteLowerCase("PortfolioShow", "Portfolio/{Slug}", new {
                Controller = "Portfolio",
                Action = "Show"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("About", "About-Us", new {
                Controller = "About",
                Action = "Index"
            });

            routes.MapRouteLowerCase("AboutMember", "About-Us/{Slug}", new {
                Controller = "About",
                Action = "Member"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("AboutMemberResume", "About-Us/{Slug}/Download-Resume", new {
                Controller = "About",
                Action = "DownloadResume"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("Contact", "Contact-Us", new {
                Controller = "Contact",
                Action = "Index"
            });

            routes.MapRouteLowerCase("Widget-SocialNetworks", "Follow-Us-On/{Slug}", new {
                Controller = "Widgets",
                Action = "RedirectToSocialNetwork"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("Widget-Testimonial", "Testimonials/{Slug}", new {
                Controller = "Widgets",
                Action = "RedirectToTestimonial"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("Widget-FriendLink", "Go-To-Friend/{Slug}", new {
                Controller = "Widgets",
                Action = "RedirectToFriendLink"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("Blog", "Blog", new {
                Controller = "Blog",
                Action = "Index"
            });

            routes.MapRouteLowerCase("BlogEntriesByAuthor", "Blog/Authors/{Slug}", new {
                Controller = "Blog",
                Action = "ByAuthor"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("BlogEntriesArchive", "Blog/Archive/{Year}/{Month}/{Day}", new {
                Controller = "Blog",
                Action = "ByAuthor"
            }, new {
                Year = new NumericConstraint(true),
                Month = new NumericConstraint(true),
                Day = new NumericConstraint(true)
            });

            routes.MapRouteLowerCase("BlogEntry", "Blog/{Slug}", new {
                Controller = "Entry",
                Action = "Show"
            }, new {
                Slug = new SlugConstraint()
            });

            routes.MapRouteLowerCase("BlogFeed", "Blog/Feed", new {
                Controller = "Feed",
                Action = "Index"
            });

            routes.MapRouteLowerCase("BlogSearch", "Blog/Search", new {
                Controller = "Search",
                Action = "Index"
            });

            routes.MapRouteLowerCase("Error", "{CatchAll*}", new {
                Controller = "Error",
                Action = "Index",
                HttpExeption = new HttpException((int) HttpStatusCode.NotFound, "Route not found.")
            });

            routes.MapRouteLowerCase("_INTERNAL", "_INTERNAL_/", new {});
        }
    }
}