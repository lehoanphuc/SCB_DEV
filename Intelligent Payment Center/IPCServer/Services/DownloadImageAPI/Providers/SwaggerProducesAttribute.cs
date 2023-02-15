using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadImageAPI.Providers
{
    public class SwaggerProducesAttribute : Attribute
    {
        public SwaggerProducesAttribute(params string[] contentTypes)
        {
            this.ContentTypes = contentTypes;
        }

        public IEnumerable<string> ContentTypes { get; }
    }
}