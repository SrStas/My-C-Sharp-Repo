using System;
namespace MainExecutor
{
    class Program
    {
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            //var t = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                
            
            //MessLight light = new MessLight();
            //light.Init();

           // MccDaqHrdWr.MccDaqHrdWr Daq = new MccDaqHrdWr.MccDaqHrdWr();
            //Daq.Init();
            //Daq.CmdStartConvert();
            //Daq.CmdStopConvert();

            //SupportedDevicesCatalog catalog = new SupportedDevicesCatalog();
            //var t = catalog.getSpecificClass("scs814");

            SCS800API.SCS800API Filter = new SCS800API.SCS800API();
            Console.Write("Enter Chassi Port #:");
            string portNum = Console.ReadLine();
            Console.Write("Enter Chassi Address:");
            string chassiNum = Console.ReadLine();
            Console.Write("Enter Board Address:");
            string boardNum = Console.ReadLine();
            Console.Write("Enter Chan Num:");
            string chanNum = Console.ReadLine();
            Console.Write("Enter Gain:1, 2, 5, 10, def: ");
            string gain = Console.ReadLine();
            int filterInitReturnValue = Filter.InitializeCommunication(Convert.ToInt32(int.Parse(portNum))-1);
            //int filterInitReturnValue = Filter.InitializeCommunication(3);
            Console.WriteLine("Flter Returns on InitializeSCS800Communication() - " + filterInitReturnValue);
            int programChannelStatus = 0;
            int stat1 = Filter.ProgramCoupling(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, 0);
            stat1 = Filter.ProgramReferenceA(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, 0);
            Console.WriteLine("ProgramReferenceA() Status: " + stat1);
            stat1 = Filter.ProgramReferenceB(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, 0);
            Console.WriteLine("ProgramReferenceB() Status: " + stat1);
            switch (gain)
            {
                case "1":
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 1 on channel " + chanNum);
                     programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, Convert.ToUInt32(int.Parse(chanNum)), SCS800API.SCS800API.Gain.Gain_1, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_3, 1000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                case "2":
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 2 on channel " + chanNum);
                     programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, Convert.ToUInt32(int.Parse(chanNum)), SCS800API.SCS800API.Gain.Gain_2, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_3, 1000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                case "5":
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 5 on channel " + chanNum);
                    programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, Convert.ToUInt32(int.Parse(chanNum)), SCS800API.SCS800API.Gain.Gain_5, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_3, 1000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                case "10":
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 10 on channel " + chanNum);
                    programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, Convert.ToUInt32(int.Parse(chanNum)), SCS800API.SCS800API.Gain.Gain_10, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_3, 1000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                case "1000":
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 10 on channel " + chanNum);
                    programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(int.Parse(chassiNum)), Convert.ToUInt32(int.Parse(boardNum)), SCS800API.SCS800API.boardType.SCS814, Convert.ToUInt32(int.Parse(chanNum)), SCS800API.SCS800API.Gain.Gain_1000, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_3, 1000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                default:
                    Console.WriteLine("Chassi Address:" + chassiNum + " Board Address:" + boardNum + " filter model:SCS814 Setting Gain = 2 on channel " + chanNum);
                    programChannelStatus = Filter.ProgramChannel(0, 0, SCS800API.SCS800API.boardType.SCS814, 2, SCS800API.SCS800API.Gain.Gain_2, SCS800API.SCS800API.filterType.butterworth, SCS800API.SCS800API.clockSource.Clock_0, 3000);
                    Console.WriteLine("Program Ch Status: " + programChannelStatus);
                    break;
                    
            }
            
           // int getComunicationStatus = Filter.GetCommunication();
            //Console.WriteLine("Comunication Status: " + getComunicationStatus);

            //int stat1 = Filter.ProgramGainFilterTypeReference(0, 0, SCS800API.SCS800API.boardType.SCS816, 2);
            //Console.WriteLine("ProgramGainFilterTypeReference() Status: " + stat1);
            //stat1 = Filter.ProgramPacerClock(0, 0, SCS800API.SCS800API.boardType.SCS816, 2);
            //Console.WriteLine("ProgramPacerClock() Status: " + stat1);
            //stat1 = Filter.ProgramCoupling(0, 0, SCS800API.SCS800API.boardType.SCS816, 0);
            //Console.WriteLine("ProgramCoupling() Status: " + stat1);
            //stat1 = Filter.ProgramReferenceA(0, 0, SCS800API.SCS800API.boardType.SCS816, 1);

            //Console.WriteLine("ProgramReferenceA() Status: " + stat1);
            //stat1 = Filter.ProgramReferenceB(0, 0, SCS800API.SCS800API.boardType.SCS814, 1);
            //Console.WriteLine("ProgramReferenceB() Status: " + stat1);
            //stat1 = Filter.ProgramOutputMUX(0, 0, SCS800API.SCS800API.boardType.SCS816, 1,1);
            //Console.WriteLine("ProgramOutputMUX() Status: " + stat1);



            //Console.WriteLine("Get Channel params: Gain:"+gain+" FilterType:"+filterType+" clockSource:"+clockSource+" Fc"+fc);

            //stat = Filter.SetCoupling(Convert.ToUInt32(portNum), Convert.ToUInt32(boardNum), SCS800API.SCS800API.boardType.SCS814, 1);
            //Console.WriteLine("Program Coupling to 1: " + stat);
            //Int32 coupling = 99;
            //programChannelStatus = Filter.GetCoupling(Convert.ToUInt32(portNum), Convert.ToUInt32(boardNum), SCS800API.SCS800API.boardType.SCS814, out coupling);
            //Console.WriteLine("Coupling is: " + coupling);
            //Console.WriteLine("Read Coupling Status: " + programChannelStatus);
            filterInitReturnValue = Filter.TerminateCommunication();
            Console.WriteLine("Flter Returns on TerminateSCS800Communication() " + filterInitReturnValue);

             Console.ReadKey();
            }
        }
    }
}
