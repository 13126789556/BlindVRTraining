using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject Arrow;
    public bool isCollected = false;

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

    //force player to stop
    public void stop()
    {
        speed = 0;
    }

    //allow player keep moving
    public void move()
    {
        speed = 1;
    }

    public void resetLocation(Vector3 location)
    {
        location.y = transform.position.y;
        transform.position = location;
    }
}
