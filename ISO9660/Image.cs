using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ISO9660
{
    public class Image
    {
        private readonly int _SectorsCount;
        private readonly ImageStream _imgStream;
        private readonly List<VolumeDescriptor> _volumeDescriptors = new List<VolumeDescriptor>();
        private DirectoryRecord _rootDirectory;

        public Image(string path)
        {
            _imgStream = new ImageStream(path, FileMode.Open);
            _SectorsCount = (int)(_imgStream.Length / 2352);// тоже самое как Sectors.Count
            readVolumeDescriptors();
            if (_volumeDescriptors.Count > 1)
            {
                readDirectoryRecord((int)_rootDirectory.ExtentLocation);
            }
            _imgStream.Close();
        }

        public XASectorForm1 this[int index]
        {
            get { return _imgStream.ReadXA1Sector(index); }
        }

        public int NbSectors
        {
            get { return _SectorsCount; }
        }

        public List<VolumeDescriptor> VolumeDescriptors
        {
            get { return _volumeDescriptors; }
        }

        public DirectoryRecord RootDirectory
        {
            get { return _rootDirectory; }
        }


        private void readVolumeDescriptors()
        {
            XASectorForm1 sector;
            VolumeDescriptor vd;
            int sectorId = 16;
            sector = _imgStream.ReadXA1Sector(sectorId);
            vd = new VolumeDescriptor(sector.Data);
            while (vd.VolumeDescriptorType != VolumeDescriptorType.VolumeDescriptionSetTerminator && sectorId < _SectorsCount)
            {
                if (vd.StandardIdentifier == "CD001") //valid volume descriptor
                {
                    switch (vd.VolumeDescriptorType)
                    {
                        case VolumeDescriptorType.PrimaryVolumeDescriptor:
                            PrimaryVolumeDescriptor pvd = new PrimaryVolumeDescriptor(sector.Data);
                            _rootDirectory = pvd.RootDirectoryRecord;
                            _volumeDescriptors.Add(pvd);
                            break;
                        default:
                            break;
                    }
                }
                sectorId++;
                sector = _imgStream.ReadXA1Sector(sectorId);
                vd = new VolumeDescriptor(sector.Data);
            }
            if (vd.StandardIdentifier == "CD001") //valid volume descriptor
                _volumeDescriptors.Add(vd);
        }

        private void readDirectoryRecord(int sectorId)
        {
            PrimaryVolumeDescriptor pvd = (PrimaryVolumeDescriptor)_volumeDescriptors[0];
            XASectorForm1 sector = _imgStream.ReadXA1Sector(sectorId);
            int offset = 0;
            while (sector.Data[offset] != 0)
            {
                int size = sector.Data[offset];
                byte[] data = new byte[size];
                Array.Copy(sector.Data, offset, data, 0, size);
                DirectoryRecord dr = new DirectoryRecord(data);
                if (dr.FileIdentifierLength > 1)
                    dr.FileIdentifier = Encoding.ASCII.GetString(data, 33, dr.FileIdentifierLength - 2);
                else
                    switch (data[33])
                    {
                        case 0:
                            dr.FileIdentifier = ".";
                            break;
                        case 1:
                            dr.FileIdentifier = "..";
                            break;
                        default:
                            dr.FileIdentifier = "";
                            break;
                    }
                _rootDirectory.Children.Add(dr);
                offset += size;
            }
        }
        public Dictionary<string, int> GetSectorStats()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (SectorHeader s in _imgStream.Sectors)
            {
                string subheaderhex = s.SubHeader1.ToString("X8");
                if (stats.ContainsKey(subheaderhex))
                    stats[subheaderhex]++;
                else
                    stats.Add(subheaderhex, 1);
            }
            return stats;
        }

        public void ExtractDirectoryRecord(DirectoryRecord dr, string path)
        {
            int size = (int)dr.DataLength;
            byte[] buffer = new byte[size];
            _imgStream.Read(buffer, (int)dr.ExtentLocation, size);
            FileStream ds = new FileStream(path, FileMode.Create);
            ds.Write(buffer, 0, size);
            ds.Close();
        }

        public void ExtractVideo(DirectoryRecord dr, string path)
        {
            int sectorId = (int)dr.ExtentLocation;
            int filecounter = 0;
            List<byte> bytes = new List<byte>();
            SectorHeader sh = _imgStream.Sectors[sectorId];
            while ((sh.Submode & Submodes.EOF) == 0)
            {
                if ((sh.Submode & Submodes.Audio) == 0)
                {
                    XASectorForm1 s = _imgStream.ReadXA1Sector(sectorId);
                    bytes.AddRange(s.Data);
                    if ((sh.Submode & Submodes.EOR) > 0)
                    {
                        FileStream f = new FileStream(Path.Combine(path, "track" + filecounter.ToString("00") + ".str"), FileMode.Create);
                        f.Write(bytes.ToArray(), 0, bytes.Count);
                        bytes = new List<byte>();
                        filecounter++;
                    }
                }
                sectorId++;
                sh = _imgStream.Sectors[sectorId];
            }
        }

        public void ExtractAudio(DirectoryRecord dr, string outDir)
        {
            int sectorId = (int)dr.ExtentLocation;
            int fileCounter = 0;
            Int32 prev1 = 0, prev2 = 0;
            List<Int16> pcms = new List<Int16>();
            SectorHeader sh = _imgStream.Sectors[sectorId];

            while ((sh.Submode & Submodes.EOF) == 0)
            {
                if ((sh.Submode & Submodes.Audio) > 0)
                {
                    XASectorForm2 xa2Sec = _imgStream.ReadXA2Sector(sectorId);
                    for (int sg = 0; sg < 18; sg++)
                    {
                        byte[] data = new byte[128];
                        Array.Copy(xa2Sec.Data, sg * 128, data, 0, 128);
                        ADPCMBlock block = new ADPCMBlock(data);
                        pcms.AddRange(block.GetPCM(ref prev1, ref prev2));
                    }
                    if ((sh.Submode & Submodes.EOR) > 0)
                    {
                        string filePath = Path.Combine(outDir, $"track{fileCounter:00}.wav");
                        using (FileStream f = new FileStream(filePath, FileMode.Create))
                        using (BinaryWriter bw = new BinaryWriter(f))
                        {
                            bw.Write(Encoding.ASCII.GetBytes("RIFF"));  // "RIFF"
                            bw.Write((Int32)(pcms.Count * 2) + 36);     // size of entire file with 16-bit data
                            bw.Write(Encoding.ASCII.GetBytes("WAVE"));  // "WAVE"
                                                                        // chunk 1:
                            bw.Write(Encoding.ASCII.GetBytes("fmt "));  // "fmt "
                            bw.Write((Int32)16);                        // size of chunk in bytes
                            bw.Write((Int16)1);                         // 1 - for PCM
                            bw.Write((Int16)1);                         // only Stereo files in this version
                            bw.Write((Int32)44100);          // sample rate per second (usually 44100)
                            bw.Write((Int32)(2 * 44100));    // bytes per second (usually 176400)
                            bw.Write((Int16)2);                         // data align 4 bytes (2 bytes sample stereo)
                            bw.Write((Int16)16);                        // only 16-bit in this version
                                                                        // chunk 2:
                            bw.Write(Encoding.ASCII.GetBytes("data"));  // "data"
                            bw.Write((Int32)(pcms.Count * 2));   // size of audio data 16-bit

                            foreach (Int16 pcm in pcms)
                            {
                                bw.Write(pcm);
                            }

                        }

                        pcms.Clear();
                        prev1 = 0;
                        prev2 = 0;
                        fileCounter++;
                    }
                }
                sectorId++;
                sh = _imgStream.Sectors[sectorId];
            }
        }

        public List<List<Int16>> GetAudio()
        {
            var audioFiles = new List<List<Int16>>();
            int fileCounter = 0;
            Int32 prev1 = 0, prev2 = 0;
            List<Int16> pcms = new List<Int16>();

            foreach (DirectoryRecord dr in RootDirectory.Children)
            {
                if (dr.FileIdentifier.EndsWith(".AJS"))
                {
                    int sectorId = (int)dr.ExtentLocation;
                    SectorHeader sh = _imgStream.Sectors[sectorId];

                    //TODO:
                    while ((sh.Submode & Submodes.EOF) == 0)
                    {
                        if ((sh.Submode & Submodes.Audio) > 0)
                        {
                            XASectorForm2 xa2Sec = _imgStream.ReadXA2Sector(sectorId);
                            for (int sg = 0; sg < 18; sg++)
                            {
                                byte[] data = new byte[128];
                                Array.Copy(xa2Sec.Data, sg * 128, data, 0, 128);
                                ADPCMBlock block = new ADPCMBlock(data);
                                pcms.AddRange(block.GetPCM(ref prev1, ref prev2));
                            }
                            if ((sh.Submode & Submodes.EOR) > 0)
                            {
                                audioFiles.Add(pcms);

                                pcms.Clear();
                                prev1 = 0;
                                prev2 = 0;
                                fileCounter++;
                            }
                        }
                        sectorId++;
                        sh = _imgStream.Sectors[sectorId];
                    }
                }
            }

            return audioFiles;
        }
    }
}
