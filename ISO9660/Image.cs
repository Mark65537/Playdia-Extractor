﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ISO9660
{
    public class Image
    {
        private int intNbSectors;
        private ImageStream fs;
        private List<VolumeDescriptor> volumeDescriptors;
        private DirectoryRecord rootDirectory;

        public Image()
        {
            fs = null;
            intNbSectors = 0;
            volumeDescriptors = new List<VolumeDescriptor>();
        }

        public Image(string path) : this()
        {
            fs = new ImageStream(path, FileMode.Open);
            intNbSectors = (int)(fs.Length / 2352);
            readVolumeDescriptors();
            if(volumeDescriptors.Count > 1)
            {
                readDirectoryRecord((int)rootDirectory.ExtentLocation);
            }
            fs.Close();
        }

        public XASectorForm1 this[int index]
        {
            get { return fs.ReadXA1Sector(index); }
        }

        public int NbSectors
        {
            get { return intNbSectors; }
        }

        public List<VolumeDescriptor> VolumeDescriptors
        {
            get { return volumeDescriptors; }
        }

        public DirectoryRecord RootDirectory
        {
            get { return rootDirectory; }
        }


        private void readVolumeDescriptors()
        {
            XASectorForm1 sector;
            VolumeDescriptor vd;
            int sectorId = 16;
            sector = fs.ReadXA1Sector(sectorId);
            vd = new VolumeDescriptor(sector.Data);
            while(vd.VolumeDescriptorType!= VolumeDescriptorType.VolumeDescriptionSetTerminator && sectorId < intNbSectors)
            {
                if (vd.StandardIdentifier == "CD001") //valid volume descriptor
                {
                    switch (vd.VolumeDescriptorType)
                    {
                        case VolumeDescriptorType.PrimaryVolumeDescriptor:
                            PrimaryVolumeDescriptor pvd = new PrimaryVolumeDescriptor(sector.Data);
                            rootDirectory = pvd.RootDirectoryRecord;
                            volumeDescriptors.Add(pvd);
                            break;
                        default:
                            break;
                    }
                }
                sectorId++;
                sector = fs.ReadXA1Sector(sectorId);
                vd = new VolumeDescriptor(sector.Data);
            }
            if(vd.StandardIdentifier=="CD001") //valid volume descriptor
                volumeDescriptors.Add(vd);
        }

        private void readDirectoryRecord(int sectorId)
        {
            PrimaryVolumeDescriptor pvd = (PrimaryVolumeDescriptor)volumeDescriptors[0];
            XASectorForm1 sector = fs.ReadXA1Sector(sectorId);
            int offset = 0;
            while(sector.Data[offset] !=0)
            {
                int size = sector.Data[offset];
                byte[] data = new byte[size];
                Array.Copy(sector.Data, offset, data, 0, size);
                DirectoryRecord dr = new DirectoryRecord(data);
                if (dr.FileIdentifierLength > 1)
                    dr.FileIdentifier = Encoding.ASCII.GetString(data, 33, dr.FileIdentifierLength -2);
                else
                    switch(data[33])
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
                rootDirectory.Children.Add(dr);
                offset += size;
            }
        }

        private void ExtractDirectoryRecord(DirectoryRecord dr)
        {
            int startSector = 0;
            int bytesRead = 0;
        }
    }
}
