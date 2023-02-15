using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LAPNETAPI.Providers;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(LAPNETAPI.Startup))]

namespace LAPNETAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(typeof(CustomBodyMiddleware));
            //ConfigureAuth(app);
        }

    }
}
