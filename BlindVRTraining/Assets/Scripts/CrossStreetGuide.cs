﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossStreetGuide : MonoBehaviour
{
    public enum State { Guide_Direction, Push_To_Walk, Wait };

    public GameObject guideManager;

    public Vector3[] directions;
    private Vector3 direction;
    public GameObject[] Singnals;
    private SignalController sc;

    public State state = State.Wait;
    private static float span = 5.0f;
    private bool istriggered = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !istriggered)
        {
            state = State.Guide_Direction;
            other.gameObject.GetComponent<player>().stop();
            other.gameObject.GetComponent<EncouragementGuide>().isInSafeZone = true;

            if (!other.gameObject.GetComponent<player>().isCollected)
            {
                direction = directions[0];
                sc = Singnals[0].GetComponent<SignalController>();
            }
            else
            {
                direction = directions[1];
                sc = Singnals[1].GetComponent<SignalController>();
            }

            //determine which direction to turn
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._XStreet_Direction);
            Vector3 playerFaceDir = other.gameObject.GetComponent<player>().getUnitFacingDirection();
            float dot = Vector3.Dot(direction, playerFaceDir);

            if (dot >= 0.7f)
            {
                guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Direction_Front);
            }
            else if (dot <= -0.7f)
            {
                guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Direction_Back);
            }
            else
            {
                Vector3 cross = Vector3.Cross(playerFaceDir, direction);
                if (cross.y > 0)
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Direction_Right);
                }
                else if (cross.y < 0)
                {
                    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Direction_Left);
                }
            }

            istriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (state)
            {
                case State.Guide_Direction:
                    {
                        Vector3 playerFaceDir = other.gameObject.GetComponent<player>().getUnitFacingDirection();
                        float dot = Vector3.Dot(direction, playerFaceDir);
                        int index = -1;
                        if (dot >= 0.7f)
                        {
                            state = State.Push_To_Walk;
                            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Xstreet_PushButton);
                            break;
                        }
                        else if (dot <= -0.7f)
                        {
                            index = (int)GuideManager.GuideDic._Direction_Back;
                        }
                        else
                        {
                            Vector3 cross = Vector3.Cross(playerFaceDir, direction);
                            if (cross.y > 0)
                            {
                                index = (int)GuideManager.GuideDic._Direction_Right;
                            }
                            else if (cross.y < 0)
                            {
                                index = (int)GuideManager.GuideDic._Direction_Left;
                            }
                        }
                        if (index != -1 && guideManager.GetComponent<GuideManager>().span >= span)
                        {
                            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._XStreet_Direction);
                            guideManager.GetComponent<GuideManager>().playList.Add(index);
                        }
                    }
                    break;
                case State.Push_To_Walk:
                    {
                        if (Input.GetButton("Confirm"))
                        {
                            state = State.Wait;
                            sc.AllowBeep = true;
                            other.gameObject.GetComponent<player>().move();
                            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Xstreet_Wait);
                        }
                    }
                    break;
                case State.Wait:
                    {
                        //if (guideManager.GetComponent<GuideManager>().span >= span && !sc.AllowGoStraight)
                        //{
                        //    guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Xstreet_Wait);
                        //}
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        istriggered = false;
        other.gameObject.GetComponent<EncouragementGuide>().position = transform.position;
        other.gameObject.GetComponent<EncouragementGuide>().signal = sc;
        other.gameObject.GetComponent<EncouragementGuide>().isInSafeZone = false;
        if (!sc.AllowGoStraight)
        {
            guideManager.GetComponent<GuideManager>().stop();
            guideManager.GetComponent<GuideManager>().playList.Add((int)GuideManager.GuideDic._Error_HurtByCar);
            other.gameObject.GetComponent<player>().resetLocation(transform.position);
        }
    }
}
