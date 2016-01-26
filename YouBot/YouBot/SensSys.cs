
namespace YouBot
{
    class SensSys : Subsystem
    {
        int clientID;

        int[] sensorHandlers = { -1, -1, -1, -1 };

        public bool[] find = { false, false, false, false };

        Sensor left;
        Sensor right;
        Sensor front;
        Sensor back;


        public SensSys(int id)
        {
            clientID = id;

            for (int i = 0; i < 4; ++i)
            {
                GetHandle(clientID, "Proximity_sensor" + i.ToString(), out sensorHandlers[i]);
            }

            back    = new Sensor(id, sensorHandlers[0]);
            front   = new Sensor(id, sensorHandlers[1]);
            left    = new Sensor(id, sensorHandlers[2]);
            right   = new Sensor(id, sensorHandlers[3]);            
            
        }

        public void Scan()
        {
            ScanRight();
        }

        public void ScanRight()
        {
            right.Scan();

            if (right.detectionState != 0)  find[3] = true;            
            else find[3] = false;
        }

    }
}
