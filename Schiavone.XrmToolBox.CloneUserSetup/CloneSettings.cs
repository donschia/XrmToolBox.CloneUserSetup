using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Schiavone.XrmToolBox.CloneUserSetup.MyPluginControl;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    public class CloneSettings
    {
        public bool OpenLinksInClassicInterface { get; set; }
        public bool ChangeBusinessUnit { get; set; }

        public bool CloneSecurityRoles { get; set; }
        public bool ClearTargetUserRolesListBeforeClone { get; set; }

        public bool CloneTeams { get; set; }
        public bool ClearTargetTeamsListBeforeClone { get; set; }

        public bool IsThereWorkToBeDone
        {
            get { return (this.CloneSecurityRoles || this.CloneTeams); }
        }
        public string GetWorkStepsReport(string delimiter = "\r\n")
        {
            this.WorkSteps.Clear();
            if (this.CloneSecurityRoles)
            {
                this.WorkSteps.Add("Clone Security Roles");
                if (this.ChangeBusinessUnit)
                {
                    this.WorkSteps.Add(" -> Change Business Unit");
                }
                else
                {
                    this.WorkSteps.Add(" -> * Preserve Target Business Unit");
                }

                if (this.ClearTargetUserRolesListBeforeClone)
                {
                    this.WorkSteps.Add(" -> Clear Target User Roles List Before Clone");
                }
                else
                {
                    this.WorkSteps.Add(" -> * Preserve Target User Roles");
                }
            }
            else
            {
                this.WorkSteps.Add("* DO NOT Clone Security Roles");
            }

            if (this.CloneTeams)
            {
                this.WorkSteps.Add("Clone Teams");
                if (this.ClearTargetTeamsListBeforeClone)
                {
                    this.WorkSteps.Add(" -> Clear Target Teams List Before Clone");
                }
                else
                {
                    this.WorkSteps.Add(" -> * Preserve Target Teams");
                }
            }
            else
            {
                this.WorkSteps.Add("* DO NOT Clone Teams");
            }
            //if (this.WorkSteps.Count == 0)
            //{
            //    this.WorkSteps.Add("No Work Selected.");
            //}
            return String.Join(delimiter, this.WorkSteps.ToArray());
        }
        public List<string> WorkSteps { get; set; } = new List<string>();

        public List<SecurityRole> SecurityRoleMap { get; set; }
        public List<BusinessUnit> AllBusinessUnits { get; set; }
    }
}
