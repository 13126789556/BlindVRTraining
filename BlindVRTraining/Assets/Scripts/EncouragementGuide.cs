using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EncouragementGuide : NetworkBehaviour
{
    public GameObject guideManager;
    public GameObject audioManager;
    public SignalController signal;
    public Vector3 position;
    public bool isInSafeZone = true;
    private float span = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        guideManager = GameObject.Find("GuideManager");
        audioManager = GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (audioManager.GetComponent<AudioSource>().isPlaying)
        {
            guideManager.GetComponent<GuideManager>().span = 0;
        }
        if (guideManager.GetComponent<GuideManager>().span >= span) 
        {
            if (GetComponent<player>().getSpeed() != 0)
            {
                float interval = Random.value;
                if (interval < 0.3f)
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_1);
                }
                else if (interval < 0.6f)
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_2);
                }
                else
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_3);
                }
            }
            else
            {
                //guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_Hint);
            }

            guideManager.GetComponent<GuideManager>().span = 0.0f;
        }

        if (signal!=null&&signal.AllowGoStraight != true && !isInSafeZone)
        {
            GetComponent<player>().resetLocation(position);
            guideManager.GetComponent<GuideManager>().stop();
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Error_HurtByCar);
            isInSafeZone = true;
        }
    }
}
