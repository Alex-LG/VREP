
using System;
namespace YouBot
{
    enum SENSOR_DIRECTION
    { 
        NOVALUE,
        BACK,
        FRONT,
        LEFT,
        RIGHT
    };

    class SensSys : Subsystem
    {
        int clientID;

        int[] sensorHandlers = { -1, -1, -1, -1 };

        public bool sensingEnabled = true;

        public SENSOR_DIRECTION ObjectDetected()
        {

            if (back.detectionState == 1) return SENSOR_DIRECTION.BACK;
            if (front.detectionState == 1) return SENSOR_DIRECTION.FRONT;
            if (left.detectionState == 1) return SENSOR_DIRECTION.LEFT;
            if (right.detectionState == 1) return SENSOR_DIRECTION.RIGHT;

             return SENSOR_DIRECTION.NOVALUE;
        }

        Sensor left;
        Sensor right;
        Sensor front;
        public Sensor back;


        public SensSys(int id, string suffix)
        {
            clientID = id;

            for (int i = 0; i < 4; ++i)
            {
                GetHandle(clientID, "Proximity_sensor" + i.ToString() + suffix, out sensorHandlers[i]);
            }

            back    = new Sensor(id, sensorHandlers[0]);
            front   = new Sensor(id, sensorHandlers[1]);
            left    = new Sensor(id, sensorHandlers[2]);
            right   = new Sensor(id, sensorHandlers[3]);            
            
        }

        public void loop()
        {
            while(sensingEnabled)
            {
                back.Scan();
                front.Scan();
                left.Scan();
                right.Scan();                
            }
            
        }


    }
}
