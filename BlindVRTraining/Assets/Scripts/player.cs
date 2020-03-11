using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private GameObject Arrow;
    public bool isCollected = false;
    Rigidbody rb;
    bool footstepPlaying;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (!isServer)
        {
            transform.position = new Vector3(0, 10, -15);
        }
    }

  
    void Update()
    {
        if (!isLocalPlayer) 
        { return; }
        if (isClient)
        {
            return;
        }
        //press A to move forward
        if (Input.GetButton("Submit"))
        {
            Vector3 newDir = getUnitFacingDirection();
            newDir.y = 0;
            //transform.position = transform.position + Time.deltaTime * newDir.normalized * speed;
            rb.velocity = newDir.normalized * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        Arrow.transform.forward = getUnitFacingDirection();

        if (Input.GetButtonDown("Submit") && speed != 0)
        {
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetButtonUp("Submit") || speed == 0)
        {
            GetComponent<AudioSource>().Stop();
        }
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
        speed = 2.0f;
    }
    
    public float getSpeed()
    {
        return rb.velocity == Vector3.zero ? 0 : 1;
    }

    public void resetLocation(Vector3 location)
    {
        location.y = transform.position.y;
        transform.position = location;
    }
    
}
