using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalController : MonoBehaviour
{
    bool allowTurnLeft, allowGoStraight, allowBeep = false, isFirst = true;
    [SerializeField] private AudioClip beep_1, beep_2;
    private AudioSource ac;

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
    public bool AllowBeep 
    {
        get 
        {
            return allowBeep;
        }
        set 
        {
            allowBeep = value;
            isFirst = !value;
        }
    }

    void Start()
    {
        GetComponent<AudioSource>().Stop();
        beep_1 = Resources.Load<AudioClip>("Audio/Beep_1");
        beep_2 = Resources.Load<AudioClip>("Audio/Beep_2");
        ac = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (AllowBeep && AllowGoStraight)
        {
            ac.clip = beep_1;
            ac.Play();
            isFirst = false;
            allowBeep = false;
        }

        if (!ac.isPlaying && !isFirst)
        {
            ac.clip = beep_2;
            ac.Play();
        }

        if (!AllowGoStraight)
        {
            isFirst = true;
            ac.Stop();
        }
    }
}
