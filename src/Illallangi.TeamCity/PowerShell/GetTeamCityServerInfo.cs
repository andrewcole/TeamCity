using System.Collections.Generic;
using System.Management.Automation;
using TeamCitySharp;

namespace Illallangi.TeamCity.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TeamCityServerInfo")]
    public sealed class GetTeamCityServerInfo : TeamCityCmdlet
    {
        protected override IEnumerable<object> Process(ITeamCityClient client)
        {
            var serverInfo = client.ServerInformation.ServerInfo();

            yield return new
            {
                HostName = this.HostName,
                BuildNumber = serverInfo.BuildNumber,
                CurrentTime = serverInfo.CurrentTime,
                StartTime = serverInfo.StartTime,
                Version = serverInfo.Version,
                VersonMajor = serverInfo.VersonMajor,
            };
        }
    }
}
