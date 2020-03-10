using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHeadParallel : MonoBehaviour
{
    player _player;
    GameObject _startPosition;
    // Start is call`ed before the first frame update
    void Start(){
        _player = GetComponent<player>();
        _startPosition = GameObject.FindGameObjectWithTag("StartPosition");
    }

    private void FixedUpdate() {
        //turn head to track the car sound
        //TODO: instruction

        //TODO: instruction
        //turn left side parallel to the traffic
        comfirmPosition();
    }
    // Update is called once per frame

    public bool isLeftSideParallel()
    {
        return getAngle() < 15f;
    }

    public bool isBackfacing(){
        return Mathf.Abs((getAngle() - 90f)) < 15f;
    }

    public float getAngle(){
        //angle = x1x2+y1y2 / sqr(x1*x1 + y1*y1) * sqr(x2*x2 + y2*y2)
        Vector3 facing = _player.getUnitFacingDirection();
        //forward (0,1)
        float cosAngle = facing.z / Mathf.Sqrt(facing.x * facing.x + facing.z * facing.z);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        print("angle:" + angle);
        return angle;
    }
    
    public void comfirmPosition(){
        if(Input.GetButtonDown("Confirm")){
            //check if the player is in the right position
            if(isLeftSideParallel()){
                //TODO: ADD SOUND
                print("you got it!");
            }
            else{
                //TODO: ADD SOUND
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.y, Random.Range(-180f, 180f), this.transform.rotation.y);
                print("try again");
            }
            print("confirm");
        }
    }

    public void turnHead2TrackSound(){
        //if car in the trigger
    }
}
