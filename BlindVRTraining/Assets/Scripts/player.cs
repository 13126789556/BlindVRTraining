using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private GvrEditorEmulator VREmu;
    [SerializeField] private float speed = 1;
    public bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    void Update()
    {
        //press A to move forward
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 newDir = getUnitFacingDirection();
            newDir.y = 0;
            transform.position = transform.position + Time.deltaTime * newDir.normalized * speed; 
        }
    }

    //get unit vector of the facing direction
    public Vector3 getUnitFacingDirection()
    {
        return (VREmu.HeadRotation * Vector3.forward).normalized;
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
