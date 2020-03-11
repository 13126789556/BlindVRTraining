using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    //singleton
    public static IntersectionController _instance;
    public static IntersectionController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<IntersectionController>();
            if (_instance == null)
            {
                var obj = new GameObject();
                obj.AddComponent<IntersectionController>();
                _instance = obj.GetComponent<IntersectionController>();
            }
            return _instance;
        }
    }

    //signal light gameobject
    //GameObject s2N, s2W, w2E, w2N, e2W, e2S, n2S, n2E;

    //Signal Light Object
    public SignalController signalLightNorth;
    public SignalController signalLightSorth;
    public SignalController signalLightWest;
    public SignalController signalLightEast;


    //intersection state
    public enum IntersectionState { State1, State2, State3, State4, State5, State6, State7, State8 }
    public IntersectionState intersectionState = IntersectionState.State1;

    //timer
    float i = 0;
    public float signalInterval = 10;
    void Awake()
    {
        AllRed();
    }

    void Update()
    {
        i += Time.deltaTime;
        switch (intersectionState)
        {
            case IntersectionState.State1:
                AllRed();
                signalLightNorth.AllowGoStraight = signalLightSorth.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State2;
                }
                break;
            case IntersectionState.State2:
                AllRed();
                signalLightNorth.AllowTurnLeft = signalLightSorth.AllowTurnLeft= true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State3;
                }
                break;
            case IntersectionState.State3:
                AllRed();
                signalLightWest.AllowGoStraight = signalLightEast.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State4;
                }
                break;
            case IntersectionState.State4:
                AllRed();
                signalLightWest.AllowTurnLeft = signalLightEast.AllowTurnLeft = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State1;
                }
                break;
            case IntersectionState.State5:
                AllRed();
                signalLightSorth.AllowTurnLeft = signalLightSorth.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State6;
                }
                break;
            case IntersectionState.State6:
                AllRed();
                signalLightNorth.AllowTurnLeft = signalLightNorth.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State7;
                }
                break;
            case IntersectionState.State7:
                AllRed();
                signalLightWest.AllowTurnLeft = signalLightWest.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State8;
                }
                break;
            case IntersectionState.State8:
                AllRed();
                signalLightEast.AllowTurnLeft = signalLightEast.AllowGoStraight = true;
                if (i >= signalInterval)
                {
                    i = 0;
                    intersectionState = IntersectionState.State1;
                }
                break;
        }
    }
    void AllRed()
    {
        signalLightEast.AllowTurnLeft = signalLightEast.AllowGoStraight = false;
        signalLightNorth.AllowTurnLeft = signalLightNorth.AllowGoStraight = false;
        signalLightSorth.AllowTurnLeft = signalLightSorth.AllowGoStraight = false;
        signalLightWest.AllowTurnLeft = signalLightWest.AllowGoStraight = false;
    }
}
