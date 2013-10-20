using System.Configuration;

namespace Illallangi.TeamCityPS.Config
{
    public sealed class TeamCityConfig : ConfigurationSection
    {
        private static string staticPath;
        private static Configuration staticExeConfig;
        private static TeamCityConfig staticConfig;

        public static string Path
        {
            get
            {
                return TeamCityConfig.staticPath ??
                    (TeamCityConfig.staticPath = System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public static Configuration ExeConfig
        {
            get
            {
                return TeamCityConfig.staticExeConfig ??
                    (TeamCityConfig.staticExeConfig = ConfigurationManager.OpenExeConfiguration(TeamCityConfig.Path));
            }
        }

        public static TeamCityConfig Config
        {
            get
            {
                return TeamCityConfig.staticConfig ??
                    (TeamCityConfig.staticConfig = (TeamCityConfig)TeamCityConfig.ExeConfig.GetSection("TeamCityConfig"));
            }
        }

        [ConfigurationProperty("AuthCache", DefaultValue = "%localappdata%\\Illallangi Enterprises\\TeamCityPS\\AuthTokens.json")]
        public string AuthCache
        {
            get { return (string)this["AuthCache"]; }
            set { this["AuthCache"] = value; }
        }

        [ConfigurationProperty("WaitPrompt", DefaultValue = "Press any key to continue...")]
        public string WaitPrompt
        {
            get { return (string)this["WaitPrompt"]; }
            set { this["WaitPrompt"] = value; }
        }
    }
}