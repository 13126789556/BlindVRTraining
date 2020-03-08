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
        _Tutorial_XStreet = 6,
        _Tutorial_PushButton = 7,
        _Tutorial_Beep = 8,
        _Tutorial_Collect = 9,
        _Tutorial_TurnBack = 10,
        _Tutorial_Congratuate = 11,
        _Error_HurtByCar = 12
    };

    public AudioClip[] audios;
    public List<int> playList = new List<int>();
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
        if (index < playList.Count)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.clip = audios[playList[index]];
                audiosource.Play();
                index++;
            }
        }
        else
        {
            playList.Clear();
            index = 0;
        }
    }
}
