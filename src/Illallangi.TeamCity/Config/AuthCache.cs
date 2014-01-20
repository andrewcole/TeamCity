using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Illallangi.TeamCity.Config
{
    public sealed class AuthCache : List<Auth>
    {
        public IEnumerable<Auth> AddAuth(string hostName, string userName, string password)
        {
            if (1 == this.Count(auth => auth.HostName.Equals(hostName)))
            {
                this.Single(auth => auth.HostName.Equals(hostName)).UserName = userName;
                this.Single(auth => auth.HostName.Equals(hostName)).Password = password;
            }
            else
            {
                this.Add(new Auth { HostName = hostName, UserName = userName, Password = password });
            }
            
            yield return this.ToFile().Single(auth => auth.HostName.Equals(hostName));
        }

        public AuthCache ToFile()
        {
            var fileName = Environment.ExpandEnvironmentVariables(TeamCityConfig.Config.AuthCache);
            
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
            
            return this;
        }

        public static AuthCache FromFile()
        {
            var fileName = Environment.ExpandEnvironmentVariables(TeamCityConfig.Config.AuthCache);

            return File.Exists(fileName) ?
                JsonConvert.DeserializeObject<AuthCache>(File.ReadAllText(fileName)) ?? new AuthCache().ToFile() :
                new AuthCache().ToFile();
        }
    }
}
