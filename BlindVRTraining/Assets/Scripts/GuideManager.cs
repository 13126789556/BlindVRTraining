using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
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
