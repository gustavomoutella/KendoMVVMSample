using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.Shared.Infra.Data
{
    public sealed class AppSettings
    {
        public sealed class Site
        {
            public static string UrlSite { get; set; }

            public static string UrlApi { get; set; }
        }

        public sealed class ConnectionStrings
        {
            public static string DefaultConnection { get; set; }
        }
    }
}
