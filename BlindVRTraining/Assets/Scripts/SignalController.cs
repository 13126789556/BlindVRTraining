using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalController : MonoBehaviour
{
    bool allowTurnLeft, allowGoStraight;
    public bool AllowTurnLeft
    {
        get
        {
            return allowTurnLeft;
        }
        set
        {
            allowTurnLeft = value;
            transform.Find("TurnLeft/Green").gameObject.SetActive(allowTurnLeft);
            transform.Find("TurnLeft/Red").gameObject.SetActive(!allowTurnLeft);
        }
    }
    public bool AllowGoStraight
    {
        get
        {
            return allowGoStraight;
        }
        set
        {
            allowGoStraight = value;
            transform.Find("GoStraight/Green").gameObject.SetActive(allowGoStraight);
            transform.Find("GoStraight/Red").gameObject.SetActive(!allowGoStraight);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
