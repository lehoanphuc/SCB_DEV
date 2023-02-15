using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using InterbankTransferService.Providers;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(InterbankTransferService.Startup))]

namespace InterbankTransferService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(CustomBodyMiddleware));
            ConfigureAuth(app);
        }

    }
}
