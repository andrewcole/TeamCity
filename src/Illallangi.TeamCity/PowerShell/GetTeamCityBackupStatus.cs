using System.Collections.Generic;
using System.Management.Automation;
using TeamCitySharp;

namespace Illallangi.TeamCity.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "TeamCityBackupStatus")]
    public sealed class GetTeamCityBackupStatus : TeamCityCmdlet
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
