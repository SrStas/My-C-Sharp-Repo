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

        //address of SCS-800 chassis
        private int chassiAddress;

        public int ChassiAddress
        {
            get { return chassiAddress; }
            set { chassiAddress = value; }
        }


        //slot number
        private int boardAddress;

        public int BoardAddress
        {
            get { return boardAddress; }
            set { boardAddress = value; }
        }



        //Corner frequency of the filter in Hz

        private int filterFc;

        public int FilterFc
        {
            get { return filterFc; }
            set { filterFc = value; }
        }

        public enum Gain : int     // index into gain level list
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


        public enum boardType : int
        {
            SCS812 = 12,
            SCS814 = 14,
            SCS815 = 15,
            SCS816 = 16,
            SCS820 = 20,
        };

        public enum chanelNumber : int //channel number, 0 thru 7
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

        public enum filterType : int //filter characteristic
        {
            elliptic = 0,
            linearPhase = 1,
        }

        public enum clockSource : int //internal or external clock number
        {
            Clock_0 = 0,
            Clock_1 = 1,
            Clock_2 = 2,
            Clock_3 = 3,
            Clock_4 = 4,
            Clock_5 = 5,
            Clock_6 = 6,
            Clock_7 = 7,
            External_clock_0 = 8,
            External_clock_1 = 9,
            External_clock_2 = 10,
            External_clock_3 = 11,

        }

        public enum Coupling : int
        {
            DC_coupled = 0,
            AC_coupled = 1,
        }

        public enum boardOutputSwitch : int
        {
            No_mux = 0,
            SCS_81x_mux_out = 1,
            SCS_804_scope_out = 2,
        }

        public enum Reference : int
        {
            BIPOLAR_ref_minus_2V5 = 0,
            UNIPOLAR_ref_0V = 1,
        }

        /// <summary>
        /// Initializes serial port monitoring, establishes character buffers and installs 
        /// the serial port interrupt handlers.This subroutine must be called once at 
        /// the beginning of the application program.
        /// </summary>
        /// <param name="ComPort">communication port number (0 through 3)</param>
        /// <returns>
        /// 0 = Successfully initialized 
        /// -1 = General 
        /// -2 = Invalid Port 
        /// -3 = In Use 
        /// -4 = Invalid Buffer Size 
        /// -5 = No Memory 
        /// -6 = Not Setup
        /// </returns>
        public int InitializeCommunication(int ComPort)
        {
            try
            {
                return NativeMethods.InitializeSCS800Communication((Int32)ComPort);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public int TerminateCommunication()
        {
            try
            {
                return NativeMethods.TerminateSCS800Communication();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public int GetCommunication()
        {
            try
            {
                return NativeMethods.GetSCS800Communication();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ProgramChannel(UInt32 chassiAddress, UInt32 boardAddress, boardType boardType, UInt32 channel, Gain gain, filterType filterType, clockSource clockSource, double filterFc)
        {
            try
            {
                return NativeMethods.Program81xChannel(chassiAddress, boardAddress, (UInt32)boardType, channel, (UInt32)gain, (UInt32)filterType, (UInt32)clockSource, filterFc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ProgramPacerClock(UInt32 chassiAddress, UInt32 boardAddress, boardType boardType, UInt32 pacerClockDivider)
        {
            return NativeMethods.Program81xPacerClock(chassiAddress, boardAddress, (UInt32)boardType, pacerClockDivider);
        }

        public int ProgramCoupling(UInt32 chassiAddress, UInt32 boardAddress, boardType boardType, UInt32 coupling)
        {
            try
            {
                return NativeMethods.Program81xCoupling(chassiAddress, boardAddress, (UInt32)boardType, coupling);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ProgramReferenceA(uint chassiAddress, uint boardAddress, boardType boardType, uint reference)
        {
            try
            {
                return NativeMethods.Program81xReferenceA(chassiAddress, boardAddress, (UInt32)boardType, reference);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int ProgramReferenceB(uint chassiAddress, uint boardAddress, boardType boardType, uint reference)
        {
            try
            {
                return NativeMethods.Program81xReferenceB(chassiAddress, boardAddress, (UInt32)boardType, reference);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int ProgramGainFilterTypeReference(uint chassiAddress, uint boardAdress, boardType boardType, uint channel)
        {
            try
            {
                int stat = NativeMethods.Program81xGainFilterTypeReference(chassiAddress, boardAdress, (uint)boardType, channel);
                return stat;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int ProgramOutputMUX(uint chassiAddress, uint boardAddress, boardType boardType, uint boardOutputSwitch, uint boardOutputChannel)
        {
            try
            {
                return NativeMethods.Program81xOutputMUX(chassiAddress, boardAddress, (UInt32)boardType, boardOutputSwitch, boardOutputChannel);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
