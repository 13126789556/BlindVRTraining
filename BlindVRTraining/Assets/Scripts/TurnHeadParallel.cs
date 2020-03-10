using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHeadParallel : MonoBehaviour
{
    player _player;
    // Start is call`ed before the first frame update
    void Start(){
        _player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        getAngle();
        isBackfacing();
    }

    public bool isLeftSideParallel()
    {
        return getAngle() < 15f;
    }

    public bool isBackfacing(){
        print(Mathf.Abs((getAngle() - 90f)) < 15f);
        return Mathf.Abs((getAngle() - 90f)) < 15f;
    }

    public float getAngle(){
        //angle = x1x2+y1y2 / sqr(x1*x1 + y1*y1) * sqr(x2*x2 + y2*y2)
        Vector3 facing = _player.getUnitFacingDirection();
        //forward (0,1)
        print(Vector3.forward);
        float cosAngle = facing.y / Mathf.Sqrt(facing.x * facing.x + facing.y * facing.y);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        print(angle);
        return angle;
    }
    

    public void comfirmPosition(){
        
    }
}
