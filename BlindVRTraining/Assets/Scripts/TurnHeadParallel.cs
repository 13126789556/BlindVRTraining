using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHeadParallel : MonoBehaviour
{
    player _player;
    GameObject _startPosition;
    IEnumerator coroutine;
    bool isTrackingCar;
    int winCondition1, winCondition2, yesCount, noCount;
    static public bool isCarInTrackZone;
    static public bool isCarComing;
    static public Vector3 targetPosition;
    static public int state;

    // Start is call`ed before the first frame update
    void Start(){
        _player = GetComponent<player>();
        _startPosition = GameObject.FindGameObjectWithTag("StartPosition");
        isCarInTrackZone = false;
        winCondition1 = winCondition2 = 0;
        yesCount = noCount = 0;
        state = 0;
        StartCoroutine(checkTracking());
    }

    private void FixedUpdate() {
        //turn head to track the car sound
        //TODO: instruction
        turnHead2TrackSound();

        //TODO: instruction
        //turn left side parallel to the traffic
        comfirmPosition();
    }
    // Update is called once per frame

    public bool isLeftSideParallel()
    {
        Vector2 v1 = new Vector2(_player.getUnitFacingDirection().x, _player.getUnitFacingDirection().z);
        Vector2 v2 = new Vector2 (0f,1f);
        return getAngle(v1,v2) < 15f;
    }

    public float getAngle(Vector2 v1, Vector2 v2){
        //angle = x1x2+y1y2 / sqr(x1*x1 + y1*y1) * sqr(x2*x2 + y2*y2)
        //forward (0,1)
        v1.Normalize();
        v2.Normalize();
        float cosAngle = v1.x * v2.x + v1.y * v2.y / (Mathf.Sqrt(v1.x * v1.x + v1.y * v1.y) * Mathf.Sqrt(v2.x * v2.x + v2.y * v2.y));
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle;
    }
    
    public void comfirmPosition(){
        if(winCondition2<4){
            if(Input.GetButtonDown("Confirm")){
            //check if the player is in the right position
                if(isLeftSideParallel()){
                    //TODO: ADD SOUND
                    print("you got it!");
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.y, Random.Range(-180f, 180f), this.transform.rotation.y);
                    winCondition2 ++;
                }
                else{
                    //TODO: ADD SOUND
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.y, Random.Range(-180f, 180f), this.transform.rotation.y);
                    print("try again");
                }
                print("confirm");
            }
        }
        else{
            print("Pass!");
            //todo: play sound
        }
    }

    public void turnHead2TrackSound(){
        if(winCondition1<4){
            if(isCarComing){   
            print("there is a car coming");
            //if the car enter the tracking zone, check if the player is looking at the car
            }       
        }
        else{
            winCondition1 = 0;
            print(winCondition1);
            print("Pass");
            //todo: play sound
        }

    }

    private IEnumerator checkTracking(){
        yield return new WaitForSeconds(0.5f);
        if(isCarInTrackZone){
            print("Hi");
            Vector2 v1, v2;
            v1 = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
            v2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.y);
            if(getAngle(v1,v2) < 50) {
                print("Yes");
                yesCount ++;
            }else{
                noCount ++;
                print("No");
            }
            if(yesCount>noCount){
                winCondition1++;
            }
            //else isTrackingCar = false;
            //print(getAngle(v1,v2));
        }
        StartCoroutine(checkTracking());
    } 

    public bool isLookingAtCar(){
        //get the position of the car, and the position of the player
        //compare with the player facing direction
        // < 20 degrees, return true else false
        print(targetPosition);
        Vector2 v1, v2;
        v1 = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
        v2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.y);
        print(isLookingAtCar());
        return getAngle(v1,v2) < 20;
    }
}
