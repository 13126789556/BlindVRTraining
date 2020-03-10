using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFacingDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setRandomPosition(){
        transform.rotation = Quaternion.Euler(this.transform.rotation.y, Random.Range(-180f, 180f), this.transform.rotation.y);
    }
}
