# ��������

������� ����������� ���� cue. ������������ � ����� �� ������ ��������.
���� 2 ������ XAForm1, XAForm2. ������� ������� �� �������� ����� Mode2 � ������

```cue
TRACK 01 MODE2/2352
```

� ����������� ���� ���� ������� � �� �����, �� �������� � ������� ReadSectors(). �� ���� ������ ��� ������������ ���� 
����� ������ ������� ���������� ����� � cue �����.
������ ���� ����� ������� ������� �������� 24 �����

��������� ���������

```cs
[StructLayout(LayoutKind.Sequential, Size=24,Pack=1)]
struct _SectorHeader
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=12)]
    public string syncPattern;
    [MarshalAs(UnmanagedType.U1)]
    public byte minute;
    [MarshalAs(UnmanagedType.U1)]
    public byte second;
    [MarshalAs(UnmanagedType.U1)]
    public byte block;
    [MarshalAs(UnmanagedType.U1)]
    public byte mode;
    [MarshalAs(UnmanagedType.ByValArray,SizeConst=2)]
    public UInt32[] subheader;
}
```

����� ���� ��� ��������� ��� �������, ����������� Volume Descriptor. � Volume Descriptor ����������� ������� XA. ��� ���������� � 16 ������� � ����������� ���� ������������ �������.

```cs
VolumeDescriptorType != VolumeDescriptorType.VolumeDescriptionSetTerminator && sectorId < intNbSectors
```

� Volume Descriptor ���������� "����������� �������������" ���� �� ����� "CD001" �� ��� Volume Descriptor ����� ���� "Primary Volume Descriptor".

SectorType ������ �� �� ������������, � ��� ������� ��� ������ ������ �� ������ �������. ������ ������ ������� ������������� 2352 �����.

SampleRate ����� ���� ������ "18.9kHz" ��� "37.8kHz".

����� ���� ��� ��������� ��� �������, ����������� ������� XA. ������ �� ��� ��� 16 ������.

1. EDC (Error Detection Code)
��� ��� ����������� ������
����������� ��� 32-������ �������� (UInt32)
������������ � ����� ����� �������� (Form1 � Form2)
�������� ����������, ���� �� ������ � ������ �������

2. ECCP (Error Correction Code P-parity)
��� ��������� ������ P-��������
������ �������� 172 �����
������������ ������ � �������� Form1
������ ����� ������� ��������� ������

3. ECCQ (Error Correction Code Q-parity)
��� ��������� ������ Q-��������
������ �������� 104 �����
������������ ������ � �������� Form1
������ ����� ������� ��������� ������


���� ����� ���� ������ � ������� ��������� ��� �����




