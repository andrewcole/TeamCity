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

        [ConfigurationProperty("WaitPrompt", DefaultValue = "Backup status {1}; waiting {0}ms.")]
        public string WaitPrompt
        {
            get { return (string)this["WaitPrompt"]; }
            set { this["WaitPrompt"] = value; }
        }

        [ConfigurationProperty("Timeout", DefaultValue = "1000")]
        public int Timeout
        {
            get { return (int)this["Timeout"]; }
            set { this["Timeout"] = value.ToString(); }
        }

        [ConfigurationProperty("IdleStatus", DefaultValue = "Idle")]
        public string IdleStatus
        {
            get { return (string)this["IdleStatus"]; }
            set { this["IdleStatus"] = value; }
        }
    }
}