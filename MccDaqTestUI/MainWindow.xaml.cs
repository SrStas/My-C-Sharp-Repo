using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DigitalIO;
using MccDaq;

namespace MccDaqTestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        
        int NumPorts, NumBits, FirstBit;
        int PortType, ProgAbility;
        string PortName;
        MccDaq.DigitalPortType PortNum;
        MccDaq.DigitalPortDirection Direction;
        DigitalIO.clsDigitalIO DioProps = new DigitalIO.clsDigitalIO();
        // Create a new MccBoard object for Board 0
        MccDaq.MccBoard DaqBoard;
        MccDaq.ErrorInfo ULStat;

        const short BoardNum = 0; // Board number
        const int ChanCount = 3; // Number of channels in scan
        const int NumPoints = 1000; // Number of data points to collect
        const int NumElements = ChanCount * NumPoints;

        private ushort[] ADData = new ushort[NumElements]; // dimension an array to hold the input values
        private IntPtr MemHandle; // define a variable to contain the handle for
        private short[] ChanArray = new short[ChanCount]; // array to hold channel queue information
        private MccDaq.ChannelType[] ChanTypeArray = new MccDaq.ChannelType[ChanCount]; // array to hold channel type information
        private MccDaq.Range[] GainArray = new MccDaq.Range[ChanCount]; // array to hold gain queue information

        private short UserTerm;

        bool TriggerIsSetToHigh = false;

        public MainWindow()
        {
            InitializeComponent();
            InitUL();
            
            

        }

       
        private void InitDIOs()
        {

            //determine if digital port exists, its capabilities, etc
            PortType = clsDigitalIO.PORTOUT;
            NumPorts = DioProps.FindPortsOfType(DaqBoard, PortType, out ProgAbility, out PortNum, out NumBits, out FirstBit);

            if (NumPorts == 0)
            {
                var lblInstructText = "Board " + DaqBoard.BoardNum.ToString() +
                    " has no compatible digital ports.";

            }
            else
            {
                // if programmable, set direction of port to output
                // configure the first port for digital output
                //  Parameters:
                //    PortNum        :the output port
                //    Direction      :sets the port for input or output

                if (ProgAbility == clsDigitalIO.PROGPORT)
                {
                    Direction = MccDaq.DigitalPortDirection.DigitalOut;
                    //ULStat = DaqBoard.DConfigPort(PortNum, Direction);
                    ULStat = DaqBoard.DConfigPort(PortNum, Direction);
                }
            }
        }

        private void ArmDaqButton_Click(object sender, RoutedEventArgs e)
        {
           // ULStat = DaqBoard.APretrig();
        }

        private void SetTriggerButton_Click(object sender, RoutedEventArgs e)
        {
           


        }


        private void InitUL()
        {
            //			short i;
           

            // Initiate error handling
            //  activating error handling will trap errors like
            //  bad channel numbers and non-configured conditions.
            //  Parameters:
            //    MccDaq.ErrorReporting.PrintAll :all warnings and errors encountered will be printed
            //    MccDaq.ErrorHandling.StopAll   :if any error is encountered, the program will stop

            ULStat = MccDaq.MccService.ErrHandling(MccDaq.ErrorReporting.PrintAll, MccDaq.ErrorHandling.StopAll);

            MemHandle = MccDaq.MccService.WinBufAllocEx(NumElements); // set aside memory to hold data
            if (MemHandle == IntPtr.Zero)
                Application.Current.Shutdown();

            DaqBoard = new MccDaq.MccBoard(0);

        }

       



    }
}

