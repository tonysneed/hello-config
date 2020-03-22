namespace hello_config
{
    public interface IMySettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
    }

    public class MySettings : IMySettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
    }

    public interface IMyOtherSettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
    }

    public class MyOtherSettings : IMyOtherSettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
    }
}