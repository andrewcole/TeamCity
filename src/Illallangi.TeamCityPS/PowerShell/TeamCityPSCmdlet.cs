using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Illallangi.TeamCityPS.Config;
using TeamCitySharp;

namespace Illallangi.TeamCityPS.PowerShell
{
    [Cmdlet("Get", "TeamCityAbstractClass")]
    public abstract class TeamCityPSCmdlet : PSCmdlet
    {
        [Alias("Server")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string HostName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(this.UserName) || string.IsNullOrWhiteSpace(this.Password))
            {
                try
                {
                    var auth = AuthCache.FromFile().Single(a => a.HostName.Equals(this.HostName));
                    this.UserName = auth.UserName;
                    this.Password = auth.Password;
                }
                catch (Exception failure)
                {
                    this.WriteError(new ErrorRecord(
                        failure,
                        failure.Message,
                        ErrorCategory.InvalidResult,
                        TeamCityConfig.Config));
                    return;
                }
            }
            try
            {
                var client = new TeamCityClient(this.HostName);
                client.Connect(this.UserName, this.Password);

                this.WriteObject(this.Process(client), true);
            }
            catch (Exception failure)
            {
                this.WriteError(new ErrorRecord(
                    failure,
                    failure.Message,
                    ErrorCategory.InvalidResult,
                    TeamCityConfig.Config));
                return;
            }
        }

        protected abstract IEnumerable<Object> Process(ITeamCityClient client);
    }
}