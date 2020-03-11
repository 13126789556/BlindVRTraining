using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuideManager : NetworkBehaviour
{
    public enum GuideDic
    {
        _Direction_Front = 0,
        _Direction_Back = 1,
        _Direction_Left = 2,
        _Direction_Right = 3,
        _XStreet_Direction = 4,
        _Xstreet_PushButton = 5,
        _Xstreet_Wait = 6,
        _Tutorial_XStreet = 7,
        _Tutorial_PushButton = 8,
        _Tutorial_Beep = 9,
        _Tutorial_Collect = 10,
        _Tutorial_TurnBack = 11,
        _Tutorial_Congratuate = 12,
        _Error_HurtByCar = 13,
        _Error_TooRight = 14,
        _Error_TooLeft = 15,
        _Error_Opposite = 16,
        _Encouragement_1 = 17,
        _Encouragement_2 = 18,
        _Encouragement_3 = 19,
        _Encouragement_Hint = 20,
        _Instruction_1 = 21,
        _Instruction_2 = 22,
        _Instruction_3 = 23,
        _Instruction_4 = 24,
        _Instruction_5 = 25,
        _Instruction_6 = 26,
        _Instruction_7 = 27,
        _Intro = 28
        
    };

    public AudioClip[] audios;
    [System.NonSerialized]
    public List<int> playList = new List<int>();
    [System.NonSerialized]
    public float span = 0.0f;
    private AudioSource audiosource;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (!audiosource.isPlaying)
        {
            if (index < playList.Count)
            {
                audiosource.clip = audios[playList[index]];
                audiosource.Play();
                index++;
            }
            else if (playList.Count >= 1000)
            {
                playList.Clear();
                index = 0;
            }
            span += Time.deltaTime;
        }
        else 
        {
            span = 0.0f;
        }
    }
}
