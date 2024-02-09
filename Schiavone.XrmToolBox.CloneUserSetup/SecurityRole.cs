using System;
using Microsoft.Xrm.Sdk;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    public class SecurityRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BusinessUnitId { get; set; }
        public string BusinessUnitIdName { get; set; }

        public Guid ParentRootRoleId { get; set; }
        public string ParentRootRoleName { get; set; }
        public Guid ParentRootRoleBusinessUnitId { get; set; }
        public string ParentRootRoleBusinessUnitIdName { get; set; }

        public EntityReference ToEntityReference()
        {
            return Extensions.GetNamedEntityReference("role", Id, Name);
        }
        public override string ToString()
        {
            return $"{this.Name} [{this.BusinessUnitIdName}] - {this.ParentRootRoleName} [{this.ParentRootRoleBusinessUnitIdName}]";
        }


    }
}
 