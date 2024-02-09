using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        public string LastUsedOrganizationWebappUrl { get; set; }
        public bool OpenLinksInClassicInterface { get; set; }
        public bool ChangeBusinessUnit { get; set; }

        public bool CloneSecurityRoles { get; set; }
        public bool ClearTargetUserRolesListBeforeClone { get; set; }

        public bool CloneTeams { get; set; }
        public bool ClearTargetTeamsListBeforeClone { get; set; }

        public void ResetToDefaults()
        {
            this.OpenLinksInClassicInterface = true;
            this.ChangeBusinessUnit = true;
            this.CloneSecurityRoles = true;
            this.ClearTargetUserRolesListBeforeClone = true;
            this.CloneTeams = true;
            this.ClearTargetTeamsListBeforeClone = true;

        }
    }
}