using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncouragementGuide : MonoBehaviour
{
    public GameObject guideManager;
    private float span = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        guideManager = GameObject.Find("GuideManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (guideManager.GetComponent<GuideManager>().span >= span) 
        {
            float interval = Random.value * 3;
            if (interval < 1.0f)
            {
                guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_1);
            }
            else if (interval < 2.0f)
            {
                guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_2);
            }
            else
            {
                guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Encouragement_3);
            }
            guideManager.GetComponent<GuideManager>().span = 0.0f;
        }
    }
}
