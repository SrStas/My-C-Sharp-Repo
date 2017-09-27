using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SCS800API
{
    public class SCS800API
    {
        // Filter Params

        int SCS800Address;  //address of SCS-800 chassis
        int boardAddress;   //slot number
        int filterFc;       //Corner frequency of the filter in Hz
        enum Gain : int     // index into gain level list
        {
            Gain_1 = 0,
            Gain_2 = 1,
            Gain_5 = 3,
            Gain_10 = 4,
            Gain_20 = 5,
            Gain_50 = 6,
            Gain_100 = 7,
            Gain_200 = 8,
            Gain_500 = 9,
            Gain_1000 = 0,
        };


        enum boardType : int
        {
            SCS812 = 12,
            SCS814 = 14,
            SCS815 = 15,
            SCS820 = 20,
        };

        enum chanelNumber : int //channel number, 0 thru 7
        {
            Ch_0 = 0,
            Ch_1 = 1,
            Ch_2 = 2,
            Ch_3 = 3,
            Ch_4 = 4,
            Ch_5 = 5,
            Ch_6 = 6,
            Ch_7 = 7,

        };

        enum filterType : int //filter characteristic
        {
            elliptic = 0,
            linearPhase = 1,
        }

        enum clockSource : int //internal or external clock number
        {
            clock_0 = 0,
            clock_1 = 1,
            clock_2 = 2,
            clock_3 = 3,
            clock_4 = 4,
            clock_5 = 5,
            clock_6 = 6,
            clock_7 = 7,
            external_clock_0 = 8,
            external_clock_1 = 9,
            external_clock_2 = 10,
            external_clock_3 = 11,

        }

        

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]

        public static extern int InitializeSCS800Communication(int ComPort);

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]

        public static extern int TerminateSCS800Communication();

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]

        public static extern int GetSCS800Communication();

        [DllImport("scs800wcom.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]

        public static extern int SendSCS800Command();

        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]

        //int Program81xChannel(UINT32 SCS800Address,UINT32 boardAddress, UINT32 boardType, UINT32 channel, UINT32 gain, UINT32 filterType, UINT32 clockSource, double filterFc)

        // Return Value SUCCESSFUL - programmed correctly ERROR_IMPROPER_RESPONSE - not programmed

        public static extern int Program81xChannel(int chassis_address, int board_address, int board_type, int channel, int gain, int filter_type, int clock_source, double filterFc);

        //int Program81xPacerClock(UINT32 SCS800Address,UINT32 boardAddress,UINT32 boardType,UINT32 pacerClockDivider)
        [DllImport("scs81xw.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Program81xPacerClock(int SCS800Address, int boardAddress, int boardType, int pacerClockDivider);

        public void filterTest()
        {
            try
            {
                

                int bla = InitializeSCS800Communication(3);
                int bla2 = TerminateSCS800Communication();
                int pacerProgRet = Program81xPacerClock(5, 2, (int)boardType.SCS812, 4);
            }
            catch (Exception)
            {

                throw;
            }

        }



      
    }
}
