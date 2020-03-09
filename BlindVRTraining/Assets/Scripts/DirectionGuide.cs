using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGuide : MonoBehaviour
{
    public enum Side { Right, Left, Back };
    public Side sideTag;
    public GameObject guideManager;
    private float span = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            guideManager.GetComponent<GuideManager>().playList.Add(GetAudioIndex());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && guideManager.GetComponent<GuideManager>().span >= span)
        { 
            guideManager.GetComponent<GuideManager>().playList.Add(GetAudioIndex());
        }
    }

    private int GetAudioIndex() 
    {
        int i = 0;
        switch (sideTag)
        {
            case Side.Right: 
                {
                    i = (int)GuideManager.GuideDic._Error_TooRight;
                }
                break;
            case Side.Back:
                {
                    i = (int)GuideManager.GuideDic._Error_Opposite;
                }
                break;
            default:
                {
                    i = (int)GuideManager.GuideDic._Error_TooLeft;
                }
                break;
        }
        return i;
    }
}
