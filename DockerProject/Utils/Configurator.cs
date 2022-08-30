using Microsoft.Extensions.Configuration;

namespace DockerProject.Utils
{
    public static class Configurator
    {
        public static readonly IConfiguration Config;
        public static readonly IConfiguration TestData;
        private const string SettingJson = "Resources/settings.json";
        private const string TestDataJson = "Resources/testDataSettings.json";

        static Configurator()
        {
            Config = new ConfigurationBuilder().AddJsonFile(SettingJson, true, true).Build();
            TestData = new ConfigurationBuilder().AddJsonFile(TestDataJson, true, true).Build();
        }
    }
}