using System;
using System.Runtime.InteropServices;

namespace SCS800API
{
    static class NativeMethods
    {
       
        
        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 InitializeSCS800Communication(Int32 ComPort);

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 TerminateSCS800Communication();

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 GetSCS800Communication();

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 SendSCS800Command();

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xChannel(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 channel, UInt32 gain, UInt32 filterType, UInt32 clockSource, double filterFc);

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xGainFilterTypeReference(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 channel);

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xPacerClock(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 pacerClockDivider);

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xCoupling(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 coupling);
        
        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xOutputMUX(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 boardOutputSwitch, UInt32 boardOutputChannel);
        
        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xReferenceA(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 reference);

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Int32 Program81xReferenceB(UInt32 SCS800Address, UInt32 boardAddress, UInt32 boardType, UInt32 reference);

       
        
    }
}
