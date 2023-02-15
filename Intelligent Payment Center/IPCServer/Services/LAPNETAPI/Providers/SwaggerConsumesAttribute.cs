using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPNETAPI.Providers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerConsumesAttribute : Attribute
    {
        public SwaggerConsumesAttribute(params string[] contentTypes)
        {
            this.ContentTypes = contentTypes;
        }

        public IEnumerable<string> ContentTypes { get; }
    }
}