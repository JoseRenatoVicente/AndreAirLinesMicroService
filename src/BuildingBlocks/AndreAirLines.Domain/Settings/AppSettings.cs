namespace AndreAirLines.Domain.Settings
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
