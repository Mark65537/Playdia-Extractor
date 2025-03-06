/*
 * Created by SharpDevelop.
 * User: I36107
 * Date: 20/10/2015
 * Time: 13:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Playdia
{
    partial class frmReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("VolumeDescriptors");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DirectoryRecords");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReader));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectorStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSectors = new System.Windows.Forms.Panel();
            this.txtSectorStats = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvSectors = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDirectoryRecord = new System.Windows.Forms.Panel();
            this.pnlPrimaryVolumeDescriptor = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tC_Extractor = new System.Windows.Forms.TabControl();
            this.tP_Extractor = new System.Windows.Forms.TabPage();
            this.tP_Player = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlSectors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tC_Extractor.SuspendLayout();
            this.tP_Extractor.SuspendLayout();
            this.tP_Player.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Cue sheets (*.cue)|*.cue";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(951, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.extractAudioToolStripMenuItem,
            this.extractVideoToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.sectorStatsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // extractAudioToolStripMenuItem
            // 
            this.extractAudioToolStripMenuItem.Name = "extractAudioToolStripMenuItem";
            this.extractAudioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extractAudioToolStripMenuItem.Text = "Extract &Audio";
            this.extractAudioToolStripMenuItem.Click += new System.EventHandler(this.extractAudioToolStripMenuItem_Click);
            // 
            // extractVideoToolStripMenuItem
            // 
            this.extractVideoToolStripMenuItem.Name = "extractVideoToolStripMenuItem";
            this.extractVideoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extractVideoToolStripMenuItem.Text = "Extract &Video";
            this.extractVideoToolStripMenuItem.Click += new System.EventHandler(this.extractVideoToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "&Save as..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // sectorStatsToolStripMenuItem
            // 
            this.sectorStatsToolStripMenuItem.Name = "sectorStatsToolStripMenuItem";
            this.sectorStatsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sectorStatsToolStripMenuItem.Text = "Sect&or Stats";
            this.sectorStatsToolStripMenuItem.Click += new System.EventHandler(this.sectorStatsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(19, 20);
            this.toolStripMenuItem1.Text = "&";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlSectors, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 601);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // pnlSectors
            // 
            this.pnlSectors.Controls.Add(this.txtSectorStats);
            this.pnlSectors.Location = new System.Drawing.Point(3, 404);
            this.pnlSectors.Name = "pnlSectors";
            this.pnlSectors.Size = new System.Drawing.Size(930, 194);
            this.pnlSectors.TabIndex = 0;
            // 
            // txtSectorStats
            // 
            this.txtSectorStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSectorStats.Location = new System.Drawing.Point(0, 0);
            this.txtSectorStats.Margin = new System.Windows.Forms.Padding(4);
            this.txtSectorStats.Multiline = true;
            this.txtSectorStats.Name = "txtSectorStats";
            this.txtSectorStats.Size = new System.Drawing.Size(930, 194);
            this.txtSectorStats.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvSectors);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(931, 395);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvSectors
            // 
            this.tvSectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSectors.Location = new System.Drawing.Point(0, 0);
            this.tvSectors.Name = "tvSectors";
            treeNode1.Name = "nodeVolumeDescriptors";
            treeNode1.Text = "VolumeDescriptors";
            treeNode2.Name = "nodeDirectoryRecords";
            treeNode2.Text = "DirectoryRecords";
            this.tvSectors.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.tvSectors.Size = new System.Drawing.Size(193, 395);
            this.tvSectors.TabIndex = 0;
            this.tvSectors.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSectors_AfterSelect);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlDirectoryRecord, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlPrimaryVolumeDescriptor, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(734, 395);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pnlDirectoryRecord
            // 
            this.pnlDirectoryRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDirectoryRecord.Location = new System.Drawing.Point(3, 3);
            this.pnlDirectoryRecord.Name = "pnlDirectoryRecord";
            this.pnlDirectoryRecord.Size = new System.Drawing.Size(728, 134);
            this.pnlDirectoryRecord.TabIndex = 0;
            // 
            // pnlPrimaryVolumeDescriptor
            // 
            this.pnlPrimaryVolumeDescriptor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrimaryVolumeDescriptor.Location = new System.Drawing.Point(3, 143);
            this.pnlPrimaryVolumeDescriptor.Name = "pnlPrimaryVolumeDescriptor";
            this.pnlPrimaryVolumeDescriptor.Size = new System.Drawing.Size(728, 249);
            this.pnlPrimaryVolumeDescriptor.TabIndex = 1;
            // 
            // tC_Extractor
            // 
            this.tC_Extractor.Controls.Add(this.tP_Extractor);
            this.tC_Extractor.Controls.Add(this.tP_Player);
            this.tC_Extractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tC_Extractor.Location = new System.Drawing.Point(0, 24);
            this.tC_Extractor.Name = "tC_Extractor";
            this.tC_Extractor.SelectedIndex = 0;
            this.tC_Extractor.Size = new System.Drawing.Size(951, 633);
            this.tC_Extractor.TabIndex = 1;
            // 
            // tP_Extractor
            // 
            this.tP_Extractor.Controls.Add(this.tableLayoutPanel1);
            this.tP_Extractor.Location = new System.Drawing.Point(4, 22);
            this.tP_Extractor.Name = "tP_Extractor";
            this.tP_Extractor.Padding = new System.Windows.Forms.Padding(3);
            this.tP_Extractor.Size = new System.Drawing.Size(943, 607);
            this.tP_Extractor.TabIndex = 0;
            this.tP_Extractor.Text = "Extractor";
            this.tP_Extractor.UseVisualStyleBackColor = true;
            // 
            // tP_Player
            // 
            this.tP_Player.Controls.Add(this.tableLayoutPanel3);
            this.tP_Player.Location = new System.Drawing.Point(4, 22);
            this.tP_Player.Name = "tP_Player";
            this.tP_Player.Padding = new System.Windows.Forms.Padding(3);
            this.tP_Player.Size = new System.Drawing.Size(961, 558);
            this.tP_Player.TabIndex = 1;
            this.tP_Player.Text = "Player";
            this.tP_Player.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.59483F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.40517F));
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(955, 552);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // frmReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 657);
            this.Controls.Add(this.tC_Extractor);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReader";
            this.Text = "Playdia Extractor ";
            this.Load += new System.EventHandler(this.frmReader_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlSectors.ResumeLayout(false);
            this.pnlSectors.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tC_Extractor.ResumeLayout(false);
            this.tP_Extractor.ResumeLayout(false);
            this.tP_Player.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlSectors;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvSectors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlDirectoryRecord;
        private System.Windows.Forms.Panel pnlPrimaryVolumeDescriptor;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem sectorStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox txtSectorStats;
        private System.Windows.Forms.ToolStripMenuItem extractAudioToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem extractVideoToolStripMenuItem;
        private System.Windows.Forms.TabControl tC_Extractor;
        private System.Windows.Forms.TabPage tP_Extractor;
        private System.Windows.Forms.TabPage tP_Player;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
