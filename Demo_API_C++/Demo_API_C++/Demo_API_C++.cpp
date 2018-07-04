// Demo_API_C++.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "NDAPI.h"
#include <iostream>
using namespace std;

int main()
{
	//instance of library
	NDAPISpace::NDAPI nd;

	//variable to get result value
	int res = 0;

	//connect with service NDSvc
	res = nd.connectToServer();

	if (res == NDAPISpace::ND_ERROR_SERVICE_UNAVAILABLE) {
		cout << "Error: Service Unavailable" << endl;
	}
	else {

		//get number of devices
		int numDevices = nd.getNumberOfDevices();

		if (numDevices < 0) {
			cout << "Error: " << numDevices << endl; //you could get the error from NDAPISpace error enum
		}
		else if (numDevices == 0) {
			cout << "There is no devices connected " << endl;
		}
		else {//There is at least 1 device connected

			int *devices = new int[numDevices];

			//get Id's from service
			res = nd.getDevicesId(devices, numDevices);

			if (res >= 0)
			{
				cout << "There is " << numDevices << " device(s)" << endl << endl;
				//get number of Actuators of the first id
				int numberActuators = nd.getNumberOfActuators(devices[0]);

				if (res > 0) {
					cout << "Start sending pulses to device with Id: " << devices[0] << endl << endl;

					//sending a pulse of 1000 ms to all actuators with value of power: 0.8
					for (int i = 0;i < numberActuators;i++) {

						res = nd.setActuatorPulse(static_cast<NDAPISpace::Actuator>(i), 0.1f+0.1*i, 1000, devices[0]);

						if (res < 0) {
							cout << "Error sending pulse to Actuator: " << i << endl;
							break;
						}
						else cout << "Pulse sent to actuator: " << i << " successfully" << endl;
					}
				}
				else cout << "Error getting actuators of device with Id: " << devices[0] << endl;

			}
			else cout << "Error getting devicesId: " << numDevices << endl;

			delete devices;
		}
	}
	cout << endl;
	cin.get();
	return 0;
}

