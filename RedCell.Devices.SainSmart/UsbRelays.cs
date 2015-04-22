using System;

namespace RedCell.Devices.SainSmart
{
    [Flags]
    public enum UsbRelays : uint
    {
        Unknown = 0x0000,
        K01 = 0x0001,
        K02 = 0x0002,
        K03 = 0x0004,
        K04 = 0x0008,
        K05 = 0x0010,
        K06 = 0x0020,
        K07 = 0x0040,
        K08 = 0x0080,
        K09 = 0x0100,
        K10 = 0x0200,
        K11 = 0x0400,
        K12 = 0x0800,
        K13 = 0x1000,
        K14 = 0x2000,
        K15 = 0x4000,
        K16 = 0x8000,
        All = 0xffff
    }

}
