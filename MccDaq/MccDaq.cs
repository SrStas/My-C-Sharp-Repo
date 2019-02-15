using AnalogIO;
using ErrorDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;


namespace MccDaqHrdWr
{
    public class MccDaqHrdWr
    {
        // Create a new MccBoard object for Board 0
        MccDaq.MccBoard DaqBoard = new MccDaq.MccBoard(0);
        MccDaq.Range Range;
        private int HighChan, NumAIChans;
        private int ADResolution;
        AnalogIO.clsAnalogIO AIOProps = new AnalogIO.clsAnalogIO();
        public Timer tmrConvert;

        public MccDaqHrdWr()
        {
            tmrConvert.Tick += new System.EventHandler(TmrConvert_Tick);
        }

        public void Init()
        {
            int LowChan;
            MccDaq.TriggerType DefaultTrig;

            InitUL();

            // determine the number of analog channels and their capabilities
            int ChannelType = clsAnalogIO.ANALOGINPUT;
            NumAIChans = AIOProps.FindAnalogChansOfType(DaqBoard, ChannelType,
                out ADResolution, out Range, out LowChan, out DefaultTrig);

            if (NumAIChans == 0)
            {
                var lblInstructionText = "Board " + DaqBoard.BoardNum.ToString() + " does not have analog input channels.";
                var cmdStartConvertEnabled = false;
                var txtNumChanEnabled = false;
            }
            else
            {
                string CurFunc = "MccBoard.AIn";
                if (ADResolution > 16)
                    CurFunc = "MccBoard.AIn32";
                var lblDemoFunctionText = "Demonstration of " + CurFunc;
                var lblInstructionText = "Board " + DaqBoard.BoardNum.ToString() + " collecting analog data using " + CurFunc + " and Range of " + Range.ToString() + ".";
                HighChan = LowChan + NumAIChans - 1;
                var lblChanPromptText = "Enter a channel (" + LowChan.ToString() + " - " + HighChan.ToString() + "):";
            }
        }

        public void CmdStopConvert()
        {

            tmrConvert.Enabled = false;
            Application.Exit();

        }

        public void CmdStartConvert()
        {

            if (tmrConvert.Enabled)
            {
                var cmdStartConvertText = "Start";
                tmrConvert.Enabled = false;
            }
            else
            {
                var cmdStartConvertText = "Stop";
                tmrConvert.Enabled = true;
            }

        }
        private void TmrConvert_Tick(object eventSender, System.EventArgs eventArgs)
        {

            float EngUnits;
            double HighResEngUnits;
            MccDaq.ErrorInfo ULStat;
            System.UInt16 DataValue;
            System.UInt32 DataValue32;
            int Chan;
            int Options = 0;

            tmrConvert.Stop();

            //  Collect the data by calling AIn member function of MccBoard object
            //   Parameters:
            //     Chan       :the input channel number
            //     Range      :the Range for the board.
            //     DataValue  :the name for the value collected

            //  set input channel
            var txtNumChanText = "1";
            bool ValidChan = int.TryParse(txtNumChanText, out Chan);
            if (ValidChan)
            {
                if (Chan > HighChan) Chan = HighChan;
                txtNumChanText = Chan.ToString();
            }

            if (ADResolution > 16)
            {
                ULStat = DaqBoard.AIn32(Chan, Range, out DataValue32, Options);
                //  Convert raw data to Volts by calling ToEngUnits
                //  (member function of MccBoard class)
                ULStat = DaqBoard.ToEngUnits32(Range, DataValue32, out HighResEngUnits);

               var lblShowDataText = DataValue32.ToString();                //  print the counts
                var lblShowVoltsText = HighResEngUnits.ToString("F5") + " Volts"; //  print the voltage
            }
            else
            {
                ULStat = DaqBoard.AIn(Chan, Range, out DataValue);

                //  Convert raw data to Volts by calling ToEngUnits
                //  (member function of MccBoard class)
                ULStat = DaqBoard.ToEngUnits(Range, DataValue, out EngUnits);

                var lblShowDataText = DataValue.ToString();                //  print the counts
                var lblShowVoltsText = EngUnits.ToString("F4") + " Volts"; //  print the voltage
            }

            tmrConvert.Start();
        }


        private void InitUL()
        {

            //  Initiate error handling
            //   activating error handling will trap errors like
            //   bad channel numbers and non-configured conditions.
            //   Parameters:
            //     MccDaq.ErrorReporting.PrintAll :all warnings and errors encountered will be printed
            //     MccDaq.ErrorHandling.StopAll   :if an error is encountered, the program will stop

            clsErrorDefs.ReportError = MccDaq.ErrorReporting.PrintAll;
            clsErrorDefs.HandleError = MccDaq.ErrorHandling.StopAll;
            MccDaq.ErrorInfo ULStat = MccDaq.MccService.ErrHandling
                (clsErrorDefs.ReportError, clsErrorDefs.HandleError);

        }
    }

}
