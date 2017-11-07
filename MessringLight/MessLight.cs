using Messring.Floodlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messring;
using MessringLight;


namespace MessringLight
{
    public class MessLight
    {
        CANForFloodLightList canlist = new CANForFloodLightList();

        //IXXAT Controller 1
        CANForFloodLight aLight = new IxxatForFloodLight();

        public void Init()
        {
            if (aLight.Init())
            {
                canlist.canNodeList.Add(aLight);
                FloodLightHardware light = new FloodLightMLight();
                light.canHardware = aLight;
                light.canId = 0x01;
                light.canStrang = 0;
                var ttt =light.cmd_getHello();
                
                Request_FloodLight_OperatingMode a = new Request_FloodLight_OperatingMode();
                a.OperatingMode = OPMODE.FLASH_LIGHT_EXT; //Steady Light
                a.Power_Rate = 5; //3% Power
                a.resetError = true;
                light.cmd_setOperatingMode(a); //power on now
                light.cmd_setLEDPower(0);

                ///
                canlist.Close();

            }

        }




    }
}
