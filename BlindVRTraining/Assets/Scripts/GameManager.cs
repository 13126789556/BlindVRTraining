using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject car;
    IntersectionController _ic;
    float timer;
    void Start()
    {
        _ic = IntersectionController.Instance;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Instantiate(car);
            timer = Random.Range(2,6);
        }
    }

}
