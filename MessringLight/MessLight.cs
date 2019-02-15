using Messring.Floodlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messring;
using MessringLight;
using System.Globalization;

namespace MessringLight
{
    public class MessLight
    {
        CANForFloodLightList canlist = new CANForFloodLightList();

        //IXXAT Controller 1
        CANForFloodLight aLight = new IxxatForFloodLight();
        CANForFloodLight bLight = new IxxatForFloodLight();





        public void Init()
        {
            CANForFloodLightList canlist = new CANForFloodLightList();
            //IXXAT Controller 1 
            CANForFloodLight a;
            a = new IxxatForFloodLight();
            bool b = a.Init();
            if (b)
            {
                canlist.canNodeList.Add(a);
            }
            // IXXAT Controller 2 
            CANForFloodLight c;
            c = new IxxatForFloodLight();
            bool d = c.Init();
            if (d)
            {
                canlist.canNodeList.Add(c);
            }
            canlist.Close();








            //if (aLight.Init())
            //{
            //    canlist.canNodeList.Add(aLight);
            //    FloodLightHardware light = new FloodLightMLight();
            //    light.canHardware = aLight;
            //    Console.WriteLine();
            //    Console.WriteLine("Enter Ligh ID");
            //    var readId = Console.ReadLine();
            //    var conv = int.Parse(readId, NumberStyles.HexNumber);
            //    light.canId = conv;
            //    light.canStrang = 0;
            //    // var ttt =light.cmd_getHello();

            //    Request_FloodLight_OperatingMode a = new Request_FloodLight_OperatingMode();
            //    a.OperatingMode = OPMODE.STEADY_LIGHT; //Steady Light
            //    a.Power_Rate = 50; //3% Power
            //    a.resetError = true;
            //    light.cmd_setOperatingMode(a); //power on now
            //    Console.ReadLine();
            //    light.cmd_setLEDPower(0);
            //    aLight.Close();



            //}

            //if (bLight.Init())
            //{
            //    canlist.canNodeList.Add(bLight);
            //    //FloodLightHardware light = new FloodLightMLight();
            //    //light.canHardware = aLight;
            //    //light.canId = 0x01;
            //    //light.canStrang = 0;
            //    //var ttt = light.cmd_getHello();

            //    //Request_FloodLight_OperatingMode a = new Request_FloodLight_OperatingMode();
            //    //a.OperatingMode = OPMODE.STEADY_LIGHT; //Steady Light
            //    //a.Power_Rate = 50; //3% Power
            //    //a.resetError = true;
            //    //light.cmd_setOperatingMode(a); //power on now
            //    //light.cmd_setLEDPower(0);
            //}
            //FloodLightList lightList = canlist.scan(true);


            //foreach (var item in canlist.canNodeList)
            //{
            //   var ttt = lightList.canNodelist[0];
            //    FloodLightHardware light = new FloodLightMLight();
            //    light.canHardware = item;
            //    light.canId = 0x01;
            //    light.canStrang = 0;
            //    Request_FloodLight_OperatingMode a = new Request_FloodLight_OperatingMode();
            //    a.OperatingMode = OPMODE.STEADY_LIGHT; //Steady Light
            //    a.Power_Rate = 50; //3% Power
            //    a.resetError = true;
            //    light.cmd_setOperatingMode(a); //power on now
            //    Console.ReadKey();
            //    light.cmd_setLEDPower(0);

            //}



            //canlist.Close();

        }




    }
}
