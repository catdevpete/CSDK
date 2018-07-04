using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using NDAPIWrapperSpace;

namespace Demo_Api_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //instance of library
            NDAPI nd = new NDAPI();

            //variable to get result value
            int res = 0;
            
            //connect with service NDSvc
            res = nd.connectToServer();

            if (res == (int)Error.ND_ERROR_SERVICE_UNAVAILABLE)
            {
                Console.WriteLine("Error: Service Unavailable");
            }
            else
            {
                //get number of devices
                int numDevices = nd.getNumberOfDevices();

                if (numDevices < 0)
                {
                    Console.WriteLine("Error: " + numDevices);//you could get the error from Error enum
                }
                else if (numDevices == 0)
                {
                    Console.WriteLine("There is no devices connected\n");
                }
                else //There is at least 1 Gloveone connected
                {
                    int[] devices = new int[numDevices];

                    //get Id's from service
                    res = nd.getDevicesId(devices);

                    if (res >= 0)
                    {
                        Console.WriteLine("There is " + numDevices + " device(s)\n");
                        //get number of Actuators of the first id
                        int numberActuators = nd.getNumberOfActuators(devices[0]);

                        if (res > 0)
                        {
                            Console.WriteLine("Start sending pulses to device with Id: " + devices[0] + "\n");

                            //sending a pulse of 1000 ms to all actuators with value of power: 0.8
                            for (int i = 0; i < numberActuators; i++)
                            {
                                res = nd.setActuatorPulse((Actuator)i, 0.8f, 1000, devices[0]);
                                if (res < 0)
                                {
                                    Console.WriteLine("Error sending pulse to Actuator: " + i);
                                    break;
                                }
                                else Console.WriteLine("Pulse sent to Actuator: " + i);
                                Thread.Sleep(1000);
                            }
                        }
                        else Console.WriteLine("Error getting actuators of device with Id: " + devices[0]);

                    }
                    else Console.WriteLine("Error getting devicesId: " + numDevices);
                }
            }

            Console.WriteLine();

            Console.ReadLine();


        }
    }
}
