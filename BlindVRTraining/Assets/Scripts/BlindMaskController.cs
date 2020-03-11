using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BlindMaskController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
