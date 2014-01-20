using System.Collections.Generic;
using System.Management.Automation;
using TeamCitySharp;
using Illallangi.TeamCity.Config;
using TeamCitySharp.ActionTypes;

namespace Illallangi.TeamCity.PowerShell
{
    [Cmdlet(VerbsLifecycle.Start, "TeamCityBackup")]
    public sealed class StartTeamCityBackup : TeamCityCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string FileName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter IncludeBuildLogs { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter ExcludeConfigurations { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter ExcludeDatabase { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter IncludePersonalChanges { get; set; }

        [Parameter()]
        public SwitchParameter Wait { get; set; }

        protected override IEnumerable<object> Process(ITeamCityClient client)
        {
            var fileName = client.ServerInformation.TriggerServerInstanceBackup(new BackupOptions
                            {
                                Filename = this.FileName,
                                IncludeBuildLogs = this.IncludeBuildLogs,
                                IncludeConfigurations = !this.ExcludeConfigurations,
                                IncludeDatabase = !this.ExcludeDatabase,
                                IncludePersonalChanges = this.IncludePersonalChanges,
                            });
            
            var status = string.Empty;

            while (!(status = client.ServerInformation.GetBackupStatus()).Equals(TeamCityConfig.Config.IdleStatus))
            {
                this.WriteDebug(string.Format(TeamCityConfig.Config.WaitPrompt, TeamCityConfig.Config.Timeout, status));
                System.Threading.Thread.Sleep(TeamCityConfig.Config.Timeout);                
            }

            yield return new
            {
                HostName = this.HostName,
                FileName = fileName,
                IncludeBuildLogs = this.IncludeBuildLogs,
                ExcludeConfigurations = this.ExcludeConfigurations,
                ExcludeDatabase = this.ExcludeDatabase,
                IncludePersonalChanges = this.IncludePersonalChanges,
            };
        }
    }
}
