using Microsoft.Extensions.Configuration;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Helpers
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;
        private readonly WebApplicationBuilder _builder;

        public ConfigurationHelper()
        { }

        public ConfigurationHelper(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        public ConfigurationHelper(WebApplicationBuilder builder) : base()
        {
            _builder = builder;
        }

        public string GetConnectionString()
        {
            return _builder.Configuration.GetSection("ConnectionStrings").GetConnectionString("DefaultConnection");
        }
    }
}