using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGuide : MonoBehaviour
{
    public GameObject guideManager;
    public GameObject crossStreet;
    public GameObject InnerZone;
    public GameObject OuterZone;
    private float span = 3.0f;
    private float duration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        crossStreet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!guideManager.GetComponent<AudioSource>().isPlaying)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            duration = span;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<player>().stop();
        duration = 0.0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (duration <= 0.0f)
            {
                if (!other.gameObject.GetComponent<player>().isCollected)
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Tutorial_Collect);
                }
                else
                {
                    InnerZone.GetComponent<DirectionGuide>().sideTag = DirectionGuide.Side.Right;
                    OuterZone.GetComponent<DirectionGuide>().sideTag = DirectionGuide.Side.Left;
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Tutorial_TurnBack);
                    crossStreet.SetActive(true);
                    this.enabled = false;
                }
                duration = span;
            }

            if (Input.GetButton("Confirm")) 
            {
                guideManager.GetComponent<GuideManager>().stop();
                other.gameObject.GetComponent<player>().isCollected = true;
                duration = 0.0f;
            }
        }
    }
}
