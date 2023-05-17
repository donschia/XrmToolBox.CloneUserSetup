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
            this.tsDoTheSync = new System.Windows.Forms.ToolStripButton();
            this.chkOpenLinksInUCI = new System.Windows.Forms.CheckBox();
            this.tsbRefreshUserLists = new System.Windows.Forms.ToolStripButton();
            this.grpSourceUser = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lvSourceTeams = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.lvSourceRoles = new System.Windows.Forms.ListView();
            this.btnOpenSource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Source = new System.Windows.Forms.ComboBox();
            this.grpTargetUser = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lvTargetTeams = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.lvTargetRoles = new System.Windows.Forms.ListView();
            this.btnOpenTarget = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Target = new System.Windows.Forms.ComboBox();
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
            this.tsbRefreshUserLists,
            this.tsDoTheSync});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1122, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(131, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsDoTheSync
            // 
            this.tsDoTheSync.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsDoTheSync.Image = ((System.Drawing.Image)(resources.GetObject("tsDoTheSync.Image")));
            this.tsDoTheSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDoTheSync.Name = "tsDoTheSync";
            this.tsDoTheSync.Size = new System.Drawing.Size(363, 28);
            this.tsDoTheSync.Text = "COPY Business Unit, Security Roles, and Teams";
            this.tsDoTheSync.Click += new System.EventHandler(this.tsDoTheSync_Click);
            // 
            // chkOpenLinksInUCI
            // 
            this.chkOpenLinksInUCI.AutoSize = true;
            this.chkOpenLinksInUCI.Location = new System.Drawing.Point(28, 31);
            this.chkOpenLinksInUCI.Name = "chkOpenLinksInUCI";
            this.chkOpenLinksInUCI.Size = new System.Drawing.Size(202, 21);
            this.chkOpenLinksInUCI.TabIndex = 14;
            this.chkOpenLinksInUCI.Text = "Open Links in UCI Interface";
            this.chkOpenLinksInUCI.UseVisualStyleBackColor = true;
            // 
            // tsbRefreshUserLists
            // 
            this.tsbRefreshUserLists.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshUserLists.Image")));
            this.tsbRefreshUserLists.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshUserLists.Name = "tsbRefreshUserLists";
            this.tsbRefreshUserLists.Size = new System.Drawing.Size(125, 28);
            this.tsbRefreshUserLists.Text = "Refresh Users";
            this.tsbRefreshUserLists.Click += new System.EventHandler(this.tsbRefreshUserLists_Click);
            // 
            // grpSourceUser
            // 
            this.grpSourceUser.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grpSourceUser.Controls.Add(this.label6);
            this.grpSourceUser.Controls.Add(this.label1);
            this.grpSourceUser.Controls.Add(this.lvSourceTeams);
            this.grpSourceUser.Controls.Add(this.Source);
            this.grpSourceUser.Controls.Add(this.label3);
            this.grpSourceUser.Controls.Add(this.btnOpenSource);
            this.grpSourceUser.Controls.Add(this.lvSourceRoles);
            this.grpSourceUser.Location = new System.Drawing.Point(0, 58);
            this.grpSourceUser.Name = "grpSourceUser";
            this.grpSourceUser.Size = new System.Drawing.Size(510, 581);
            this.grpSourceUser.TabIndex = 23;
            this.grpSourceUser.TabStop = false;
            this.grpSourceUser.Text = "SOURCE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "Source User Teams";
            // 
            // lvSourceTeams
            // 
            this.lvSourceTeams.Location = new System.Drawing.Point(16, 347);
            this.lvSourceTeams.Name = "lvSourceTeams";
            this.lvSourceTeams.Size = new System.Drawing.Size(480, 213);
            this.lvSourceTeams.TabIndex = 29;
            this.lvSourceTeams.UseCompatibleStateImageBehavior = false;
            this.lvSourceTeams.View = System.Windows.Forms.View.List;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Source User Roles";
            // 
            // lvSourceRoles
            // 
            this.lvSourceRoles.Location = new System.Drawing.Point(16, 104);
            this.lvSourceRoles.Name = "lvSourceRoles";
            this.lvSourceRoles.Size = new System.Drawing.Size(480, 213);
            this.lvSourceRoles.TabIndex = 27;
            this.lvSourceRoles.UseCompatibleStateImageBehavior = false;
            this.lvSourceRoles.View = System.Windows.Forms.View.List;
            // 
            // btnOpenSource
            // 
            this.btnOpenSource.Location = new System.Drawing.Point(442, 43);
            this.btnOpenSource.Name = "btnOpenSource";
            this.btnOpenSource.Size = new System.Drawing.Size(54, 25);
            this.btnOpenSource.TabIndex = 26;
            this.btnOpenSource.Text = "Open";
            this.btnOpenSource.UseVisualStyleBackColor = true;
            this.btnOpenSource.Click += new System.EventHandler(this.btnOpenSource_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "     Source User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Source
            // 
            this.Source.FormattingEnabled = true;
            this.Source.Location = new System.Drawing.Point(16, 43);
            this.Source.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(420, 24);
            this.Source.TabIndex = 24;
            this.Source.SelectionChangeCommitted += new System.EventHandler(this.Source_SelectionChangeCommitted);
            // 
            // grpTargetUser
            // 
            this.grpTargetUser.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpTargetUser.Controls.Add(this.label5);
            this.grpTargetUser.Controls.Add(this.label2);
            this.grpTargetUser.Controls.Add(this.lvTargetTeams);
            this.grpTargetUser.Controls.Add(this.Target);
            this.grpTargetUser.Controls.Add(this.label4);
            this.grpTargetUser.Controls.Add(this.btnOpenTarget);
            this.grpTargetUser.Controls.Add(this.lvTargetRoles);
            this.grpTargetUser.Location = new System.Drawing.Point(529, 59);
            this.grpTargetUser.Name = "grpTargetUser";
            this.grpTargetUser.Size = new System.Drawing.Size(578, 580);
            this.grpTargetUser.TabIndex = 24;
            this.grpTargetUser.TabStop = false;
            this.grpTargetUser.Text = "TARGET";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Target User Teams";
            // 
            // lvTargetTeams
            // 
            this.lvTargetTeams.Location = new System.Drawing.Point(12, 347);
            this.lvTargetTeams.Name = "lvTargetTeams";
            this.lvTargetTeams.Size = new System.Drawing.Size(548, 213);
            this.lvTargetTeams.TabIndex = 30;
            this.lvTargetTeams.UseCompatibleStateImageBehavior = false;
            this.lvTargetTeams.View = System.Windows.Forms.View.List;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "Target User Roles";
            // 
            // lvTargetRoles
            // 
            this.lvTargetRoles.Location = new System.Drawing.Point(12, 103);
            this.lvTargetRoles.Name = "lvTargetRoles";
            this.lvTargetRoles.Size = new System.Drawing.Size(548, 213);
            this.lvTargetRoles.TabIndex = 28;
            this.lvTargetRoles.UseCompatibleStateImageBehavior = false;
            this.lvTargetRoles.View = System.Windows.Forms.View.List;
            // 
            // btnOpenTarget
            // 
            this.btnOpenTarget.Location = new System.Drawing.Point(509, 42);
            this.btnOpenTarget.Name = "btnOpenTarget";
            this.btnOpenTarget.Size = new System.Drawing.Size(51, 25);
            this.btnOpenTarget.TabIndex = 27;
            this.btnOpenTarget.Text = "Open";
            this.btnOpenTarget.UseVisualStyleBackColor = true;
            this.btnOpenTarget.Click += new System.EventHandler(this.btnOpenTarget_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "     Target User";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Target
            // 
            this.Target.FormattingEnabled = true;
            this.Target.Location = new System.Drawing.Point(12, 43);
            this.Target.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(493, 24);
            this.Target.TabIndex = 25;
            this.Target.SelectionChangeCommitted += new System.EventHandler(this.Target_SelectionChangeCommitted);
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOpenLinksInUCI);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.grpSourceUser);
            this.Controls.Add(this.grpTargetUser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1122, 639);
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
        private CheckBox chkOpenLinksInUCI;
        private ToolStripButton tsbRefreshUserLists;
        private GroupBox grpSourceUser;
        private Label label6;
        private Label label1;
        private ListView lvSourceTeams;
        private ComboBox Source;
        private Label label3;
        private Button btnOpenSource;
        private ListView lvSourceRoles;
        private GroupBox grpTargetUser;
        private Label label5;
        private Label label2;
        private ListView lvTargetTeams;
        private ComboBox Target;
        private Label label4;
        private Button btnOpenTarget;
        private ListView lvTargetRoles;
    }
}
