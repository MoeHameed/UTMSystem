#include <iostream>
#include "vehicles/multirotor/api/MultirotorRpcLibClient.hpp"
#include <chrono>
#include <thread>

using namespace std;

int main()
{
    MultirotorRpcLibClient client;

	client.simSetObjectScale("1", Vector3r(1, 1, 1));
	Pose p;

    for (float i = 0; i < 5; ++i)
    {
		cout << i << endl;
		p.position = { i, 0, 0 };
		client.simSetObjectPose("1", p);

		std::chrono::duration<int, std::milli> timespan(1000);
		std::this_thread::sleep_for(timespan);
    }


	
	std::cin.get();
}
