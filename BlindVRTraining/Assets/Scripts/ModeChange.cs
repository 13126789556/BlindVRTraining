using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ModeChange : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        print(XRSettings.loadedDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMode()
    {
        if (XRSettings.loadedDeviceName == XRSettings.supportedDevices[0])
        {
            XRSettings.LoadDeviceByName(XRSettings.supportedDevices[1]);
        }
        else if(XRSettings.loadedDeviceName == XRSettings.supportedDevices[1])
        {
            XRSettings.LoadDeviceByName(XRSettings.supportedDevices[0]);
        }
        print(XRSettings.loadedDeviceName);
    }
}
