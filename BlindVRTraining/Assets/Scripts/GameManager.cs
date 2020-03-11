using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public GameObject car;
    public GameObject guideManager;
    public int maxCarCount = 30;
    IntersectionController _ic;
    float timer;
    static public bool isStart = true;
    static public bool isTrackState = false;
    //NetworkManager networkManager;
    void Start()
    {
        _ic = IntersectionController.Instance;
        guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Intro);
//        networkManager = NetworkManager.singleton;
//#if UNITY_ANDROID
//        networkManager.StartHost();
//#elif UNITY_IOS
//        networkManager.StartHost();
//#elif Unity_Editor
//        networkManager.StartClient();
//#else
//        networkManager.StartClient();
//#endif
    }

    void Update()
    {
        if (!isServer)
        { return; }
        timer -= Time.deltaTime;
        if (isStart)
        {
            if (Input.GetButton("Confirm") || Input.GetButton("Submit"))
            {
                isStart = false;
                guideManager.GetComponent<AudioSource>().Stop();
            }
        }
        else if (isTrackState)
        {
            _ic.intersectionState = IntersectionController.IntersectionState.State1;
            if (timer <= 0 && CarController.carCount <= 2)
            {
                var tempCar = Instantiate(car);
                var tempCarController = tempCar.GetComponent<CarController>();
                if (Random.Range(0, 2) < 1)
                {
                    tempCarController.sourceDir = CarController.Direction.Sorth;
                    tempCarController.distanceFormIntersection = 150;
                }
                else
                {
                    tempCarController.sourceDir = CarController.Direction.North;
                    tempCarController.distanceFormIntersection = 60;
                }
                tempCarController.behavior = CarController.Behavior.GoStraight;
                timer = Random.Range(14, 18);
                NetworkServer.Spawn(tempCar);
            }
        }
        else
        {
            //Time.timeScale = timeMutipler;
            //timer -= Time.deltaTime;
            if (timer <= 0 && CarController.carCount <= maxCarCount)
            {
                var tempCar = Instantiate(car);
                var tempCarController = tempCar.GetComponent<CarController>();
                tempCarController.distanceFormIntersection = 130;
                //tempCarController.behavior = CarController.Behavior.GoStraight;
                //tempCarController.sourceDir = CarController.Direction.East;
                timer = Random.Range(2, 6);
                NetworkServer.Spawn(tempCar);
            }
        }
    }

}
