namespace Fina.Common
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;
        public const int DefaultPageNumber = 1;
        public const int DetaultPageSize = 25;

        public static string BackendUrl { get; set; } = "http://localhost:5165";
        public static string FrontendUrl { get; set; } = "http://localhost:5100";
    }
}
