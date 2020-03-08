using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
    }

  
    void Update()
    {
        if (!isLocalPlayer) 
        { return; }
        //press A to move forward
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 newDir = getUnitFacingDirection();
            newDir.y = 0;
            transform.position = transform.position + Time.deltaTime * newDir.normalized * speed; 
        }
        Arrow.transform.forward = getUnitFacingDirection();
    }

    //get unit vector of the facing direction
    public Vector3 getUnitFacingDirection()
    {
        var cam = Camera.main.transform;
        return new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
    }
}
