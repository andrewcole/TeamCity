using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using TeamCitySharp;
using Illallangi.TeamCityPS.Config;

namespace Illallangi.TeamCityPS.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TeamCityServerInfo")]
    public sealed class GetTeamCityServerInfo : TeamCityPSCmdlet
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
