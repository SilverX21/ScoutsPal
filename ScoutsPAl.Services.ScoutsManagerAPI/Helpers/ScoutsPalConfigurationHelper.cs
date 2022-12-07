using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Helpers
{
    public class ScoutsPalConfigurationHelper
    {
        public static void AddSwaggerDocumentation(SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }
    }
}