using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scs81x;
using Microsys.Hardware.Catalog;
using MessringLight;

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

            MessLight light = new MessLight();
            light.Init();

            //SupportedDevicesCatalog catalog = new SupportedDevicesCatalog();
            //var t = catalog.getSpecificClass("scs814");

            //SCS800API.SCS800API Filter = new SCS800API.SCS800API();
            ////Console.Write("Enter Chassi Port #:");
            ////string portNum = Console.ReadLine();
            ////Console.Write("Enter Board #:");
            ////string boardNum = Console.ReadLine();
            //int filterInitReturnValue = Filter.InitializeCommunication(Convert.ToInt32(0));
            //Console.WriteLine("Flter Returns on InitializeSCS800Communication() - " + filterInitReturnValue);
            //int getComunicationStatus = Filter.GetCommunication();
            ////Console.WriteLine("Comunication Status: " + getComunicationStatus);

            //int programChannelStatus = Filter.ProgramChannel(Convert.ToUInt32(0), Convert.ToUInt32(0), SCS800API.SCS800API.boardType.SCS814, 1, SCS800API.SCS800API.Gain.Gain_10, SCS800API.SCS800API.filterType.elliptic, SCS800API.SCS800API.clockSource.Clock_0, 3000);
            //Console.WriteLine("Program Ch Status: " + programChannelStatus);

            //int stat1 = Filter.ProgramGainFilterTypeReference(0, 0, SCS800API.SCS800API.boardType.SCS814, 2);
            //Console.WriteLine("ProgramGainFilterTypeReference() Status: " + stat1);
            //stat1 = Filter.ProgramPacerClock(0, 0, SCS800API.SCS800API.boardType.SCS814, 2);
            //Console.WriteLine("ProgramPacerClock() Status: " + stat1);
            //stat1 = Filter.ProgramCoupling(0, 0, SCS800API.SCS800API.boardType.SCS814, 0);
            //Console.WriteLine("ProgramCoupling() Status: " + stat1);
            //stat1 = Filter.ProgramReferenceA(0, 0, SCS800API.SCS800API.boardType.SCS814, 1);
            //Console.WriteLine("ProgramReferenceA() Status: " + stat1);
            //stat1 = Filter.ProgramReferenceB(0, 0, SCS800API.SCS800API.boardType.SCS814, 1);
            //Console.WriteLine("ProgramReferenceB() Status: " + stat1);
            //stat1 = Filter.ProgramOutputMUX(0, 0, SCS800API.SCS800API.boardType.SCS814, 1,1);
            //Console.WriteLine("ProgramOutputMUX() Status: " + stat1);



            //Console.WriteLine("Get Channel params: Gain:"+gain+" FilterType:"+filterType+" clockSource:"+clockSource+" Fc"+fc);

            //stat = Filter.SetCoupling(Convert.ToUInt32(portNum), Convert.ToUInt32(boardNum), SCS800API.SCS800API.boardType.SCS814, 1);
            //Console.WriteLine("Program Coupling to 1: " + stat);
            //Int32 coupling = 99;
            //programChannelStatus = Filter.GetCoupling(Convert.ToUInt32(portNum), Convert.ToUInt32(boardNum), SCS800API.SCS800API.boardType.SCS814, out coupling);
            //Console.WriteLine("Coupling is: " + coupling);
            //Console.WriteLine("Read Coupling Status: " + programChannelStatus);
            //filterInitReturnValue = Filter.TerminateCommunication();
            //Console.WriteLine("Flter Returns on TerminateSCS800Communication()" + filterInitReturnValue);

             Console.ReadKey();
        }
    }
}
