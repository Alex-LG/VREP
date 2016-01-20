using System;
using System.Runtime.InteropServices;


namespace YouBot
{
    public enum simx_error
    {
        noerror = 0,
        novalue_flag = 1,
        timeout_flag = 2,
        illegal_opmode_flag = 4,
        remote_error_flag = 8,
        split_progress_flag = 16,
        local_error_flag = 32,
        initialize_error_flag = 64,
    }

    public enum simx_opmode
    {
        oneshot = 0,
        oneshot_wait = 65536,
        streaming = 131072,
        continuous = 131072,
        oneshot_split = 196608,
        continuous_split = 262144,
        streaming_split = 262144,
        discontinue = 327680,
        buffer = 393216,
        remove = 458752,
    }

    class vrepLib
    {
        
        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void simxFinish(int clientID);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int simxGetConnectionId(int clientID);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetAndClearStringSignal(int clientID, string signalName, ref IntPtr pointerToValue, ref int signalLength, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetJointPosition(int clientID, int jointHandle, ref float targetPosition, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetJointPosition(int clientID, int jointHandle, float targetPosition, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetObjectIntParameter(int clientID, int objectHandle, int parameterID, ref int parameterValue, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetObjectFloatParameter(int clientID, int objectHandle, int parameterID, ref float parameterValue, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetObjectOrientation(int clientID, int jointHandle, int relativeToHandle, float[] orientations, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetObjectPosition(int clientID, int jointHandle, int relativeToHandle, float[] positions, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxPauseCommunication(int cliendID, int pause);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static simx_error simxReadProximitySensor(int clientID, int sensorHandle, ref char detectionState, float[] detectionPoint, ref int objectHandle, float[] normalVector, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetJointTargetPosition(int clientID, int jointHandle, float targetPosition, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetJointTargetVelocity(int clientID, int jointHandle, float velocity, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetObjectFloatParameter(int clientID, int objectHandle, int parameterID, float parameterValue, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetObjectIntParameter(int clientID, int objectHandle, int parameterID, int parameterValue, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int simxStart(string ip, int port, bool waitForConnection, bool reconnectOnDisconnect, int timeoutMS, int cycleTimeMS);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetUIEventButton(int clientID, int uiHandle, ref int uiEventButtonID, IntPtr aux, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetUIHandle(int clientID, string uiName, IntPtr p, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetObjectHandle(int clientID, string objectName, out int handle, simx_opmode opmode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int simxGetLastCmdTime(int clientID);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxGetDistanceHandle(int clientID, string distanceObjectName, out int handle, simx_opmode operationMode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxReadDistance(int clientID, int distanceObjectHandle, out float minimumDistance, simx_opmode operationMode);

        [DllImport("remoteApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern simx_error simxSetIntegerSignal(int clientID, string signalName, int signalValue, simx_opmode opmode);
    }
}
