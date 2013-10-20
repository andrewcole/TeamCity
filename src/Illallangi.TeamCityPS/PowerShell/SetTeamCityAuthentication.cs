using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Illallangi.TeamCityPS.Config;

namespace Illallangi.TeamCityPS.PowerShell
{
    [Cmdlet(VerbsCommon.Set, "TeamCityAuthentication")]
    public sealed class SetTeamCityAuthentication : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string HostName { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(
                    AuthCache
                        .FromFile()
                        .AddAuth(this.HostName, this.UserName, this.Password));
        }
    }
}