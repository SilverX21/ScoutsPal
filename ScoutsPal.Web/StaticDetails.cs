namespace ScoutsPal.Web
{
    public class StaticDetails
    {
        public static string ScoutManagementAPIBase { get; set; }

        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
