/*
 * Created by SharpDevelop.
 * User: I36107
 * Date: 23/10/2015
 * Time: 16:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ISO9660
{
    /// <summary>
    /// Description of DirectoryRecord.
    /// </summary>
    /// 
    [Flags]
    public enum FileFlags
    {
        Existence = 0x01,
        Directory = 0x02,
        AssociatedFile = 0x04,
        Record = 0x08,
        Protection = 0x10,
        Reserved1 = 0x20,
        Reserved2 = 0x40,
        MultiExtent = 0x80
    }
    [StructLayout(LayoutKind.Sequential, Size = 34, Pack = 1)]
    public struct _DirectoryRecord
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte LengthDR;
        [MarshalAs(UnmanagedType.U1)]
        public byte LengthAR;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ExtentLocation;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ExtentLocationBE;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 DataLength;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 DataLengthBE;
        [MarshalAs(UnmanagedType.Struct)]
        public _ISO9660NumericDate RecordingDate;
        [MarshalAs(UnmanagedType.U1)]
        public byte FileFlags;
        [MarshalAs(UnmanagedType.U1)]
        public byte FileUnitSize;
        [MarshalAs(UnmanagedType.U1)]
        public byte InterleaveGapSize;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 VolumeSequenceNumber;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 VolumeSequenceNumberBE;
        [MarshalAs(UnmanagedType.U1)]
        public byte LengthFI;
        [MarshalAs(UnmanagedType.U1)]
        public byte padding;
    }
    public class DirectoryRecord
    {
        private _DirectoryRecord _dirRec = new _DirectoryRecord();
        private readonly List<DirectoryRecord> _children = new List<DirectoryRecord>();
        private string fi;

        public DirectoryRecord(byte[] data)
        {
            ReadBytes(data);
        }
        public byte Length
        {
            get { return _dirRec.LengthDR; }
            set { _dirRec.LengthDR = value; }
        }
        public byte AttributeLength
        {
            get { return _dirRec.LengthAR; }
            set { _dirRec.LengthAR = value; }
        }
        public UInt32 ExtentLocation
        {
            get { return _dirRec.ExtentLocation; }
            set { _dirRec.ExtentLocation = value; }
        }
        public UInt32 DataLength
        {
            get { return _dirRec.DataLength; }
            set { _dirRec.DataLength = value; }
        }

        public DateTime RecordingDate
        {
            get
            {
                return new DateTime(
                    _dirRec.RecordingDate.year,
                    _dirRec.RecordingDate.month,
                    _dirRec.RecordingDate.day,
                    _dirRec.RecordingDate.hour,
                    _dirRec.RecordingDate.minute,
                    _dirRec.RecordingDate.second
                );
            }
            set
            {
                _dirRec.RecordingDate.year = (byte)value.Year;
                _dirRec.RecordingDate.month = (byte)value.Month;
                _dirRec.RecordingDate.day = (byte)value.Day;
                _dirRec.RecordingDate.hour = (byte)value.Hour;
                _dirRec.RecordingDate.minute = (byte)value.Minute;
                _dirRec.RecordingDate.second = (byte)value.Second;
            }
        }
        public FileFlags Flags
        {
            get { return (FileFlags)_dirRec.FileFlags; }
            set { _dirRec.FileFlags = (byte)value; }
        }
        public byte FileUnitSize
        {
            get { return _dirRec.FileUnitSize; }
            set { _dirRec.FileUnitSize = value; }
        }
        public byte InterleaveGapSize
        {
            get { return _dirRec.InterleaveGapSize; }
            set { _dirRec.InterleaveGapSize = value; }
        }
        public UInt16 VolumeSequenceNumber
        {
            get { return _dirRec.VolumeSequenceNumber; }
            set { _dirRec.VolumeSequenceNumber = value; }
        }
        public byte FileIdentifierLength
        {
            get { return _dirRec.LengthFI; }
            set { _dirRec.LengthFI = value; }
        }
        public string FileIdentifier
        {
            get { return fi; }
            set { fi = value; }
        }
        public List<DirectoryRecord> Children
        {
            get { return _children; }
        }
        public void ReadBytes(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            _dirRec = (_DirectoryRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(_DirectoryRecord));
            handle.Free();
        }
        public byte[] GetBytes()
        {
            int size = Marshal.SizeOf(_dirRec);
            byte[] result = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(_dirRec));
            Marshal.StructureToPtr(_dirRec, ptr, true);
            Marshal.Copy(ptr, result, 0, size);
            Marshal.FreeHGlobal(ptr);
            return result;
        }
    }
}
