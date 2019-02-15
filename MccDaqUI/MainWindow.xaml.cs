using DevExpress.Xpf.Charts;
using DigitalIO;
using MccDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MccDaqUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MccDaq.MccBoard DaqBoard;
        private MccDaq.Range RangeSelected;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
         int Chan_Out = 0;
        DispatcherTimer timer = new DispatcherTimer();
        int pointCounter=-25;

        int NumPorts, NumBits, FirstBit;
        int PortType, ProgAbility;
        string PortName;

        MccDaq.DigitalPortType PortNum;
        MccDaq.DigitalPortDirection Direction;

        DigitalIO.clsDigitalIO DioProps = new DigitalIO.clsDigitalIO();


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MccDaq.ErrorInfo ULStat;
            float DataValue;
            int Chan;
            VInOptions Options;

            dispatcherTimer.Stop();

            //  Collect the data by calling VIn memeber function of MccBoard object
            //   Parameters:
            //     Chan       :the input channel number
            //     Range      :the Range for the board.
            //     DataValue  :the name for the value collected

            Chan = int.Parse(txtNumChan.Text); //  set input channel
            Options = VInOptions.Default;

            ULStat = DaqBoard.VIn(Chan, RangeSelected, out DataValue, Options);
            if (ULStat.Value == MccDaq.ErrorInfo.ErrorCode.BadRange)
            {
                MessageBox.Show("Change the Range argument to one supported by this board.", "Unsupported Range", 0);
            }
            
            VoltageLable.Content = DataValue.ToString()+" [V]";                //  print the counts

            
            
                double yValue = DataValue;
                SeriesPoint seriesPoint = new SeriesPoint(pointCounter, yValue);
                lineStepSeries2D.Points.Add(seriesPoint);
            
            lineStepSeries2D.Points.EndInit();
            pointCounter++;

            dispatcherTimer.Start();
        }
        public MainWindow()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
            InitializeComponent();

            //  Create a new MccBoard object for Board 0
            DaqBoard = new MccDaq.MccBoard(0);

            cmbRange.Items.Insert(0, MccDaq.Range.Bip10Volts);
            cmbRange.Items.Insert(1, MccDaq.Range.Bip5Volts);
            cmbRange.Items.Insert(2, MccDaq.Range.Uni10Volts);
            cmbRange.Items.Insert(3, MccDaq.Range.Uni5Volts);
            cmbRange.SelectedIndex = 1;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Start();
        }

        void OnTimedEvent(object source, EventArgs e)
        {
           // UpdateData();
        }
        void UpdateData()
        {
           
                
                lineStepSeries2D.Points.BeginInit();
                lineStepSeries2D.Points.Clear();
                for (int i = -25; i < 25; i++)
                {
                    double yValue = Math.Abs(i % 2) * 2 - 1;
                    SeriesPoint seriesPoint = new SeriesPoint(i , yValue);
                    lineStepSeries2D.Points.Add(seriesPoint);
                }
                lineStepSeries2D.Points.EndInit();
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            lineStepSeries2D.Points.BeginInit();
            lineStepSeries2D.Points.Clear();
            pointCounter = 0;
            //StartButton.Visibility = Visibility.Hidden;
            dispatcherTimer.Start();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProgAbility == clsDigitalIO.PROGPORT)
            {
                ushort DataValue = 0;
                MccDaq.ErrorInfo ULStat = DaqBoard.DOut(PortNum, DataValue);

                Direction = MccDaq.DigitalPortDirection.DigitalIn;
                ULStat = DaqBoard.DConfigPort(PortNum, Direction);
            }

            Application.Current.Shutdown();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsValidNumber = true;
            float DataValue = 0.0f;
            short DataValue1 = 0;
            float EngUnits = 0.0f;
            VOutOptions Options = VOutOptions.Default;
            Chan_Out = int.Parse(txtNumChan_Out.Text);

            try
            {
                DataValue = float.Parse(txtVoltsToSet.Text);
                EngUnits = float.Parse(txtVoltsToSet.Text);
            }

            catch (Exception ex)
            {
                
                MessageBoxResult result = MessageBox.Show("is not a valid voltage value", "Invalid Voltage ", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                IsValidNumber = false;
            }

            if (IsValidNumber)
            {
                //  Parameters:
                //    Chan       :the D/A output channel
                //    Range      :ignored if board does not have programmable rage
                //    DataValue  :the value to send to Chan

                //MccDaq.ErrorInfo ULStat = DaqBoard.VOut(Chan_Out, RangeSelected, DataValue, Options);
                MccDaq.ErrorInfo ULStat = DaqBoard.FromEngUnits(RangeSelected, EngUnits, out DataValue1);
                MccDaq.ErrorInfo ULStat1 = DaqBoard.AOut(Chan_Out, RangeSelected, DataValue1);
                lblVoltage.Content = "The voltage at DAC channel " + Chan_Out.ToString("0") + " is:";
                lblShowValue.Content = DataValue.ToString("0.0##") + " Volts";
            }
        }

        private void cmbRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RangeSelected = (MccDaq.Range)(cmbRange.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MccDaq.ErrorInfo ULStat;
            //determine if digital port exists, its capabilities, etc
            PortType = clsDigitalIO.PORTOUT;
            NumPorts = DioProps.FindPortsOfType(DaqBoard, PortType, out ProgAbility,
                out PortNum, out NumBits, out FirstBit);

            if (NumPorts == 0)
            {
                var lblInstructText = "Board " + DaqBoard.BoardNum.ToString() +
                    " has no compatible digital ports.";
                //hsbSetDOutVal.Enabled = false;
                //txtValSet.Enabled = false;
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
                    ULStat = DaqBoard.DConfigPort(PortNum, Direction);
                }
                PortName = PortNum.ToString();
                var lblInstructText = "Set the output value of " + PortName +
                " on board " + DaqBoard.BoardNum.ToString() +
                " using the scroll bar or enter a value in the 'Value set' box.";
                var lblValSetText = "Value set at " + PortName + ":";
                var lblDataValOutText = "Value written to " + PortName + ":";
                //  get a value to write to the port
                ushort DataValue = (ushort)0;
                
                //  write the value to the output port
                //   Parameters:
                //     PortNum    :the output port
                //     DataValue  :the value written to the port

                 ULStat = DaqBoard.DOut(PortNum, DataValue);

                
                    
            }
        }
    }
}
