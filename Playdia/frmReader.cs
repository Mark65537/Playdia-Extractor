/*
 * Created by SharpDevelop.
 * User: I36107
 * Date: 20/10/2015
 * Time: 13:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using ISO9660;
using PlaydiaControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Playdia
{
    public partial class frmReader : Form
    {
        ISO9660.Image discimg;
        int sectorPos;
        public frmReader()
        {
            InitializeComponent();
        }

        private void frmReader_Load(object sender, EventArgs e)
        {
            sectorPos = 0;
            discimg = null;
            Text += Assembly.GetExecutingAssembly().GetName().Version;
#if DEBUG
            Text += " (DEBUG)";
            LoadISOImage(@"H:\Playdia\games\bandai_playdia_quick_interactive_system\Dragon Ball Z - Shin Saiyajin Zetsumetsu Keikaku - Chikyuu-hen (Japan)\Dragon Ball Z - Shin Saiyajin Zetsumetsu Keikaku - Chikyuu-hen (Japan).cue");
#endif
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (sectorPos <= 0)
                sectorPos = discimg.NbSectors - 1;
            else
                sectorPos--;
            //RefreshSectorInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (sectorPos >= discimg.NbSectors - 1)
                sectorPos = 0;
            else
                sectorPos++;
            //RefreshSectorInfo();
        }

        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadISOImage();
            }
        }

        /// <summary>
        /// Загрузить ISO image из openFileDialog1
        /// </summary>
        private void LoadISOImage()
        {
            LoadISOImage(openFileDialog1.FileName);
        }

        private void LoadISOImage(string filePath)
        {
            TreeNode vdNode = tvSectors.Nodes["nodeVolumeDescriptors"];
            vdNode.Nodes.Clear();
            discimg = new ISO9660.Image(filePath);
            foreach (VolumeDescriptor vd in discimg.VolumeDescriptors)
            {
                TreeNode node = vdNode.Nodes.Add(vd.VolumeDescriptorType.ToString());
                node.Tag = vd;
            }
            TreeNode drNode = tvSectors.Nodes["nodeDirectoryRecords"];
            drNode.Tag = discimg.RootDirectory;
            drNode.Nodes.Clear();
            foreach (DirectoryRecord dr in discimg.RootDirectory.Children)
            {
                TreeNode node = drNode.Nodes.Add(dr.FileIdentifier);
                node.Tag = dr;
            }
            UpdateSectorStats();
        }

        private void UpdateSectorStats()
        {
            txtSectorStats.Rows.Clear();
            Dictionary<string, int> stats = discimg.GetSectorStats();

            foreach (KeyValuePair<string, int> stat in stats)
            {
                txtSectorStats.Rows.Add(stat.Key, stat.Value);
            }
            txtSectorStats.Sort(ColSubheader, System.ComponentModel.ListSortDirection.Ascending);
        }

        private void tvSectors_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.Name == "nodeVolumeDescriptors")
            {
                this.pnlPrimaryVolumeDescriptor.Controls.Clear();
                VolumeDescriptor vd = (VolumeDescriptor)e.Node.Tag;
                VolumeDescriptorControl pvdctl = new VolumeDescriptorControl(vd);
                this.pnlPrimaryVolumeDescriptor.Controls.Add(pvdctl);
            }
            else if (e.Node.Parent != null && e.Node.Parent.Name == "nodeDirectoryRecords")
            {
                this.pnlDirectoryRecord.Controls.Clear();
                DirectoryRecord dr = (DirectoryRecord)e.Node.Tag;
                DirectoryRecordControl drctl = new DirectoryRecordControl(dr);
                this.pnlDirectoryRecord.Controls.Add(drctl);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tvSectors.SelectedNode;
            if (node != null && node.Parent.Name == "nodeDirectoryRecords")
            {
                DirectoryRecord dr = (DirectoryRecord)node.Tag;
                saveFileDialog1.FileName = dr.FileIdentifier;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    discimg.ExtractDirectoryRecord(dr, saveFileDialog1.FileName);
                }
            }
        }

        private void sectorStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateSectorStats();
        }

        private void extractAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tvSectors.SelectedNode;
            if (node != null && node.Parent.Name == "nodeDirectoryRecords")
            {
                DirectoryRecord dr = (DirectoryRecord)node.Tag;
                saveFileDialog1.FileName = "track";
                saveFileDialog1.Filter = "WAV files (*.wav)|*.wav";
                saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string directory = Path.GetDirectoryName(saveFileDialog1.FileName);
                    discimg.ExtractAudio(dr, directory);
                }
            }
        }

        private void extractVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tvSectors.SelectedNode;
            if (node != null && node.Parent.Name == "nodeDirectoryRecords")
            {
                DirectoryRecord dr = (DirectoryRecord)node.Tag;
                saveFileDialog1.FileName = "video";
                saveFileDialog1.Filter = "STR files (*.str)|*.str";
                saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string directory = Path.GetDirectoryName(saveFileDialog1.FileName);
                    discimg.ExtractVideo(dr, directory);
                }
            }
        }


    }
}
