using static ScoutsPal.Web.StaticDetails;

namespace ScoutsPal.Web.Models
{
    public class ApiRequest
    {
        public APIType ApiType { get; set; } = APIType.GET;

        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
