using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartSetting : NetworkManager
{
    public NetworkDiscovery discovery;
    bool isConnected;
    void Start()
    {
#if UNITY_ANDROID
        StartHost();
#elif UNITY_IOS
        StartHost();
#elif Unity_Editor
		discovery.Initialize();
        discovery.StartAsClient();
        //StartClient();
#else
        discovery.Initialize();
        discovery.StartAsClient();
        //StartClient();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            return;
        }
        if (discovery.broadcastsReceived != null)
        {
            foreach (var addr in discovery.broadcastsReceived.Keys)
            {
                print(addr.Remove(0, 7));
                networkAddress = addr;
                StartClient();
            }
        }
    }
    public override void OnStartHost()
    {
        discovery.Initialize();
        discovery.StartAsServer();
    }
    public override void OnStartClient(NetworkClient client)
    {
        //base.OnStartClient(client);
        isConnected = true;
    }

    public override void OnStopClient()
    {
        isConnected = false;
    }
}
