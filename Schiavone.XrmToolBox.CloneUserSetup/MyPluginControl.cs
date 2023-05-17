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

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;

        private IContainer components;

        private ToolStrip toolStripMenu;

        private ToolStripButton tsbClose;

        private ToolStripSeparator tssSeparator1;
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
            base.ExecuteMethod(new Action(this.GetInitialUsers));
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


        public static void AddMembersToTeam(Guid teamId, Guid[] membersId, IOrganizationService service)
        {
            AddMembersTeamRequest addMembersTeamRequest = new AddMembersTeamRequest();
            addMembersTeamRequest.TeamId = teamId;
            addMembersTeamRequest.MemberIds = membersId;
            service.Execute(addMembersTeamRequest);
        }



        private void GetUserRoles(Guid userid, out List<EntityReference> roleIds, out EntityReference erBusinessUnit)
        {
            QueryExpression queryExpression = new QueryExpression("systemuserroles");
            queryExpression.ColumnSet.AddColumns(new string[] { "systemuserid" });
            queryExpression.Criteria.AddCondition("systemuserid", 0, new object[] { userid });
            queryExpression.AddLink("systemuser", "systemuserid", "systemuserid", 0).Columns.AddColumns(new string[] { "fullname", "internalemailaddress", "businessunitid" });
            queryExpression.AddLink("role", "roleid", "roleid", 0).Columns.AddColumns(new string[] { "roleid", "name" });
            EntityCollection userRoles = base.Service.RetrieveMultiple(queryExpression);
            roleIds = new List<EntityReference>();
            erBusinessUnit = new EntityReference(); //new Guid();

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

        private void GetInitialUsers()
        {
            GetUsers(null, null);
        }
        private void GetUsers(string sourceSelectedUserId = null, string targetSelectedUserId = null)
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
                queryExpression.ColumnSet.AddColumns(new string[] { "systemuserid", "fullname", "businessunitid" });

                // Add link-entity for businessunit name
                var QEsystemuser_businessunit = queryExpression.AddLink("businessunit", "businessunitid", "businessunitid");
                QEsystemuser_businessunit.Columns.AddColumns("name");
                QEsystemuser_businessunit.EntityAlias = "bu";

                queryExpression.Criteria.AddFilter(filterExpression);

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
                    ListItemCollection TargetListItems = new ListItemCollection();
                    for (int i = 0; i < result.Entities.Count; i++)
                    {
                        sourceListItems.Add(new ListItem($"{result.Entities[i].Attributes["fullname"]} [{((AliasedValue)result.Entities[i].Attributes["bu.name"]).Value}]",
                            result.Entities[i].Attributes["systemuserid"].ToString()));
                        TargetListItems.Add(new ListItem($"{result.Entities[i].Attributes["fullname"]} [{((AliasedValue)result.Entities[i].Attributes["bu.name"]).Value}]",
                            result.Entities[i].Attributes["systemuserid"].ToString()));
                    }
                    // this.Source.SuspendLayout();
                    this.Source.DataSource = sourceListItems;
                    this.Source.DisplayMember = "Text";
                    this.Source.ValueMember = "Value";

                    this.Target.DataSource = TargetListItems;
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

        public static void RemoveMembersFromTeam(Guid teamId, Guid[] membersId, IOrganizationService service)
        {
            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest();
            removeMembersTeamRequest.TeamId = teamId;
            removeMembersTeamRequest.MemberIds = membersId;
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
            bool forceUci = this.chkOpenLinksInUCI.Checked;
            if (sourceId != null)
            {
                OpenEntityReference(new EntityReference("systemuser", new Guid(sourceId)), forceUci);
            }
        }

        private void btnOpenTarget_Click(object sender, EventArgs e)
        {
            var targetId = this.Target.SelectedValue.ToString();
            bool forceUci = this.chkOpenLinksInUCI.Checked;
            if (targetId != null)
            {
                OpenEntityReference(new EntityReference("systemuser", new Guid(targetId)), forceUci);
            }
        }

        private void tsDoTheSync_Click(object sender, EventArgs e)
        {

            Guid sourceId = new Guid(this.Source.SelectedValue.ToString());
            Guid targetId = new Guid(this.Target.SelectedValue.ToString());
            Exception exception1 = null;

            if (sourceId == targetId)
            {
                MessageBox.Show("Source and Target user are the same.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you sure you want to copy Business Unit, Security Roles, and Teams from source to target user?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    WorkAsyncInfo workAsyncInfo = new WorkAsyncInfo();
                    workAsyncInfo.Message = "Syncing the Business Unit, Security Roles, and Teams from Source to Target...";
                    workAsyncInfo.Work = ((BackgroundWorker w, DoWorkEventArgs ar) =>
                    {
                        List<EntityReference> sourceRoles;
                        EntityReference sourceBusinessUnit;
                        List<EntityReference> targetRoles;
                        EntityReference targetBusinessUnit;
                        try
                        {
                            //TODO: Also sync up the Buisness Group too!

                            // Get Source Users Security Roles
                            this.GetUserRoles(sourceId, out sourceRoles, out sourceBusinessUnit);
                            // Get Target Users Security Roles
                            this.GetUserRoles(targetId, out targetRoles, out targetBusinessUnit);

                            // If Buisness Units are different, change the Target Business Unit to match the Source User's Business Unit 

                            if (sourceBusinessUnit.Id != targetBusinessUnit.Id)
                            {
                                workAsyncInfo.Message = $"Changing Target Business Unit from '{targetBusinessUnit.Name}' to '{sourceBusinessUnit.Name}'";
                                // (Target User's Security Roles are automatically removed when changing Business Unit)
                                SetBusinessSystemUserRequest setBusinessSystemUserRequest = new SetBusinessSystemUserRequest();
                                setBusinessSystemUserRequest.BusinessId = sourceBusinessUnit.Id;
                                setBusinessSystemUserRequest.UserId = targetId;
                                setBusinessSystemUserRequest.ReassignPrincipal = new EntityReference("systemuser", targetId);
                                SetBusinessSystemUserResponse setBusinessSystemUserResponse = (SetBusinessSystemUserResponse)base.Service.Execute(setBusinessSystemUserRequest);
                            }
                            else
                            {
                                workAsyncInfo.Message = $"Same Source and Target Business Unit. Keeping Target BU as '{sourceBusinessUnit.Name}'";
                                // Remove Target User's Security Roles
                                EntityReferenceCollection entityReferenceCollection = new EntityReferenceCollection();
                                foreach (EntityReference r in targetRoles)
                                {
                                    entityReferenceCollection.Add(r);
                                }
                                base.Service.Disassociate("systemuser", targetId, new Relationship("systemuserroles_association"), entityReferenceCollection);
                            }

                            // Give the Target User the Source User's security roles 
                            EntityReferenceCollection entityReferenceCollection1 = new EntityReferenceCollection();
                            foreach (EntityReference r in sourceRoles)
                            {
                                entityReferenceCollection1.Add(r); // new EntityReference("role", g));
                            }
                            base.Service.Associate("systemuser", targetId, new Relationship("systemuserroles_association"), entityReferenceCollection1);

                            // Team Sync
                            // Get Target User Teams (exclude default teams)
                            bool includeDefaultTeams = false;
                            var targetUserTeams = this.GetUserTeams(targetId, base.Service, includeDefaultTeams);
                            Guid[] teamIds = new Guid[] { targetId };

                            // Remove all Target User teams
                            foreach (var userTeam in targetUserTeams)
                            {
                                MyPluginControl.RemoveMembersFromTeam(userTeam.Id, teamIds, base.Service);
                            }

                            // Put Target User into all Source User's teams
                            foreach (var userTeam1 in this.GetUserTeams(sourceId, base.Service, includeDefaultTeams))
                            {
                                MyPluginControl.AddMembersToTeam(userTeam1.Id, teamIds, base.Service);
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
                            MessageBox.Show("Business Unit, Security Roles, and Teams synced.");
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
                Guid sourceId = new Guid(this.Source.SelectedValue.ToString());
                bool includeDefaultTeams = true;
                lvSourceRoles.Items.Clear();
                List<EntityReference> sourceRoles;
                EntityReference sourceBusinessUnit;
                GetUserRoles(sourceId, out sourceRoles, out sourceBusinessUnit);
                // Populate the list
                var list = new List<ListViewItem>();
                foreach (var r in sourceRoles)
                {
                    var item = new ListViewItem { Text = r.Name, Tag = r.Id };
                    //item.SubItems.Add(r.LogicalName);
                    list.Add(item);
                }

                lvSourceRoles.Items.AddRange(list.ToArray());
                lvSourceRoles.Sorting = SortOrder.Ascending;
                lvSourceRoles.Sort();

                // Do the Source Teams List
                lvSourceTeams.Items.Clear();
                //Guid sourceId = new Guid(this.Source.SelectedValue.ToString());
                List<EntityReference> sourceTeams = GetUserTeams(sourceId, base.Service, includeDefaultTeams);
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
                Guid targetId = new Guid(this.Target.SelectedValue.ToString());
                bool includeDefaultTeams = true;
                lvTargetRoles.Items.Clear();
                List<EntityReference> targetRoles;
                EntityReference sourceBusinessUnit;
                GetUserRoles(targetId, out targetRoles, out sourceBusinessUnit);
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
                List<EntityReference> targetTeams = GetUserTeams(targetId, base.Service, includeDefaultTeams);
                // Populate the list
                var teamList = new List<ListViewItem>();
                foreach (var t in targetTeams)
                {
                    teamList.Add(new ListViewItem { Text = t.Name, Tag = t.Id });
                }

                lvTargetTeams.Items.AddRange(teamList.ToArray());
                lvTargetTeams.Sorting = SortOrder.Ascending;
                lvTargetTeams.Sort();

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
            // Refresh the lists
            GetUsers(sourceUserId, targetUserId);
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
 