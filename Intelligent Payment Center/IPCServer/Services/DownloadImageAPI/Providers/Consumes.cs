using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace DownloadImageAPI.Providers
{
    public class Consumes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attribute = apiDescription.GetControllerAndActionAttributes<SwaggerConsumesAttribute>().SingleOrDefault();
            if (attribute == null)
            {
                return;
            }
            operation.consumes.Clear();
            operation.consumes = attribute.ContentTypes.ToList();
        }
    }
}