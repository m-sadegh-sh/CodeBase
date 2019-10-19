namespace CodeBase.Common.Web.Optimization.Compression {
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Utilities.Web;

    public class CompressorModule : IHttpModule {
        private const string GZIP = "gzip";
        private const string DEFLATE = "deflate";

        public void Init(HttpApplication context) {
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
        }

        private static void context_PreRequestHandlerExecute(object sender,
                                                             EventArgs e) {
            var application = (HttpApplication) sender;

            if (application.Context.CurrentHandler is MvcHandler) {
                if (HTML.IsEncodingAccepted(DEFLATE)) {
                    application.Response.Filter = new CompressorStream(application.Response.Filter,
                                                                       DEFLATE);
                    HTML.SetEncoding(DEFLATE);
                } else if (HTML.IsEncodingAccepted(GZIP)) {
                    application.Response.Filter = new CompressorStream(application.Response.Filter,
                                                                       GZIP);
                    HTML.SetEncoding(GZIP);
                }
            }
        }

        public void Dispose() {}
    }
}