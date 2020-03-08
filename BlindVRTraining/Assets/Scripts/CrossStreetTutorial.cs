using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossStreetTutorial : MonoBehaviour
{
    public GameObject guideManager;
    [SerializeField] private bool isUsed_XStreet = false;
    [SerializeField] private bool isUsed_Beep = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsed_XStreet && GetComponent<CrossStreetGuide>().state == CrossStreetGuide.State.Push_To_Walk) 
        {
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Tutorial_XStreet);
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Tutorial_PushButton);
            isUsed_XStreet = true;
        }

        if (!isUsed_Beep && GetComponent<CrossStreetGuide>().Singnals[0].GetComponent<SignalController>().AllowGoStraight)
        {
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Tutorial_Beep);
            isUsed_Beep = true;
        }
    }
}
