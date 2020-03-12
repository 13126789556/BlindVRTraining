using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHeadParallel : MonoBehaviour
{
    player _player;
    GameObject _startPosition;
    IEnumerator coroutine;
    bool isTrackingCar;
    //bool playsOUN;
    int winCondition1, winCondition2, yesCount, noCount;
    GameObject guideManager;
    GameManager gameManager;
    //AudioManager audioManager;
    bool isPlayed1, isPlayed2;
    float lastTime, repeatTime;
    AudioManager audioManager;
    public AudioClip[] audios;
    static public bool isCarInTrackZone;
    static public bool isCarComing;
    static public Vector3 targetPosition;
    static public int state;
    // Start is call`ed before the first frame update
    void Start()
    {
        isPlayed1 = isPlayed2 = false;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        guideManager = GameObject.Find("GuideManager");
        _player = GetComponent<player>();
        _startPosition = GameObject.FindGameObjectWithTag("StartPosition");
        isCarInTrackZone = false;
        //isplayed1 = isPlayed2 = false;
        winCondition1 = winCondition2 = 0;
        yesCount = noCount = 0;
        repeatTime = 0.3f;
        state = 0;
        //StartCoroutine(checkTracking());
    }

    private void Update()
    {
        if (GameManager.isStart == false)
        {
            switch (state)
            {
                case 0:
                    _player.stop();
                    //turn head to track the car sound
                    if (!isPlayed1)
                    {
                        AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                        am.flag = true;
                        am.playAudio(audios[0]);
                        isPlayed1 = true;
                        //audioManager.isPlayed = true;
                    }
                    //guideManager.GetComponent<GuideManager>().playOnce(21);
                    //else isplayed = false;
                    turnHead2TrackSound();
                    break;
                case 1:
                    //StopAllCoroutines();
                    //print("Stopped " + Time.time);
                    _player.stop();
                    //turn left side parallel to the traffic
                    //if(!isplayed)
                    if (!isPlayed2)
                    {
                        AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                        am.flag = true;
                        am.playAudio(audios[5]);
                        isPlayed2 = true;
                        //audioManager.isPlayed = true;
                    }

                    //guideManager.GetComponent<GuideManager>().playOnce(26);
                    comfirmPosition();
                    break;
            }
        }
    }
    public bool isLeftSideParallel()
    {
        Vector2 v1 = new Vector2(_player.getUnitFacingDirection().x, _player.getUnitFacingDirection().z);
        Vector2 v2 = new Vector2(0f, 1f);
        return getAngle(v1, v2) < 30f;
    }

    public float getAngle(Vector2 v1, Vector2 v2)
    {
        //angle = x1x2+y1y2 / sqr(x1*x1 + y1*y1) * sqr(x2*x2 + y2*y2)
        //forward (0,1)
        v1.Normalize();
        v2.Normalize();
        float cosAngle = v1.x * v2.x + v1.y * v2.y / (Mathf.Sqrt(v1.x * v1.x + v1.y * v1.y) * Mathf.Sqrt(v2.x * v2.x + v2.y * v2.y));
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle;
    }

    public void comfirmPosition()
    {
        if (winCondition2 < 4)
        {
            if (Input.GetButtonDown("Confirm"))
            {
                //check if the player is in the right position
                if (isLeftSideParallel())
                {
                    //TODO: ADD SOUND
                    audioManager.flag = true;
                    audioManager.playAudio(audios[10]);
                    //guideManager.GetComponent<GuideManager>().playOnce(Random.Range(17, 19));
                    //print("you got it!");
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, Random.Range(-180f, 180f), this.transform.rotation.z);
                    ++winCondition2;
                }
                else
                {
                    //TODO: ADD SOUND
                    audioManager.flag = true;
                    audioManager.playAudio(audios[8]);
                    //guideManager.GetComponent<GuideManager>().playOnce(30);
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, Random.Range(-180f, 180f), this.transform.rotation.z);
                    //print("try again");
                }
                //print("confirm");
            }
        }
        else
        {
            //print("Pass!");
            //todo: play sound
            audioManager.flag = true;
            audioManager.playAudio(audios[6]);
            //guideManager.GetComponent<GuideManager>().playOnce(27);
            state = 3;
            _player.move();
        }
    }

    public void turnHead2TrackSound()
    {
        if (winCondition1 < 3)
        {
            if (isCarComing) //play prepare sound
            {
                //print("there is a car coming");
                if (targetPosition.x > 0 && audioManager.isPlayed == false)
                { //coming from left
                    //todo: play sound
                    AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    am.flag = true;
                    //audioManager.flag = true;
                    am.playAudio(audios[1]);
                    am.isPlayed = true;
                }
                else if (targetPosition.x < 0)
                { //coming from right
                  //print("there is a car coming right");
                    AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    am.flag = true;
                    am.playAudio(audios[2]);
                    am.isPlayed = true;
                }
                //if the car enter the tracking zone, check if the player is looking at the car
            }
            if (isCarInTrackZone)
            {
                lastTime = Time.deltaTime;
                if (Time.deltaTime - lastTime < repeatTime)
                {
                    Vector2 v1, v2;
                    v1 = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
                    v2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.y);
                    if (getAngle(v1, v2) < 50)
                    {
                        yesCount++;
                    }
                    else
                    {
                        noCount++;
                    }
                }
            }
            else
            {
                if (yesCount > noCount)
                {
                    winCondition1++;
                    AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    am.flag = true;
                    //print("play good job");
                    am.isPlayed = false;
                    //print("am isPlayed: " + am.isPlayed);
                    am.playAudio(audios[9]);
                    //am.isPlayed = true;
                    yesCount = noCount = 0;
                }
                else if (yesCount != 0 || noCount != 0)
                {
                    AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    am.flag = true;
                    am.isPlayed = false;
                    //print("am isPlayed: " + am.isPlayed);
                    am.playAudio(audios[7]);
                    //am.isPlayed = true;
                    //guideManager.GetComponent<GuideManager>().playList.Add(29);
                    yesCount = noCount = 0;
                }
            }
        }
        else
        {
            winCondition1 = 0;
            //todo: play sound
            AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            am.flag = true;
            am.playAudio(audios[5]);
            //am.isPlayed = true;
            GameManager.isTrackState = false;
            state = 1;
        }

    }

    // private IEnumerator checkTracking()
    // {
    //     yield return new WaitForSeconds(0.3f);
    //     if (isCarInTrackZone && state == 0)
    //     {
    //         Vector2 v1, v2;
    //         v1 = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
    //         v2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.y);
    //         if (getAngle(v1, v2) < 50)
    //         {
    //             yesCount++;
    //         }
    //         else
    //         {
    //             noCount++;
    //         }
    //     }
    //     else if (!isCarInTrackZone && state == 0)
    //     { // the target car is out of the track zone
    //         //compare these count
    //         if (yesCount > noCount)
    //         {
    //             winCondition1++;
    //             audioManager.flag = true;
    //             audioManager.playAudio(audios[9]);
    //             yesCount = noCount = 0;
    //         }
    //         else if (yesCount != 0 || noCount != 0)
    //         {
    //             audioManager.flag = true;
    //             audioManager.playAudio(audios[7]);
    //             //guideManager.GetComponent<GuideManager>().playList.Add(29);
    //             yesCount = noCount = 0;
    //         }
    //     }
    //     yield return StartCoroutine(checkTracking());
    // }

    public bool isLookingAtCar()
    {
        //get the position of the car, and the position of the player
        //compare with the player facing direction
        // < 20 degrees, return true else false
        print(targetPosition);
        Vector2 v1, v2;
        v1 = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
        v2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.y);
        //print(isLookingAtCar());
        return getAngle(v1, v2) < 40;
    }
}
