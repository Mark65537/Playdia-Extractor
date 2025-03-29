# Описание

Сначала считывается файл cue. определяется в каком он режиме работает.
Есть 2 режима XAForm1, XAForm2. Которые зависят от значения после Mode2 в строке

```cue
TRACK 01 MODE2/2352
```

и считывается весь файл сначала и до конца, по секторам в функции ReadSectors(). то есть сектор это определенная чать 
файла размер которой определяет режим в cue файле.
первым идет хедер сектора который занимает 24 байта

структура заголовка

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

после того как считались все сектара, считываются Volume Descriptor. В Volume Descriptor считываются сектора XA. Они начинаются с 16 сектора и считываются пока удовлетворит условию.

```cs
VolumeDescriptorType != VolumeDescriptorType.VolumeDescriptionSetTerminator && sectorId < intNbSectors
```

В Volume Descriptor содержится "стандартный идентификатор" если он равен "CD001" то тип Volume Descriptor может быть "Primary Volume Descriptor".

SectorType почему то не используется, я так понимаю она должна влиять на размер сектора. Сейчас размер сектора фиксированный 2352 байта.

SampleRate может быть только "18.9kHz" или "37.8kHz".

после того как считались все сектара, считываются сектора XA. Первый из них это 16 сектор.

1. EDC (Error Detection Code)
Это код обнаружения ошибок
Представлен как 32-битное значение (UInt32)
Используется в обоих типах секторов (Form1 и Form2)
Помогает определить, есть ли ошибки в данных сектора

2. ECCP (Error Correction Code P-parity)
Код коррекции ошибок P-четности
Массив размером 172 байта
Присутствует только в секторах Form1
Первая часть системы коррекции ошибок

3. ECCQ (Error Correction Code Q-parity)
Код коррекции ошибок Q-четности
Массив размером 104 байта
Присутствует только в секторах Form1
Вторая часть системы коррекции ошибок





