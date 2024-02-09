using Microsoft.Xrm.Sdk;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    public partial class MyPluginControl
    {
        public class BusinessUnit
        {
            public EntityReference BU { get; set; }
            public EntityReference ParentBU { get; set; }
            public override string ToString()
            {
                return $"{this.ParentBU?.Name} -> [{this.BU.Name}]";
            }
        }

    }
        }
 