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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Playdia
{
    public partial class frmReader : Form
    {
        ISO9660.Image _discimg;
        int _sectorPos;

        public frmReader()
        {
            InitializeComponent();
        }

        private void frmReader_Load(object sender, EventArgs e)
        {
            Text += Assembly.GetExecutingAssembly().GetName().Version;
#if DEBUG
            Text += " (DEBUG)";
            LoadISOImage(@"H:\Playdia\games\bandai_playdia_quick_interactive_system\Dragon Ball Z - Shin Saiyajin Zetsumetsu Keikaku - Chikyuu-hen (Japan)\Dragon Ball Z - Shin Saiyajin Zetsumetsu Keikaku - Chikyuu-hen (Japan).cue");
            UpdateSectorStats();
            LoadPlayerTBL();
#endif
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_sectorPos <= 0)
                _sectorPos = _discimg.NbSectors - 1;
            else
                _sectorPos--;
            //RefreshSectorInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_sectorPos >= _discimg.NbSectors - 1)
                _sectorPos = 0;
            else
                _sectorPos++;
            //RefreshSectorInfo();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (oFD_main.ShowDialog() == DialogResult.OK)
            {
                LoadISOImage(oFD_main.FileName);
                UpdateSectorStats();
                LoadPlayerTBL();
            }
        }

        private void LoadPlayerTBL()
        {
            dataGridView1.Rows.Clear();
            var audioFiles = _discimg.GetAudio();

            for (int i = 0; i < audioFiles.Count; i++)
            {

                dataGridView1.Rows.Add(
                    i,
                    false,
                    $"track{i}",
                    "Audio",
                    ""
                //$"{sh.ChannelMode}, {sh.SampleRate}, {sh.BitsPerSample}"
                );
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                string trackName = row.Cells["ColName"].Value.ToString();

                // Создаем временную директорию для аудио файлов
                string tempDir = Path.Combine(Path.GetTempPath(), "PlaydiaAudio");
                Directory.CreateDirectory(tempDir);

                // Извлекаем аудио
                var dr = _discimg.RootDirectory.Children.FirstOrDefault(x => x.FileIdentifier == trackName);
                if (dr != null)
                {
                    _discimg.ExtractAudio(dr, tempDir);

                    // Воспроизводим первый WAV файл из директории
                    var wavFiles = Directory.GetFiles(tempDir, "*.wav");
                    if (wavFiles.Length > 0)
                    {
                        System.Diagnostics.Process.Start(wavFiles[0]);
                    }
                }
            }
        }

        private void LoadISOImage(string filePath)
        {
            TreeNode vdNode = tvSectors.Nodes["nodeVolumeDescriptors"];
            vdNode.Nodes.Clear();
            _discimg = new ISO9660.Image(filePath);
            foreach (VolumeDescriptor vd in _discimg.VolumeDescriptors)
            {
                TreeNode node = vdNode.Nodes.Add(vd.VolumeDescriptorType.ToString());
                node.Tag = vd;
            }
            TreeNode drNode = tvSectors.Nodes["nodeDirectoryRecords"];
            drNode.Tag = _discimg.RootDirectory;
            drNode.Nodes.Clear();
            foreach (DirectoryRecord dr in _discimg.RootDirectory.Children)
            {
                TreeNode node = drNode.Nodes.Add(dr.FileIdentifier);
                node.Tag = dr;
            }
        }

        private void UpdateSectorStats()
        {
            txtSectorStats.Rows.Clear();
            Dictionary<string, int> stats = _discimg.GetSectorStats();

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
                    _discimg.ExtractDirectoryRecord(dr, saveFileDialog1.FileName);
                }
            }
        }

        //TODO: переименовать
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
                    _discimg.ExtractAudio(dr, directory);
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
                    _discimg.ExtractVideo(dr, directory);
                }
            }
        }


    }
}
