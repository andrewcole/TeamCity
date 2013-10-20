using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using TeamCitySharp;
using Illallangi.TeamCityPS.Config;

namespace Illallangi.TeamCityPS.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TeamCityBackupStatus")]
    public sealed class GetTeamCityBackupStatus : TeamCityPSCmdlet
    {
        protected override IEnumerable<object> Process(ITeamCityClient client)
        {
            var backupStatus = client.ServerInformation.GetBackupStatus();

            yield return new
            {
                HostName = this.HostName,
                BackupStatus = backupStatus,
            };
        }
    }
}
