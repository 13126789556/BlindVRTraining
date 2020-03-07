using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private GvrEditorEmulator VREmu;
    [SerializeField] private float speed = 1;
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
}
