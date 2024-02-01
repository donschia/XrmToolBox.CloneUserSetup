using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using System.Web.UI.WebControls;
using Microsoft.Crm.Sdk.Messages;
using System.Diagnostics;
using System.Windows.Navigation;
using static System.Windows.Forms.LinkLabel;
using System.Collections;
using System.Web.Services.Description;
using NuGet;
using System.Xml.Linq;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;

        private IContainer components;

        private ToolStrip toolStripMenu;

        private ToolStripButton tsbClose;

        private ToolStripSeparator tssSeparator1;

        private class CloneSettings
        {
            public bool OpenLinksInClassicInterface { get; set; }
            public bool ChangeBusinessUnit { get; set; }

            public bool CloneSecurityRoles { get; set; }
            public bool ClearTargetUserRolesListBeforeClone { get; set; }

            public bool CloneTeams { get; set; }
            public bool ClearTargetTeamsListBeforeClone { get; set; }

            public bool IsThereWorkToBeDone
            {
                get { return (this.CloneSecurityRoles || this.CloneTeams);  }
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

        public class BusinessUnit
        {
            public EntityReference BU { get; set; }
            public EntityReference ParentBU { get; set; }
            public override string ToString()
            {
                return $"{this.ParentBU?.Name} -> [{this.BU.Name}]";
            }
        }

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
        public MyPluginControl()
        {
            this.InitializeComponent();
        }

        /*
        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }
        */
        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            if (SettingsManager.Instance.TryLoad<Settings>(base.GetType(), out this.mySettings, null))
            {
                base.LogInfo("Settings found and loaded", Array.Empty<object>());
            }
            else
            {
                this.mySettings = new Settings();
                base.LogWarning("Settings not found => a new settings file has been created!", Array.Empty<object>());
            }

            UpdateInterface();

            base.ExecuteMethod(new Action(this.GetInitialData));
        }

        //private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        //{
        //    SettingsManager.Instance.Save(base.GetType(), this.mySettings, null);
        //}
        //private void tsbClose_Click(object sender, EventArgs e)
        //{
        //    CloseTool();
        //}

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        private CloneSettings GetCloneSettingsFromUI(bool deferLoadingSecurityRoleMap = false)
        {
            return new CloneSettings()
            {
                ChangeBusinessUnit = chkChangeBusinessUnit.Checked,
                ClearTargetTeamsListBeforeClone = chkClearTargetTeamListBeforeClone.Checked,
                ClearTargetUserRolesListBeforeClone = chkClearTargetUserRoleListBeforeClone.Checked,
                CloneSecurityRoles = chkCloneSecurityRoles.Checked,
                CloneTeams = chkCloneTeams.Checked,
                OpenLinksInClassicInterface = chkOpenLinksInClassicInterface.Checked,
                SecurityRoleMap = deferLoadingSecurityRoleMap ? null : GetSecurityRoleLookup(),
            };
        }


        public static void AddMembersToTeam(Guid teamId, Guid[] membersId, IOrganizationService service)
        {
            AddMembersTeamRequest addMembersTeamRequest = new AddMembersTeamRequest();
            addMembersTeamRequest.TeamId = teamId;
            addMembersTeamRequest.MemberIds = membersId;
            service.Execute(addMembersTeamRequest);
        }
        public List<SecurityRole> GetUserRoles(Guid userId) //, out EntityReference erBusinessUnit)
        {
            var securityRoles = new List<SecurityRole>();

            var fetchXml = $@"<?xml version='1.0' encoding='utf-16'?>
<fetch>
  <entity name='role'>
    <attribute name='roleid' />
    <attribute name='roleidunique' />
    <attribute name='name' />
    <attribute name='businessunitid' />
    <attribute name='parentrootroleid' />
    <attribute name='parentroleid' />
    <link-entity name='role' to='parentrootroleid' from='roleid' alias='prr' link-type='inner'>
      <attribute name='businessunitid' alias='ParentRootRoleBU' />
    </link-entity>
    <link-entity name='role' to='parentroleid' from='roleid' alias='pr' link-type='outer'>
      <attribute name='businessunitid' alias='ParentRoleBU' />
    </link-entity>
    <order attribute='name' />
    <order attribute='businessunitid' />
    <link-entity name='systemuserroles' from='roleid' to='roleid' intersect='true'>
      <filter>
        <condition attribute='systemuserid' operator='eq' value='{userId}' />
      </filter>
    </link-entity>
  </entity>
</fetch>";

            var result = Extensions.GetAllRecordsFetchXML(base.Service, fetchXml);

            foreach (var entity in result.Entities)
            {
                securityRoles.Add(new SecurityRole()
                {
                    Id = entity.Id,
                    Name = entity.GetAttributeValue<string>("name"),
                    BusinessUnitId = entity.GetAttributeValue<EntityReference>("businessunitid").Id,
                    BusinessUnitIdName = entity.GetAttributeValue<EntityReference>("businessunitid").Name,
                    ParentRootRoleId = entity.GetAttributeValue<EntityReference>("parentrootroleid").Id,
                    ParentRootRoleName = entity.GetAttributeValue<EntityReference>("parentrootroleid").Name,
                    ParentRootRoleBusinessUnitId = entity.GetAliasedValue<EntityReference>("businessunitid").Id,
                    ParentRootRoleBusinessUnitIdName = entity.GetAliasedValue<EntityReference>("businessunitid").Name,
                });
            }
            return securityRoles;
        }

        /*

        private void GetUserRoles(Guid userid, out List<EntityReference> roleIds, out EntityReference erBusinessUnit)
        {
            QueryExpression queryExpression = new QueryExpression("systemuserroles");
            queryExpression.ColumnSet.AddColumns(new string[] { "systemuserid" });
            queryExpression.Criteria.AddCondition("systemuserid", 0, new object[] { userid });
            queryExpression.AddLink("systemuser", "systemuserid", "systemuserid", 0).Columns.AddColumns(new string[] { "fullname", "internalemailaddress", "businessunitid" });
            queryExpression.AddLink("role", "roleid", "roleid", 0).Columns.AddColumns(new string[] { "roleid", "name", "parentrootroleid"});
            EntityCollection userRoles = base.Service.RetrieveMultiple(queryExpression);
            roleIds = new List<EntityReference>();
            erBusinessUnit = new EntityReference(); //new Guid();

            // Getting Business Unit from the Security Roles
            // This wont work if the user doesn't have any security roles (ie. was just changed to a new Busiess Unit
            foreach (Entity entity in userRoles.Entities)
            {
                EntityReference value = (EntityReference)((AliasedValue)entity.Attributes["systemuser1.businessunitid"]).Value;
                erBusinessUnit = value;
                //roleIds.Add(new Guid(((AliasedValue)entity.Attributes["role2.roleid"]).Value.ToString()));
                var er = new EntityReference("role", new Guid(((AliasedValue)entity.Attributes["role2.roleid"]).Value.ToString()));
                er.Name = ((AliasedValue)entity.Attributes["role2.name"]).Value.ToString();
                roleIds.Add(er);
            }
        }
        */

        private List<SecurityRole> GetSecurityRoleLookup()
        {
            var securityRoleMap = new List<SecurityRole>();
            var fetchXml = $@"<?xml version='1.0' encoding='utf-16'?>
<fetch>
    <entity name='role'>
        <attribute name='roleid' />
        <attribute name='roleidunique' />
        <attribute name='name' />
        <attribute name='businessunitid' />
        <attribute name='businessunitidname' />
        <attribute name='parentrootroleid' />
        <attribute name='parentroleid' />
        <link-entity name='role' to='parentrootroleid' from='roleid' alias='prr' link-type='inner'>
            <attribute name='businessunitid'  />
        </link-entity>
        <order attribute='name' />
        <order attribute='businessunitid' />
    </entity>
</fetch>";
            var result = Extensions.GetAllRecordsFetchXML(base.Service, fetchXml);
            foreach (var entity in result.Entities)
            {
                securityRoleMap.Add(new SecurityRole()
                {
                    Id = entity.Id,
                    Name = entity.GetAttributeValue<string>("name"),
                    BusinessUnitId = entity.GetAttributeValue<EntityReference>("businessunitid").Id,
                    BusinessUnitIdName = entity.GetAttributeValue<EntityReference>("businessunitid").Name,
                    ParentRootRoleId = entity.GetAttributeValue<EntityReference>("parentrootroleid").Id,
                    ParentRootRoleName = entity.GetAttributeValue<EntityReference>("parentrootroleid").Name,
                    ParentRootRoleBusinessUnitId = entity.GetAliasedValue<EntityReference>("businessunitid").Id,
                    ParentRootRoleBusinessUnitIdName = entity.GetAliasedValue<EntityReference>("businessunitid").Name,
                });
            }
            return securityRoleMap;
        }

        private List<BusinessUnit> GetAllBusinessUnits()
        {
            var allBusinessUnits = new List<BusinessUnit>();
            var fetchXml = $@"<?xml version='1.0' encoding='utf-16'?>
<fetch>
  <entity name='businessunit'>
    <attribute name='businessunitid' />
    <attribute name='name' />
    <attribute name='isdisabled' />
    <attribute name='parentbusinessunitid' />
    <filter>
      <condition attribute='isdisabled' operator='eq' value='0' />
    </filter>
    <order attribute='parentbusinessunitid' />
    <order attribute='name' />
  </entity>
</fetch>";
            var result = Extensions.GetAllRecordsFetchXML(base.Service, fetchXml);
            foreach (var entity in result.Entities)
            {
                var parentBusinessUnit = entity.GetAttributeValue<EntityReference>("parentbusinessunitid");

                if (parentBusinessUnit == null)
                {
                    parentBusinessUnit = Extensions.GetNamedEntityReference("businessunit",
                        Guid.Empty,
                        "[ROOT]");
                }
                allBusinessUnits.Add(new BusinessUnit()
                {
                    BU = Extensions.GetNamedEntityReference("businessunit",
                        entity.Id,
                        $"{entity.GetAttributeValue<string>("name")}"),
                    ParentBU = entity.GetAttributeValue<EntityReference>("parentbusinessunitid") ?? parentBusinessUnit
                });
                /*
                 *   BU = Extensions.GetNamedEntityReference("businessunit",
                        entity.Id,
                        $"{entity.GetAttributeValue<string>("name")}"),
                    ParentBU = Extensions.GetNamedEntityReference("parentbusinessunit",
                        entity.Id,
                        $"{entity.GetAttributeValue<string>("name")}")
                */
            }
            return allBusinessUnits;
        }


        private void GetInitialData()
        {
            GetUsers(null, null);

            var allBusinessUnitsList = GetAllBusinessUnits();
            //lbBusinessUnits.DataSource = allBusinessUnitsList
            var list = new ListItemCollection();
            foreach (var r in allBusinessUnitsList)
            {
                var item = new ListItem { Text = r.BU.Name, Value = r.BU.Id.ToString() };
                list.Add(item);
            }

            this.cboBusinessUnit.DataSource = list;
            this.cboBusinessUnit.DisplayMember = "Text";
            this.cboBusinessUnit.ValueMember = "Value";
            //this.cboBusinessUnit.Sorting = SortOrder.Ascending;
            //cboBusinessUnit.Sort();
            FillNode(allBusinessUnitsList, null);
        }

        private void FillNode(List<BusinessUnit> items, System.Windows.Forms.TreeNode node)
        {
            var parentID = node != null
                ? (string)node.Tag
                : Guid.Empty.ToString();

            var nodesCollection = node != null
                ? node.Nodes
                : tvBusinessUnitSelection.Nodes;

            foreach (var item in items.Where(i => i.ParentBU?.Id.ToString() == parentID && i.ParentBU != null))
            {
                var newNode = nodesCollection.Add(item.BU.Name, item.BU.Name);
                newNode.Tag = item.BU.Id.ToString();

                FillNode(items, newNode);
            }
        }


        private void GetUsers(string sourceSelectedUserId = null, string targetSelectedUserId = null, bool includeInactiveUsers = false)
        {
            WorkAsyncInfo workAsyncInfo = new WorkAsyncInfo();
            workAsyncInfo.Work = ((BackgroundWorker worker, DoWorkEventArgs args) => {
               
                ConditionExpression conditionExpression = new ConditionExpression();
                conditionExpression.AttributeName = "isdisabled";
                conditionExpression.Operator = 0;
                conditionExpression.Values.Add("false");

                FilterExpression filterExpression = new FilterExpression();
                filterExpression.Conditions.Add(conditionExpression);

                QueryExpression queryExpression = new QueryExpression("systemuser");
                queryExpression.ColumnSet.AddColumns(new string[] { "systemuserid", "fullname", "businessunitid", "isdisabled" });

                // Add link-entity for businessunit name
                var QEsystemuser_businessunit = queryExpression.AddLink("businessunit", "businessunitid", "businessunitid");
                QEsystemuser_businessunit.Columns.AddColumns("name");
                QEsystemuser_businessunit.EntityAlias = "bu";

                if (!includeInactiveUsers)
                {
                    queryExpression.Criteria.AddFilter(filterExpression);
                }

                // Sort these by the name, for goodness sake
                queryExpression.AddOrder("fullname", OrderType.Ascending);
                args.Result = base.Service.RetrieveMultiple(queryExpression);
            });
            workAsyncInfo.PostWorkCallBack = ((RunWorkerCompletedEventArgs args) => {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                EntityCollection result = args.Result as EntityCollection;
                if (result != null)
                {
                    ListItemCollection sourceListItems = new ListItemCollection();
                    ListItemCollection targetListItems = new ListItemCollection();
                    
                    //Save the Users Business Unit Id too!
                    for (int i = 0; i < result.Entities.Count; i++)
                    {
                        bool isDisabled = result.Entities[i].GetAttributeValue<bool>("isdisabled");
                        var li = new ListItem($"{result.Entities[i].Attributes["fullname"]}{(isDisabled?"*":"")} [{((AliasedValue)result.Entities[i].Attributes["bu.name"]).Value}]",
                            result.Entities[i].Attributes["systemuserid"].ToString());
                        li.Attributes["UserFullName"] = result.Entities[i].Attributes["fullname"].ToString();
                        li.Attributes["BusinessUnitName"] = ((AliasedValue)result.Entities[i].Attributes["bu.name"]).Value.ToString();
                        li.Attributes["BusinessUnitId"] = ((EntityReference)result.Entities[i].Attributes["businessunitid"]).Id.ToString();
                        sourceListItems.Add(li);
                        // Do not allow disabled users in the Target User list
                        if (!isDisabled) targetListItems.Add(li);
                    }

                    // this.Source.SuspendLayout();
                    this.Source.DataSource = sourceListItems;
                    this.Source.DisplayMember = "Text";
                    this.Source.ValueMember = "Value";

                    this.Target.DataSource = targetListItems;
                    this.Target.DisplayMember = "Text";
                    this.Target.ValueMember = "Value";

                    // set these back if requested
                    if (sourceSelectedUserId != null)
                    {
                        this.Source.SelectedValue = sourceSelectedUserId;
                        SourceUserChanged();
                    }
                    if (targetSelectedUserId != null)
                    {
                        this.Target.SelectedValue = targetSelectedUserId;
                        TargetUserChanged();
                    }

                }
            });
            base.WorkAsync(workAsyncInfo);
        }

        public List<EntityReference> GetUserTeams(Guid UserID, IOrganizationService service, bool includeDefaultTeams = false)
        {
            List<EntityReference> userTeams;
            QueryExpression queryExpression = new QueryExpression("team");
            queryExpression.ColumnSet = new ColumnSet(new string[] { "teamid", "isdefault", "name" });
            queryExpression.AddLink("teammembership", "teamid", "teamid").LinkCriteria.AddCondition(new ConditionExpression("systemuserid", 0, (object)UserID));
            try
            {
                EntityCollection entityCollection = service.RetrieveMultiple(queryExpression);
                List<EntityReference> teams = new List<EntityReference>();
                foreach (Entity entity in entityCollection.Entities)
                {
                    bool isDefault = false;
                    if (entity.Attributes["isdefault"].ToString() != "False")
                    {
                        if (includeDefaultTeams == false)
                        {
                            continue;
                        }
                        else
                        {
                            isDefault = true;
                        }
                    
                    }
                    var er = entity.ToEntityReference();
                    er.Name = $"{entity.GetAttributeValue<string>("name")}{(isDefault?" (Default Team)":"")}";
                    teams.Add(er);
                }
                userTeams = teams;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return userTeams;
        }

        public static void RemoveMembersFromTeam(Guid teamId, Guid[] memberIds, IOrganizationService service)
        {
            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest();
            removeMembersTeamRequest.TeamId = teamId;
            removeMembersTeamRequest.MemberIds = memberIds;
            service.Execute(removeMembersTeamRequest);
        }

        private void syncRoles_Click(object sender, EventArgs e)
        {
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.CloseTool();
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void OpenEntityReference(EntityReference entref, bool forceUCI = true)
        {
            if (!string.IsNullOrEmpty(entref.LogicalName) && !entref.Id.Equals(Guid.Empty))
            {
                var url = ConnectionDetail.WebApplicationUrl;
                if (string.IsNullOrEmpty(url))
                {
                    url = string.Concat(ConnectionDetail.ServerName, "/", ConnectionDetail.Organization);
                    if (!url.ToLower().StartsWith("http"))
                    {
                        url = string.Concat("http://", url);
                    }
                }
                url = string.Concat(url,
                    url.EndsWith("/") ? "" : "/",
                    "main.aspx?forceclassic=",
                    forceUCI ? "0" : "1",
                    "&etn=",
                    entref.LogicalName,
                    "&pagetype=entityrecord&id=",
                    entref.Id.ToString());
                if (!string.IsNullOrEmpty(url))
                {
                    Process.Start(url);
                }
            }
        }

        private void btnOpenSource_Click(object sender, EventArgs e)
        {
            var sourceId = this.Source.SelectedValue.ToString();
            bool forceUci = !this.chkOpenLinksInClassicInterface.Checked;
            if (sourceId != null)
            {
                OpenEntityReference(new EntityReference("systemuser", new Guid(sourceId)), forceUci);
            }
        }

        private void btnOpenTarget_Click(object sender, EventArgs e)
        {
            var targetId = this.Target.SelectedValue.ToString();
            bool forceUci = !this.chkOpenLinksInClassicInterface.Checked;
            if (targetId != null)
            {
                OpenEntityReference(new EntityReference("systemuser", new Guid(targetId)), forceUci);
            }
        }



        private void tsDoTheSync_Click(object sender, EventArgs e)
        {
            // Get the Clone Settings
            var cloneSettings = GetCloneSettingsFromUI(deferLoadingSecurityRoleMap: true);


            Guid sourceUserId = new Guid(this.Source.SelectedValue.ToString());
            EntityReference sourceBusinessUnit = Extensions.GetNamedEntityReference("businessunit", new Guid(((System.Web.UI.WebControls.ListItem)this.Source.SelectedItem).Attributes["BusinessUnitId"]), ((System.Web.UI.WebControls.ListItem)this.Source.SelectedItem).Attributes["BusinessUnitName"]);

            Guid targetUserId = new Guid(this.Target.SelectedValue.ToString());
            EntityReference targetBusinessUnit = Extensions.GetNamedEntityReference("businessunit", new Guid(((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitId"]), ((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitName"]);
            
            Exception exception1 = null;

            if (sourceUserId == targetUserId)
            {
                MessageBox.Show("Source and Target user are the same.");
                return;
            }
            else
            {
                string workStepsReport = cloneSettings.GetWorkStepsReport();
                if (!cloneSettings.IsThereWorkToBeDone)
                {
                    MessageBox.Show($"Please select work to be completed.");
                    return;
                }

                DialogResult result = MessageBox.Show($"Are you sure you want to do the following: \r\n{workStepsReport}?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    WorkAsyncInfo workAsyncInfo = new WorkAsyncInfo();
                    workAsyncInfo.Message = $"{workStepsReport}"; //  the Business Unit, Security Roles, and Teams
                    workAsyncInfo.Work = ((BackgroundWorker w, DoWorkEventArgs ar) =>
                    {
                        // Now we can do the potentially time consumming load of the Security Role Map
                        cloneSettings = GetCloneSettingsFromUI(deferLoadingSecurityRoleMap: false);

                        var sourceUserRoles = new List<SecurityRole>();
                        var targetUserRoles = new List<SecurityRole>();

                        try
                        {
                            // TODO: Perhaps there is a way to mark a user so that it is immune to this to prevent modifying Service Accounts, protected users, test account, etc.
                            // TODO: Also sync up any other fields (configurable) here too? i.e.  Business Group
                            // TODO: Logging!  I would like to collect all of the Messages and also howbout we log what we did regarding the adding and removing Roles and Teams?

                            // Get Source User Security Roles
                            sourceUserRoles = GetUserRoles(sourceUserId);
                            
                            // Get Target User Security Roles
                            targetUserRoles = GetUserRoles(targetUserId);

                            // SECURITY ROLE SYNC
                            if (cloneSettings.CloneSecurityRoles)
                            {
                                // If Business Units are different, we will change the Target Business Unit to match the Source User's Business Unit 
                                if (sourceBusinessUnit.Id != targetBusinessUnit.Id)
                                {
                                    // DIFFERENT BUSINESS UNIT FOR SOURCE AND TARGET
                                    if (cloneSettings.ChangeBusinessUnit)
                                    {
                                        // We will change Target Business Unit to Match Source Business Unit
                                        workAsyncInfo.Message = $"Changing Target Business Unit from '{targetBusinessUnit.Name}' to '{sourceBusinessUnit.Name}'";

                                        // Change Business unit, which automatically clears the Target User's Security Roles
                                        SetBusinessSystemUserRequest setBusinessSystemUserRequest = new SetBusinessSystemUserRequest();
                                        setBusinessSystemUserRequest.BusinessId = sourceBusinessUnit.Id;
                                        setBusinessSystemUserRequest.UserId = targetUserId;
                                        setBusinessSystemUserRequest.ReassignPrincipal = new EntityReference("systemuser", targetUserId);
                                        SetBusinessSystemUserResponse setBusinessSystemUserResponse = (SetBusinessSystemUserResponse)base.Service.Execute(setBusinessSystemUserRequest);

                                        // Give the Target User the Source User's security roles 
                                        EntityReferenceCollection sourceUserRolesToGiveToTargetUser = new EntityReferenceCollection();
                                        foreach (var r in sourceUserRoles)
                                        {
                                            sourceUserRolesToGiveToTargetUser.Add(r.ToEntityReference()); // new EntityReference("role", g));
                                        }
                                        base.Service.Associate("systemuser", targetUserId, new Relationship("systemuserroles_association"), sourceUserRolesToGiveToTargetUser);


                                        // If we want to preserve the target user's roles, we can add them back in
                                        if (!cloneSettings.ClearTargetUserRolesListBeforeClone)
                                        {
                                            // Get reference to source role's ParentRootRole and then we will use that to find a new role with the current Buisness Unit
                                            var targetUserRolesToRestoreAfterClone = new EntityReferenceCollection();
                                            foreach (var r in targetUserRoles)
                                            {
                                                Guid parentRootRoleId = r.ParentRootRoleId;
                                                // Security role where ParentRootId is same but the Business Unit is the Target's Business Unit
                                                // Actually needs to be the Source Business Unit since we are changing Target BU to match Source
                                                var roleInTargetBusinessUnit = cloneSettings.SecurityRoleMap.Where(m => m.ParentRootRoleId == parentRootRoleId && m.BusinessUnitId == sourceBusinessUnit.Id).FirstOrDefault();

                                                // If we don't already have it, add it
                                                if (roleInTargetBusinessUnit != null && !sourceUserRoles.Any(tr => tr.Id == roleInTargetBusinessUnit.Id))
                                                {
                                                    targetUserRolesToRestoreAfterClone.Add(roleInTargetBusinessUnit.ToEntityReference());
                                                }
                                            }
                                            if (targetUserRolesToRestoreAfterClone.Count > 0)
                                            {
                                                base.Service.Associate("systemuser", targetUserId, new Relationship("systemuserroles_association"), targetUserRolesToRestoreAfterClone);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // We will not change the Target User's business unit, keeping security roles as originally listed
                                        workAsyncInfo.Message = $"Keeping Target Business Unit as '{targetBusinessUnit.Name}'.";

                                        // Clear the target list if necessary
                                        if (cloneSettings.ClearTargetUserRolesListBeforeClone)
                                        {
                                            // Remove Target User's Security Roles
                                            EntityReferenceCollection removingTheseRolesFromTarget = new EntityReferenceCollection();
                                            foreach (var r in targetUserRoles)
                                            {
                                                removingTheseRolesFromTarget.Add(r.ToEntityReference());
                                            }
                                            base.Service.Disassociate("systemuser", targetUserId, new Relationship("systemuserroles_association"), removingTheseRolesFromTarget);
                                        }

                                        // Get reference to source role's ParentRootRole and use it find a new role with the current Business Unit
                                        var sourceRolesToGiveToTargetInTargetBU = new EntityReferenceCollection();
                                        foreach (var r in sourceUserRoles)
                                        {
                                            Guid parentRootRoleId = r.ParentRootRoleId;

                                            // Security role where ParentRootId is same but the Business Unit is the Target's Business Unit
                                            var roleInTargetBusinessUnit = cloneSettings.SecurityRoleMap.Where(m => m.ParentRootRoleId == parentRootRoleId && m.BusinessUnitId == targetBusinessUnit.Id).FirstOrDefault();

                                            if (roleInTargetBusinessUnit == null)
                                            {
                                                // Could not find a role in the desired business unit

                                            }
                                            // If we don't already have it, adding it to the Target
                                            else if (roleInTargetBusinessUnit != null &&
                                            (cloneSettings.ClearTargetUserRolesListBeforeClone || !targetUserRoles.Any(tr => tr.Id == roleInTargetBusinessUnit.Id)) )
                                            {
                                                sourceRolesToGiveToTargetInTargetBU.Add(roleInTargetBusinessUnit.ToEntityReference());
                                            }
                                        }
                                        if (sourceRolesToGiveToTargetInTargetBU.Count > 0)
                                        {
                                            base.Service.Associate("systemuser", targetUserId, new Relationship("systemuserroles_association"), sourceRolesToGiveToTargetInTargetBU);
                                        }
                                    }
                                }
                                else
                                {
                                    // SAME BUSINESS UNIT FOR SOURCE AND TARGET
                                    workAsyncInfo.Message = $"Same Source and Target Business Unit. Keeping Target Business Unit as '{sourceBusinessUnit.Name}'";
                                    // Clear the target list if necessary
                                    if (cloneSettings.ClearTargetUserRolesListBeforeClone)
                                    {
                                        // Remove Target User's Security Roles
                                        EntityReferenceCollection removingTheseRolesFromTarget = new EntityReferenceCollection();
                                        foreach (var r in targetUserRoles)
                                        {
                                            removingTheseRolesFromTarget.Add(r.ToEntityReference());
                                        }
                                        base.Service.Disassociate("systemuser", targetUserId, new Relationship("systemuserroles_association"), removingTheseRolesFromTarget);
                                    }

                                    // Give the Target User the Source User's security roles 
                                    // Since this is the same Business Group, we don't need anything fancy
                                    EntityReferenceCollection sourceRolesToGiveToTarget = new EntityReferenceCollection();
                                    foreach (var r in sourceUserRoles)
                                    {
                                        // Only add it if doesn't already exist
                                        if (!targetUserRoles.Any(tr => tr.Id == r.Id))
                                        {
                                            sourceRolesToGiveToTarget.Add(r.ToEntityReference());
                                        }
                                    }
                                    if (sourceRolesToGiveToTarget.Count > 0)
                                    {
                                        base.Service.Associate("systemuser", targetUserId, new Relationship("systemuserroles_association"), sourceRolesToGiveToTarget);
                                    }

                                }
                            }

                            // TEAM SYNC
                            // Get Target User Teams (exclude default teams)
                            if (cloneSettings.CloneTeams)
                            {
                                bool includeDefaultTeams = false;
                                var targetUserTeams = this.GetUserTeams(targetUserId, base.Service, includeDefaultTeams);
                                Guid[] memberIds = new Guid[] { targetUserId };

                                // Remove all Target User teams if required
                                if (cloneSettings.ClearTargetTeamsListBeforeClone)
                                {
                                    foreach (var userTeam in targetUserTeams)
                                    {
                                        MyPluginControl.RemoveMembersFromTeam(userTeam.Id, memberIds, base.Service);
                                    }
                                }

                                // Put Target User into all Source User's teams
                                foreach (var userTeam in this.GetUserTeams(sourceUserId, base.Service, includeDefaultTeams))
                                {
                                    // Only add membership where we cleared the list or it doesn't already exist in the Target users memberships
                                    if (cloneSettings.ClearTargetTeamsListBeforeClone || !targetUserTeams.Any(t => t.Id == userTeam.Id))
                                    {
                                        MyPluginControl.AddMembersToTeam(userTeam.Id, memberIds, base.Service);
                                    }
                                }
                            }

                        }
                        catch (Exception exception)
                        {
                            exception1 = exception;
                        }
                    });
                    workAsyncInfo.PostWorkCallBack = ((RunWorkerCompletedEventArgs ar) =>
                    {
                        if (exception1 == null)
                        {
                            MessageBox.Show($"Sync Completed: \r\n {cloneSettings.GetWorkStepsReport()}.");
                            // Refresh lists and keep selected items
                            RefreshUserListsAndKeepSelections();
                            return;
                        }

                        MessageBox.Show(exception1.Message);
                    });
                    workAsyncInfo.AsyncArgument = null;
                    workAsyncInfo.MessageWidth = 340;
                    workAsyncInfo.MessageHeight = 150;
                    base.WorkAsync(workAsyncInfo);
                }

            }
        }


        private void SourceUserChanged()
        {
            if (this.Source.SelectedValue != null)
            {
                Guid sourceUserId = new Guid(this.Source.SelectedValue.ToString());
                bool includeDefaultTeams = true;
                lvSourceRoles.Items.Clear();

                var sourceRoles = GetUserRoles(sourceUserId);
                // Populate the list
                var list = new List<ListViewItem>();
                foreach (var r in sourceRoles)
                {
                    var item = new ListViewItem { Text = r.Name, Tag = r.Id };
                    list.Add(item);
                }

                lvSourceRoles.Items.AddRange(list.ToArray());
                lvSourceRoles.Sorting = SortOrder.Ascending;
                lvSourceRoles.Sort();

                // Do the Source Teams List
                lvSourceTeams.Items.Clear();

                List<EntityReference> sourceTeams = GetUserTeams(sourceUserId, base.Service, includeDefaultTeams);
                // Populate the list
                var teamList = new List<ListViewItem>();
                foreach (var t in sourceTeams)
                {
                    teamList.Add(new ListViewItem { Text = t.Name, Tag = t.Id });
                }

                lvSourceTeams.Items.AddRange(teamList.ToArray());
                lvSourceTeams.Sorting = SortOrder.Ascending;
                lvSourceTeams.Sort();
            }
        }
        private void Source_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SourceUserChanged();
        }

        private void Target_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TargetUserChanged();
        }

        private void TargetUserChanged()
        {
            if (this.Target.SelectedValue != null)
            {
                Guid targetUserId = new Guid(this.Target.SelectedValue.ToString());
                EntityReference targetBusinessUnit = Extensions.GetNamedEntityReference("businessunit", new Guid(((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitId"]), ((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitName"]);

                bool includeDefaultTeams = true;
                lvTargetRoles.Items.Clear();
                //List<EntityReference> targetRoles;
                // EntityReference sourceBusinessUnit;
                // GetUserRoles(targetId, out targetRoles, out sourceBusinessUnit);
                var targetRoles = GetUserRoles(targetUserId);
                // Populate the list
                var list = new List<ListViewItem>();
                foreach (var r in targetRoles)
                {
                    var item = new ListViewItem { Text = r.Name, Tag = r.Id };
                    //item.SubItems.Add(r.LogicalName);
                    list.Add(item);
                }

                lvTargetRoles.Items.AddRange(list.ToArray());
                lvTargetRoles.Sorting = SortOrder.Ascending;
                lvTargetRoles.Sort();

                // Do the Target Teams List
                lvTargetTeams.Items.Clear();
                //Guid sourceId = new Guid(this.Source.SelectedValue.ToString());
                List<EntityReference> targetTeams = GetUserTeams(targetUserId, base.Service, includeDefaultTeams);
                // Populate the list
                var teamList = new List<ListViewItem>();
                foreach (var t in targetTeams)
                {
                    teamList.Add(new ListViewItem { Text = t.Name, Tag = t.Id }); 
                }

                lvTargetTeams.Items.AddRange(teamList.ToArray());
                lvTargetTeams.Sorting = SortOrder.Ascending;
                lvTargetTeams.Sort();

                // Pick the Business Unit from the Tree
                tvBusinessUnitSelection.SelectedNode = tvBusinessUnitSelection.Nodes.Find(targetBusinessUnit.Name, true).FirstOrDefault();

            }
        }
        private void tsbRefreshUserLists_Click(object sender, EventArgs e)
        {
            RefreshUserListsAndKeepSelections();
        }

        private void RefreshUserListsAndKeepSelections()
        {
            // Get the selected Source and Taret user so we can preserve them after refreshing user lists
            var sourceUserId = this.Source.SelectedValue.ToString();
            var targetUserId = this.Target.SelectedValue.ToString();
            bool includeInactiveUsers = chkIncludeInactiveUsersInSourceUserList.Checked;
            // Refresh the lists
            GetUsers(sourceUserId, targetUserId, includeInactiveUsers);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chkOpenLinksInClassicInterface_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateInterface()
        {


            // Clone Security Roles must be enabled to allow Change Business Unit or Clear Target User Role List
            if (chkCloneSecurityRoles.Checked)
            {
                chkClearTargetUserRoleListBeforeClone.Enabled = true;
                chkClearTargetUserRoleListBeforeClone.AutoCheck = true;
                chkChangeBusinessUnit.Enabled = true;
                chkChangeBusinessUnit.AutoCheck = true;
                lvSourceRoles.Enabled = true;
                lvTargetRoles.Enabled = true;
            }
            else
            {
                chkClearTargetUserRoleListBeforeClone.Enabled = false;
                chkClearTargetUserRoleListBeforeClone.AutoCheck = false;
                chkChangeBusinessUnit.Enabled = false;
                chkChangeBusinessUnit.AutoCheck = false;
                lvSourceRoles.Enabled = false;
                lvTargetRoles.Enabled = false;
            }

            // Clone Teams must be enabled to allow Clear Target Teams List
            if (chkCloneTeams.Checked)
            {
                chkClearTargetTeamListBeforeClone.Enabled = true;
                chkClearTargetTeamListBeforeClone.AutoCheck = true;
                lvSourceTeams.Enabled = true;
                lvTargetTeams.Enabled = true;
            }
            else
            {
                chkClearTargetTeamListBeforeClone.Enabled = false;
                chkClearTargetTeamListBeforeClone.AutoCheck = false;
                lvSourceTeams.Enabled = false;
                lvTargetTeams.Enabled = false;
            }


            if (chkClearTargetUserRoleListBeforeClone.Checked)
            {
                lvTargetRoles.Enabled = false;
                //lvTargetRoles.BackColor = Color.Silver;
            }
            else
            {
                lvTargetRoles.Enabled = true;
                //lvTargetRoles.BackColor = Color.White;
            }

            if (chkClearTargetTeamListBeforeClone.Checked)
            {
                lvTargetTeams.Enabled = false;
                //lvTargetTeams.BackColor = Color.Silver;
            }
            else
            {
                lvTargetTeams.Enabled = true;
                //lvTargetTeams.BackColor = Color.White;
            }


        }
        private void chkCloneSecurityRoles_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void chkCloneTeams_CheckedChanged(object sender, EventArgs e)
        {

            UpdateInterface();
        }

        private void chkClearTargetUserRoleListBeforeClone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void chkClearTargetTeamListBeforeClone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInterface();
        }

        private void tvBusinessUnitSelection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            System.Windows.Forms.TreeNode tn = tvBusinessUnitSelection.SelectedNode;
            //MessageBox.Show(string.Format("You selected: {0}, {1}", tn.Text, tn.Tag));
            cboBusinessUnit.SelectedValue = tn.Tag as string;
            // We can't see it's selected if it's out of focus
            tvBusinessUnitSelection.Focus();
        }

        private void btnChangeBusinessUnit_Click(object sender, EventArgs e)
        {
            // Get the Clone Settings
            var cloneSettings = GetCloneSettingsFromUI(deferLoadingSecurityRoleMap: true);
            var userFullName = ((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["UserFullName"];

            Guid targetUserId = new Guid(this.Target.SelectedValue.ToString());
            EntityReference targetUserCurrentBusinessUnit = Extensions.GetNamedEntityReference("businessunit", new Guid(((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitId"]), ((System.Web.UI.WebControls.ListItem)this.Target.SelectedItem).Attributes["BusinessUnitName"]);


            EntityReference newBusinessUnit = cboBusinessUnit.SelectedItem!= null ? Extensions.GetNamedEntityReference("businessunit", new Guid(((System.Web.UI.WebControls.ListItem)cboBusinessUnit.SelectedItem).Value), ((System.Web.UI.WebControls.ListItem)cboBusinessUnit.SelectedItem).Text) : null; 
            Exception exception1 = null;

            if (newBusinessUnit == null)
            {
                MessageBox.Show("Be sure to select the Target User's new Business Unit.");
                return;
            }            
            else if (targetUserCurrentBusinessUnit.Id == newBusinessUnit.Id)
            {
                MessageBox.Show("Target User's current Business Unit and new Business Unit are the same.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Do you want to change {userFullName}'s business unit from\r\n {targetUserCurrentBusinessUnit.Name} \r\nto\r\n {newBusinessUnit.Name}?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    WorkAsyncInfo workAsyncInfo = new WorkAsyncInfo();
                    workAsyncInfo.Message = $"Changing Business Unit"; //  the Business Unit, Security Roles, and Teams
                    workAsyncInfo.Work = ((BackgroundWorker w, DoWorkEventArgs ar) =>
                    {
                        // Now we can do the potentially time consumming load of the Security Role Map
                        cloneSettings = GetCloneSettingsFromUI(deferLoadingSecurityRoleMap: false);

                        //var sourceUserRoles = new List<SecurityRole>();
                        var targetUserRoles = new List<SecurityRole>();

                        try
                        {
                            // Get Target User Security Roles
                            targetUserRoles = GetUserRoles(targetUserId);

                            // SECURITY ROLE SYNC
                            if (cloneSettings.CloneSecurityRoles)
                            {
                                // If Business Units are different, we will change the Target Business Unit to match the Source User's Business Unit 
                                // DIFFERENT BUSINESS UNIT FOR SOURCE AND TARGET
                                if (cloneSettings.ChangeBusinessUnit)
                                {
                                    // We will change Target Business Unit to Match Source Business Unit
                                    workAsyncInfo.Message = $"Changing Target Business Unit from '{targetUserCurrentBusinessUnit.Name}' to '{newBusinessUnit.Name}'";

                                    // Change Business unit, which automatically clears the Target User's Security Roles
                                    SetBusinessSystemUserRequest setBusinessSystemUserRequest = new SetBusinessSystemUserRequest();
                                    setBusinessSystemUserRequest.BusinessId = newBusinessUnit.Id;
                                    setBusinessSystemUserRequest.UserId = targetUserId;
                                    setBusinessSystemUserRequest.ReassignPrincipal = new EntityReference("systemuser", targetUserId);
                                    SetBusinessSystemUserResponse setBusinessSystemUserResponse = (SetBusinessSystemUserResponse)base.Service.Execute(setBusinessSystemUserRequest);

                                    // We will preserve the target user's roles

                                    // Get reference to source role's ParentRootRole and then we will use that to find a new role with the current Buisness Unit
                                    var targetUserRolesToRestoreAfterClone = new EntityReferenceCollection();
                                    foreach (var r in targetUserRoles)
                                    {
                                        Guid parentRootRoleId = r.ParentRootRoleId;
                                        // Security role where ParentRootId is same but the Business Unit is the Target's Business Unit
                                        // Actually needs to be the Source Business Unit since we are changing Target BU to match Source
                                        var roleInTargetBusinessUnit = cloneSettings.SecurityRoleMap.Where(m => m.ParentRootRoleId == parentRootRoleId && m.BusinessUnitId == newBusinessUnit.Id).FirstOrDefault();

                                        // If we don't already have it, add it
                                        if (roleInTargetBusinessUnit != null) // && !sourceUserRoles.Any(tr => tr.Id == roleInTargetBusinessUnit.Id))
                                        {
                                            targetUserRolesToRestoreAfterClone.Add(roleInTargetBusinessUnit.ToEntityReference());
                                        }
                                    }
                                    if (targetUserRolesToRestoreAfterClone.Count > 0)
                                    {
                                        base.Service.Associate("systemuser", targetUserId, new Relationship("systemuserroles_association"), targetUserRolesToRestoreAfterClone);
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            exception1 = exception;
                        }
                    });
                    workAsyncInfo.PostWorkCallBack = ((RunWorkerCompletedEventArgs ar) =>
                    {
                        if (exception1 == null)
                        {
                            MessageBox.Show($"Completed Changing Business Unit");
                            // Refresh lists and keep selected items
                            RefreshUserListsAndKeepSelections();
                            // TODO: Do we need to refresh EVERYTHING NOW!?
                            return;
                        }
                        else
                        {
                            MessageBox.Show(exception1.Message);
                        }
                    });
                    workAsyncInfo.AsyncArgument = null;
                    workAsyncInfo.MessageWidth = 340;
                    workAsyncInfo.MessageHeight = 150;
                    base.WorkAsync(workAsyncInfo);
                }

            }
        }

        /*
* 
private void tsbSample_Click(object sender, EventArgs e)
{
// The ExecuteMethod method handles connecting to an
// organization if XrmToolBox is not yet connected
ExecuteMethod(GetAccounts);
}

private void GetAccounts()
{
WorkAsync(new WorkAsyncInfo
{
Message = "Getting accounts",
Work = (worker, args) =>
{
args.Result = Service.RetrieveMultiple(new QueryExpression("account")
{
TopCount = 50
});
},
PostWorkCallBack = (args) =>
{
if (args.Error != null)
{
MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
var result = args.Result as EntityCollection;
if (result != null)
{
MessageBox.Show($"Found {result.Entities.Count} accounts");
}
}
});
}

*/
    }
        }
 