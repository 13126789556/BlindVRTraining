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
            var tempCarController = Instantiate(car).GetComponent<CarController>();
            //tempCarController.behavior = CarController.Behavior.GoStraight;
            //tempCarController.sourceDir = CarController.Direction.East;
            timer = Random.Range(2,6);
        }
    }

}
