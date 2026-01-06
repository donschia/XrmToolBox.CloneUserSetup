using System;
using System.Drawing;
using System.Windows.Forms;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
       // private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants
        /*
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSample = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbSample,
            this.toolStripButton1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(746, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(107, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbSample
            // 
            this.tsbSample.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSample.Name = "tsbSample";
            this.tsbSample.Size = new System.Drawing.Size(57, 28);
            this.tsbSample.Text = "Try me";
            this.tsbSample.Click += new System.EventHandler(this.tsbSample_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(746, 370);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSample;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;

        */

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbResetToDefaults = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshUserLists = new System.Windows.Forms.ToolStripButton();
            this.tsDoTheSync = new System.Windows.Forms.ToolStripButton();
            this.chkOpenLinksInClassicInterface = new System.Windows.Forms.CheckBox();
            this.grpSourceUser = new System.Windows.Forms.GroupBox();
            this.chkIncludeInactiveUsersInSourceUserList = new System.Windows.Forms.CheckBox();
            this.chkCloneSecurityRoles = new System.Windows.Forms.CheckBox();
            this.chkCloneTeams = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvSourceTeams = new System.Windows.Forms.ListView();
            this.SourceUser = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenSource = new System.Windows.Forms.Button();
            this.lvSourceRoles = new System.Windows.Forms.ListView();
            this.chkChangeBusinessUnit = new System.Windows.Forms.CheckBox();
            this.grpTargetUser = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnChangeBusinessUnit = new System.Windows.Forms.Button();
            this.tvBusinessUnitSelection = new System.Windows.Forms.TreeView();
            this.cboBusinessUnit = new System.Windows.Forms.ComboBox();
            this.chkClearTargetUserRoleListBeforeClone = new System.Windows.Forms.CheckBox();
            this.chkClearTargetTeamListBeforeClone = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvTargetTeams = new System.Windows.Forms.ListView();
            this.TargetUser = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenTarget = new System.Windows.Forms.Button();
            this.lvTargetRoles = new System.Windows.Forms.ListView();
            this.toolStripMenu.SuspendLayout();
            this.grpSourceUser.SuspendLayout();
            this.grpTargetUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbResetToDefaults,
            this.tsbRefreshUserLists,
            this.tsDoTheSync});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1356, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(110, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbResetToDefaults
            // 
            this.tsbResetToDefaults.Image = ((System.Drawing.Image)(resources.GetObject("tsbResetToDefaults.Image")));
            this.tsbResetToDefaults.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbResetToDefaults.Name = "tsbResetToDefaults";
            this.tsbResetToDefaults.Size = new System.Drawing.Size(125, 28);
            this.tsbResetToDefaults.Text = "Reset To Defaults";
            this.tsbResetToDefaults.Click += new System.EventHandler(this.tsbResetToDefaults_Click);
            // 
            // tsbRefreshUserLists
            // 
            this.tsbRefreshUserLists.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshUserLists.Image")));
            this.tsbRefreshUserLists.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshUserLists.Name = "tsbRefreshUserLists";
            this.tsbRefreshUserLists.Size = new System.Drawing.Size(126, 28);
            this.tsbRefreshUserLists.Text = "Refresh User Lists";
            this.tsbRefreshUserLists.Click += new System.EventHandler(this.tsbRefreshUserLists_Click);
            // 
            // tsDoTheSync
            // 
            this.tsDoTheSync.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsDoTheSync.Image = ((System.Drawing.Image)(resources.GetObject("tsDoTheSync.Image")));
            this.tsDoTheSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDoTheSync.Name = "tsDoTheSync";
            this.tsDoTheSync.Size = new System.Drawing.Size(206, 28);
            this.tsDoTheSync.Text = "Apply Changes to TARGET User";
            this.tsDoTheSync.Click += new System.EventHandler(this.tsDoTheSync_Click);
            // 
            // chkOpenLinksInClassicInterface
            // 
            this.chkOpenLinksInClassicInterface.AutoSize = true;
            this.chkOpenLinksInClassicInterface.Checked = true;
            this.chkOpenLinksInClassicInterface.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenLinksInClassicInterface.Location = new System.Drawing.Point(16, 34);
            this.chkOpenLinksInClassicInterface.Name = "chkOpenLinksInClassicInterface";
            this.chkOpenLinksInClassicInterface.Size = new System.Drawing.Size(172, 17);
            this.chkOpenLinksInClassicInterface.TabIndex = 14;
            this.chkOpenLinksInClassicInterface.Text = "Open Links in Classic Interface";
            this.chkOpenLinksInClassicInterface.UseVisualStyleBackColor = true;
            this.chkOpenLinksInClassicInterface.CheckedChanged += new System.EventHandler(this.chkOpenLinksInClassicInterface_CheckedChanged);
            // 
            // grpSourceUser
            // 
            this.grpSourceUser.BackColor = System.Drawing.Color.Cornsilk;
            this.grpSourceUser.Controls.Add(this.chkIncludeInactiveUsersInSourceUserList);
            this.grpSourceUser.Controls.Add(this.chkCloneSecurityRoles);
            this.grpSourceUser.Controls.Add(this.chkCloneTeams);
            this.grpSourceUser.Controls.Add(this.label6);
            this.grpSourceUser.Controls.Add(this.label1);
            this.grpSourceUser.Controls.Add(this.lvSourceTeams);
            this.grpSourceUser.Controls.Add(this.SourceUser);
            this.grpSourceUser.Controls.Add(this.label3);
            this.grpSourceUser.Controls.Add(this.btnOpenSource);
            this.grpSourceUser.Controls.Add(this.lvSourceRoles);
            this.grpSourceUser.Location = new System.Drawing.Point(4, 58);
            this.grpSourceUser.Name = "grpSourceUser";
            this.grpSourceUser.Size = new System.Drawing.Size(510, 581);
            this.grpSourceUser.TabIndex = 23;
            this.grpSourceUser.TabStop = false;
            this.grpSourceUser.Text = "SOURCE";
            // 
            // chkIncludeInactiveUsersInSourceUserList
            // 
            this.chkIncludeInactiveUsersInSourceUserList.AutoSize = true;
            this.chkIncludeInactiveUsersInSourceUserList.Location = new System.Drawing.Point(16, 59);
            this.chkIncludeInactiveUsersInSourceUserList.Name = "chkIncludeInactiveUsersInSourceUserList";
            this.chkIncludeInactiveUsersInSourceUserList.Size = new System.Drawing.Size(132, 17);
            this.chkIncludeInactiveUsersInSourceUserList.TabIndex = 14;
            this.chkIncludeInactiveUsersInSourceUserList.Text = "Include Inactive Users";
            this.chkIncludeInactiveUsersInSourceUserList.UseVisualStyleBackColor = true;
            this.chkIncludeInactiveUsersInSourceUserList.CheckedChanged += new System.EventHandler(this.chkIncludeInactiveUsersInSourceUserList_CheckedChanged);
            // 
            // chkCloneSecurityRoles
            // 
            this.chkCloneSecurityRoles.AutoSize = true;
            this.chkCloneSecurityRoles.Checked = true;
            this.chkCloneSecurityRoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloneSecurityRoles.Location = new System.Drawing.Point(385, 79);
            this.chkCloneSecurityRoles.Name = "chkCloneSecurityRoles";
            this.chkCloneSecurityRoles.Size = new System.Drawing.Size(124, 17);
            this.chkCloneSecurityRoles.TabIndex = 26;
            this.chkCloneSecurityRoles.Text = "Clone Security Roles";
            this.chkCloneSecurityRoles.UseVisualStyleBackColor = true;
            this.chkCloneSecurityRoles.CheckedChanged += new System.EventHandler(this.chkCloneSecurityRoles_CheckedChanged);
            // 
            // chkCloneTeams
            // 
            this.chkCloneTeams.AutoSize = true;
            this.chkCloneTeams.Checked = true;
            this.chkCloneTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloneTeams.Location = new System.Drawing.Point(385, 328);
            this.chkCloneTeams.Name = "chkCloneTeams";
            this.chkCloneTeams.Size = new System.Drawing.Size(88, 17);
            this.chkCloneTeams.TabIndex = 27;
            this.chkCloneTeams.Text = "Clone Teams";
            this.chkCloneTeams.UseVisualStyleBackColor = true;
            this.chkCloneTeams.CheckedChanged += new System.EventHandler(this.chkCloneTeams_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Source User Teams";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "     Source User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvSourceTeams
            // 
            this.lvSourceTeams.HideSelection = false;
            this.lvSourceTeams.Location = new System.Drawing.Point(16, 347);
            this.lvSourceTeams.Name = "lvSourceTeams";
            this.lvSourceTeams.Size = new System.Drawing.Size(480, 213);
            this.lvSourceTeams.TabIndex = 29;
            this.lvSourceTeams.UseCompatibleStateImageBehavior = false;
            this.lvSourceTeams.View = System.Windows.Forms.View.List;
            // 
            // SourceUser
            // 
            this.SourceUser.FormattingEnabled = true;
            this.SourceUser.Location = new System.Drawing.Point(16, 36);
            this.SourceUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SourceUser.Name = "SourceUser";
            this.SourceUser.Size = new System.Drawing.Size(420, 21);
            this.SourceUser.TabIndex = 24;
            this.SourceUser.SelectionChangeCommitted += new System.EventHandler(this.Source_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Source User Roles";
            // 
            // btnOpenSource
            // 
            this.btnOpenSource.Location = new System.Drawing.Point(438, 36);
            this.btnOpenSource.Name = "btnOpenSource";
            this.btnOpenSource.Size = new System.Drawing.Size(54, 25);
            this.btnOpenSource.TabIndex = 26;
            this.btnOpenSource.Text = "Open";
            this.btnOpenSource.UseVisualStyleBackColor = true;
            this.btnOpenSource.Click += new System.EventHandler(this.btnOpenSource_Click);
            // 
            // lvSourceRoles
            // 
            this.lvSourceRoles.HideSelection = false;
            this.lvSourceRoles.Location = new System.Drawing.Point(16, 104);
            this.lvSourceRoles.Name = "lvSourceRoles";
            this.lvSourceRoles.Size = new System.Drawing.Size(480, 213);
            this.lvSourceRoles.TabIndex = 27;
            this.lvSourceRoles.UseCompatibleStateImageBehavior = false;
            this.lvSourceRoles.View = System.Windows.Forms.View.List;
            // 
            // chkChangeBusinessUnit
            // 
            this.chkChangeBusinessUnit.AutoSize = true;
            this.chkChangeBusinessUnit.Checked = true;
            this.chkChangeBusinessUnit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChangeBusinessUnit.Location = new System.Drawing.Point(345, 69);
            this.chkChangeBusinessUnit.Name = "chkChangeBusinessUnit";
            this.chkChangeBusinessUnit.Size = new System.Drawing.Size(130, 17);
            this.chkChangeBusinessUnit.TabIndex = 25;
            this.chkChangeBusinessUnit.Text = "Change Business Unit";
            this.chkChangeBusinessUnit.UseVisualStyleBackColor = true;
            // 
            // grpTargetUser
            // 
            this.grpTargetUser.BackColor = System.Drawing.Color.AliceBlue;
            this.grpTargetUser.Controls.Add(this.label7);
            this.grpTargetUser.Controls.Add(this.btnChangeBusinessUnit);
            this.grpTargetUser.Controls.Add(this.tvBusinessUnitSelection);
            this.grpTargetUser.Controls.Add(this.cboBusinessUnit);
            this.grpTargetUser.Controls.Add(this.chkChangeBusinessUnit);
            this.grpTargetUser.Controls.Add(this.chkClearTargetUserRoleListBeforeClone);
            this.grpTargetUser.Controls.Add(this.chkClearTargetTeamListBeforeClone);
            this.grpTargetUser.Controls.Add(this.label5);
            this.grpTargetUser.Controls.Add(this.label2);
            this.grpTargetUser.Controls.Add(this.lvTargetTeams);
            this.grpTargetUser.Controls.Add(this.TargetUser);
            this.grpTargetUser.Controls.Add(this.label4);
            this.grpTargetUser.Controls.Add(this.btnOpenTarget);
            this.grpTargetUser.Controls.Add(this.lvTargetRoles);
            this.grpTargetUser.Location = new System.Drawing.Point(521, 59);
            this.grpTargetUser.Name = "grpTargetUser";
            this.grpTargetUser.Size = new System.Drawing.Size(833, 580);
            this.grpTargetUser.TabIndex = 24;
            this.grpTargetUser.TabStop = false;
            this.grpTargetUser.Text = "TARGET";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(574, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Business Unit";
            // 
            // btnChangeBusinessUnit
            // 
            this.btnChangeBusinessUnit.Location = new System.Drawing.Point(573, 47);
            this.btnChangeBusinessUnit.Name = "btnChangeBusinessUnit";
            this.btnChangeBusinessUnit.Size = new System.Drawing.Size(252, 23);
            this.btnChangeBusinessUnit.TabIndex = 35;
            this.btnChangeBusinessUnit.Text = "Change Target User\'s Business Unit";
            this.btnChangeBusinessUnit.UseVisualStyleBackColor = true;
            this.btnChangeBusinessUnit.Click += new System.EventHandler(this.btnChangeBusinessUnit_Click);
            // 
            // tvBusinessUnitSelection
            // 
            this.tvBusinessUnitSelection.Location = new System.Drawing.Point(573, 103);
            this.tvBusinessUnitSelection.Name = "tvBusinessUnitSelection";
            this.tvBusinessUnitSelection.Size = new System.Drawing.Size(252, 456);
            this.tvBusinessUnitSelection.TabIndex = 34;
            this.tvBusinessUnitSelection.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBusinessUnitSelection_AfterSelect);
            // 
            // cboBusinessUnit
            // 
            this.cboBusinessUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBusinessUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBusinessUnit.FormattingEnabled = true;
            this.cboBusinessUnit.Location = new System.Drawing.Point(651, 76);
            this.cboBusinessUnit.Name = "cboBusinessUnit";
            this.cboBusinessUnit.Size = new System.Drawing.Size(174, 21);
            this.cboBusinessUnit.TabIndex = 33;
            // 
            // chkClearTargetUserRoleListBeforeClone
            // 
            this.chkClearTargetUserRoleListBeforeClone.AutoSize = true;
            this.chkClearTargetUserRoleListBeforeClone.Checked = true;
            this.chkClearTargetUserRoleListBeforeClone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearTargetUserRoleListBeforeClone.Location = new System.Drawing.Point(345, 86);
            this.chkClearTargetUserRoleListBeforeClone.Name = "chkClearTargetUserRoleListBeforeClone";
            this.chkClearTargetUserRoleListBeforeClone.Size = new System.Drawing.Size(222, 17);
            this.chkClearTargetUserRoleListBeforeClone.TabIndex = 32;
            this.chkClearTargetUserRoleListBeforeClone.Text = "Clear Target User Roles List Before Clone";
            this.chkClearTargetUserRoleListBeforeClone.UseVisualStyleBackColor = true;
            this.chkClearTargetUserRoleListBeforeClone.CheckedChanged += new System.EventHandler(this.chkClearTargetUserRoleListBeforeClone_CheckedChanged);
            // 
            // chkClearTargetTeamListBeforeClone
            // 
            this.chkClearTargetTeamListBeforeClone.AutoSize = true;
            this.chkClearTargetTeamListBeforeClone.Checked = true;
            this.chkClearTargetTeamListBeforeClone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearTargetTeamListBeforeClone.Location = new System.Drawing.Point(345, 327);
            this.chkClearTargetTeamListBeforeClone.Name = "chkClearTargetTeamListBeforeClone";
            this.chkClearTargetTeamListBeforeClone.Size = new System.Drawing.Size(202, 17);
            this.chkClearTargetTeamListBeforeClone.TabIndex = 28;
            this.chkClearTargetTeamListBeforeClone.Text = "Clear Target Teams List Before Clone";
            this.chkClearTargetTeamListBeforeClone.UseVisualStyleBackColor = true;
            this.chkClearTargetTeamListBeforeClone.CheckedChanged += new System.EventHandler(this.chkClearTargetTeamListBeforeClone_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Target User Teams";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "     Target User";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick_1);
            // 
            // lvTargetTeams
            // 
            this.lvTargetTeams.Enabled = false;
            this.lvTargetTeams.HideSelection = false;
            this.lvTargetTeams.Location = new System.Drawing.Point(12, 347);
            this.lvTargetTeams.Name = "lvTargetTeams";
            this.lvTargetTeams.Size = new System.Drawing.Size(548, 213);
            this.lvTargetTeams.TabIndex = 30;
            this.lvTargetTeams.UseCompatibleStateImageBehavior = false;
            this.lvTargetTeams.View = System.Windows.Forms.View.List;
            // 
            // TargetUser
            // 
            this.TargetUser.FormattingEnabled = true;
            this.TargetUser.Location = new System.Drawing.Point(12, 37);
            this.TargetUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TargetUser.Name = "TargetUser";
            this.TargetUser.Size = new System.Drawing.Size(493, 21);
            this.TargetUser.TabIndex = 25;
            this.TargetUser.SelectionChangeCommitted += new System.EventHandler(this.Target_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Target User Roles";
            // 
            // btnOpenTarget
            // 
            this.btnOpenTarget.Location = new System.Drawing.Point(506, 36);
            this.btnOpenTarget.Name = "btnOpenTarget";
            this.btnOpenTarget.Size = new System.Drawing.Size(51, 25);
            this.btnOpenTarget.TabIndex = 27;
            this.btnOpenTarget.Text = "Open";
            this.btnOpenTarget.UseVisualStyleBackColor = true;
            this.btnOpenTarget.Click += new System.EventHandler(this.btnOpenTarget_Click);
            // 
            // lvTargetRoles
            // 
            this.lvTargetRoles.Enabled = false;
            this.lvTargetRoles.HideSelection = false;
            this.lvTargetRoles.Location = new System.Drawing.Point(12, 103);
            this.lvTargetRoles.Name = "lvTargetRoles";
            this.lvTargetRoles.Size = new System.Drawing.Size(548, 213);
            this.lvTargetRoles.TabIndex = 28;
            this.lvTargetRoles.UseCompatibleStateImageBehavior = false;
            this.lvTargetRoles.View = System.Windows.Forms.View.List;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOpenLinksInClassicInterface);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.grpSourceUser);
            this.Controls.Add(this.grpTargetUser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1356, 639);
            this.OnCloseTool += new System.EventHandler(this.MyPluginControl_OnCloseTool);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.grpSourceUser.ResumeLayout(false);
            this.grpSourceUser.PerformLayout();
            this.grpTargetUser.ResumeLayout(false);
            this.grpTargetUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private ToolStripButton tsDoTheSync;
        private CheckBox chkOpenLinksInClassicInterface;
        private ToolStripButton tsbRefreshUserLists;
        private GroupBox grpSourceUser;
        private Label label6;
        private Label label1;
        private ListView lvSourceTeams;
        private ComboBox SourceUser;
        private Label label3;
        private Button btnOpenSource;
        private ListView lvSourceRoles;
        private GroupBox grpTargetUser;
        private Label label5;
        private Label label2;
        private ListView lvTargetTeams;
        private ComboBox TargetUser;
        private Label label4;
        private Button btnOpenTarget;
        private ListView lvTargetRoles;
        private CheckBox chkChangeBusinessUnit;
        private CheckBox chkCloneSecurityRoles;
        private CheckBox chkCloneTeams;
        private CheckBox chkClearTargetTeamListBeforeClone;
        private CheckBox chkClearTargetUserRoleListBeforeClone;
        private CheckBox chkIncludeInactiveUsersInSourceUserList;
        private ComboBox cboBusinessUnit;
        private TreeView tvBusinessUnitSelection;
        private Button btnChangeBusinessUnit;
        private Label label7;
        private ToolStripButton tsbResetToDefaults;
    }
}
