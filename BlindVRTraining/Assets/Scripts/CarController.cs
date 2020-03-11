using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarController : NetworkBehaviour
{
    public static int carCount;
    public enum Direction { Null, North, Sorth, West, East}
    public Direction sourceDir; 
    public float speed = 10;
    public float leftTurnRadius, rightTurnRadius;
    float originSpeed;
    Ray forwardDetection;
    public enum Behavior { Null, TurnLeft, GoStraight, TurnRight }
    public Behavior behavior;
    IntersectionController _ic;
    RaycastHit hit;
    float prevHitVelocity;
    Rigidbody rb;
    AudioSource engineWoring;
    float timeFromInit;
    LayerMask ignoreWlakSide = ~(1 << 8);
    bool isTracking;
    private void Awake()
    {
        carCount++;
    }
    void Start()
    {
        _ic = IntersectionController.Instance;

        originSpeed = speed;
        rb = GetComponent<Rigidbody>();
        #region car initialization
        switch (Random.Range(0, 4))
        {
            case 0:
                {
                    sourceDir = sourceDir == Direction.Null ? Direction.East : sourceDir;
                    transform.forward = new Vector3(-1, 0, 0);
                    int i = Random.Range(0, 3);
                    if (i == 0)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnLeft : behavior;
                        transform.position = new Vector3(190, 0, 2);
                    }
                    else if (i == 1)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.GoStraight : behavior;
                        transform.position = new Vector3(190, 0, Random.Range(0, 1) > 0 ? 2 : 5);
                    }
                    else if (i == 2)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnRight : behavior;
                        transform.position = new Vector3(190, 0, 8);
                    }
                    break;
                }
            case 1:
                {
                    sourceDir = sourceDir == Direction.Null ? Direction.North : sourceDir;
                    transform.forward = new Vector3(0, 0, -1);
                    int i = Random.Range(0, 3);
                    if (i == 0)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnLeft : behavior;
                        transform.position = new Vector3(-2, 0, 190);
                    }
                    else if (i == 1)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.GoStraight : behavior;
                        transform.position = new Vector3(Random.Range(0, 1) > 0 ? -2 : -5, 0, 190);
                    }
                    else if (i == 2)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnRight : behavior;
                        transform.position = new Vector3(-8, 0, 190);
                    }
                    break;
                }
            case 2:
                {
                    sourceDir = sourceDir == Direction.Null ? Direction.Sorth : sourceDir;
                    transform.forward = new Vector3(0, 0, 1);
                    int i = Random.Range(0, 3);
                    if (i == 0)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnLeft : behavior;
                        transform.position = new Vector3(2, 0, -190);
                    }
                    else if (i == 1)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.GoStraight : behavior;
                        transform.position = new Vector3(Random.Range(0, 1) > 0 ? 2 : 5, 0, -190);
                    }
                    else if (i == 2)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnRight : behavior;
                        transform.position = new Vector3(8, 0, -190);
                    }
                    break;
                }
            case 3:
                {
                    sourceDir = sourceDir == Direction.Null ? Direction.West : sourceDir;
                    transform.forward = new Vector3(1, 0, 0);
                    int i = Random.Range(0, 3);
                    if (i == 0)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnLeft : behavior;
                        transform.position = new Vector3(-190, 0, -2);
                    }
                    else if (i == 1)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.GoStraight : behavior;
                        transform.position = new Vector3(-190, 0, Random.Range(0, 1) > 0 ? -2 : -5);
                    }
                    else if (i == 2)
                    {
                        behavior = behavior == Behavior.Null ? Behavior.TurnRight : behavior;
                        transform.position = new Vector3(-190, 0, -8);
                    }
                    break;
                }
        }
        switch (sourceDir)
        {
            case Direction.East:
                {
                    transform.forward = new Vector3(-1, 0, 0);
                    if (behavior == Behavior.TurnLeft)
                    {
                        transform.position = new Vector3(190, 0, 2);
                    }
                    else if (behavior == Behavior.GoStraight)
                    {
                        transform.position = new Vector3(190, 0, Random.Range(0, 1) > 0 ? 2 : 5);
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        transform.position = new Vector3(190, 0, 8);
                    }
                    break;
                }
            case Direction.North:
                {
                    transform.forward = new Vector3(0, 0, -1);
                    if (behavior == Behavior.TurnLeft)
                    {
                        transform.position = new Vector3(-2, 0, 190);
                    }
                    else if (behavior == Behavior.GoStraight)
                    {
                        transform.position = new Vector3(Random.Range(0, 1) > 0 ? -2 : -5, 0, 190);
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        transform.position = new Vector3(-8, 0, 190);
                    }
                    break;
                }
            case Direction.Sorth:
                {
                    transform.forward = new Vector3(0, 0, 1);
                    if (behavior == Behavior.TurnLeft)
                    {
                        transform.position = new Vector3(2, 0, -190);
                    }
                    else if (behavior == Behavior.GoStraight)
                    {
                        transform.position = new Vector3(Random.Range(0, 1) > 0 ? 2 : 5, 0, -190);
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        transform.position = new Vector3(8, 0, -190);
                    }
                    break;
                }
            case Direction.West:
                {
                    transform.forward = new Vector3(1, 0, 0);
                    if (behavior == Behavior.TurnLeft)
                    {
                        transform.position = new Vector3(-190, 0, -2);
                    }
                    else if (behavior == Behavior.GoStraight)
                    {
                        transform.position = new Vector3(-190, 0, Random.Range(0, 1) > 0 ? -2 : -5);
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        transform.position = new Vector3(-190, 0, -8);
                    }
                    break;
                }
        }
        engineWoring = GetComponent<AudioSource>();
        engineWoring.volume = 0;
        #endregion
    }

    void Update()
    {
        if(!isServer)
        { return; }
        forwardDetection = new Ray(transform.position + new Vector3(0, 1, 0), transform.forward);
        if (Physics.Raycast(forwardDetection, out hit, 12,ignoreWlakSide))
        {
            Debug.DrawLine(forwardDetection.origin, hit.point);
            if (hit.collider.name == "Intersection")    //signal light check
            {
                switch (sourceDir)
                {
                    case Direction.East:
                        {
                            if (!_ic.signalLightWest.AllowGoStraight && behavior == Behavior.GoStraight
                                || !_ic.signalLightWest.AllowTurnLeft && behavior == Behavior.TurnLeft)
                            { Brake(8, hit.point + (transform.position - hit.point).normalized * 8, 2); }
                            else
                            {
                                MoveOn();
                            }
                            break;
                        }
                    case Direction.West:
                        {
                            if (!_ic.signalLightEast.AllowGoStraight && behavior == Behavior.GoStraight
                                || !_ic.signalLightEast.AllowTurnLeft && behavior == Behavior.TurnLeft)
                            { Brake(8, hit.point + (transform.position - hit.point).normalized * 8, 2); }
                            else
                            { MoveOn(); }
                            break;
                        }
                    case Direction.North:
                        {
                            if (!_ic.signalLightSorth.AllowGoStraight && behavior == Behavior.GoStraight
                                || !_ic.signalLightSorth.AllowTurnLeft && behavior == Behavior.TurnLeft)
                            { Brake(8, hit.point + (transform.position - hit.point).normalized * 8, 2); }
                            else
                            { MoveOn(); }
                            break;
                        }
                    case Direction.Sorth:
                        {
                            if (!_ic.signalLightNorth.AllowGoStraight && behavior == Behavior.GoStraight
                                || !_ic.signalLightNorth.AllowTurnLeft && behavior == Behavior.TurnLeft)
                            { Brake(8, hit.point + (transform.position - hit.point).normalized * 8, 2); }
                            else
                            { MoveOn(); }
                            break;
                        }
                }
            }
            else if(/*hit.collider.name != "DeadZone" && hit.collider.name != "Track Zone"*/!hit.collider.isTrigger)    //other obstacle check
            {
                if (hit.transform.GetComponent<CarController>() != null 
                    && hit.transform.GetComponent<CarController>().speed - prevHitVelocity >= 0   //car move on in advance
                    && hit.transform.GetComponent<CarController>().speed > 2)    
                {
                    MoveOn();
                }
                else
                {
                    Brake(8, hit.point + (transform.position - hit.point).normalized * 1, 3);
                }
            }
            prevHitVelocity = hit.transform.GetComponent<CarController>() == null ? 0: hit.transform.GetComponent<CarController>().speed;
        }
        else
        {
            MoveOn();
        }

        //Turning 
        switch (sourceDir)
        {
            case Direction.East:
                {
                    if (behavior == Behavior.TurnLeft)
                    {
                        TurnLeft(new Vector3(12.5f, 0, -12.5f));
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        TurnRight(new Vector3(12.5f, 0, 12.5f));
                    }
                    break;
                }
            case Direction.North:
                {
                    if (behavior == Behavior.TurnLeft)
                    {
                        TurnLeft(new Vector3(12.5f, 0, 12.5f));
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        TurnRight(new Vector3(-12.5f, 0, 12.5f));
                    }
                    break;
                }
            case Direction.Sorth:
                {
                    if (behavior == Behavior.TurnLeft)
                    {
                        TurnLeft(new Vector3(-12.5f, 0, -12.5f));
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        TurnRight(new Vector3(12.5f, 0, -12.5f));
                    }
                    break;
                }
            case Direction.West:
                {
                    if (behavior == Behavior.TurnLeft)
                    {
                        TurnLeft(new Vector3(-12.5f, 0, 12.5f));
                    }
                    else if (behavior == Behavior.TurnRight)
                    {
                        TurnRight(new Vector3(-12.5f, 0, -12.5f));
                    }
                    break;
                }
        }

        //moving
        speed = Mathf.Clamp(speed, 0, originSpeed);
        transform.position += Time.deltaTime * speed * transform.forward;
        //rb.velocity = speed * transform.forward;

        timeFromInit += Time.deltaTime;
        if(timeFromInit > 1)
        {       
            engineWoring.volume = speed / originSpeed;
        }
    }

    void Brake(float brakeDistance,Vector3 stopPos, float brakeForce)
    {
        speed -= speed < 0 ? 0 : Time.deltaTime * brakeForce * Mathf.Max(0, (brakeDistance - (transform.position - stopPos).magnitude));
    }

    void MoveOn()
    {
        speed += speed < originSpeed ? Time.deltaTime * 4 : 0;
    }

    void TurnLeft(Vector3 pivot)
    {
        if (Mathf.Abs(transform.position.x) <= 12.5f && Mathf.Abs(transform.position.z) <= 12.5f)   //enter the intersection
        {
            transform.forward = Vector3.Cross((transform.position - pivot), transform.up);
        }
        else
        {
            if (-10 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 10) { transform.rotation = Quaternion.Euler(0, 0, 0); }
            else if (80 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 100) { transform.rotation = Quaternion.Euler(0, 90, 0); }
            else if (170 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 190) { transform.rotation = Quaternion.Euler(0, 180, 0); }
            else if (260 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 280) { transform.rotation = Quaternion.Euler(0, 270, 0); }
        }
    }
    void TurnRight(Vector3 pivot)
    {
        if (Mathf.Abs(transform.position.x) <= 12.5f && Mathf.Abs(transform.position.z) <= 12.5f)   //enter the intersection
        {
            transform.forward = Vector3.Cross(transform.up, (transform.position - pivot));
        }
        else
        {
            if (-10 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 10) { transform.rotation = Quaternion.Euler(0, 0, 0); }
            else if (80 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 100) { transform.rotation = Quaternion.Euler(0, 90, 0); }
            else if (170 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 190) { transform.rotation = Quaternion.Euler(0, 180, 0); }
            else if (260 < transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 280) { transform.rotation = Quaternion.Euler(0, 270, 0); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "DeadZone") { Destroy(gameObject); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Track Zone" && !TurnHeadParallel.isCarInTrackZone)
        {
            isTracking = true;
            TurnHeadParallel.isCarInTrackZone = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTracking)
        {
            TurnHeadParallel.targetPosition = transform.position;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Track Zone")
        {
            TurnHeadParallel.isCarInTrackZone = false;
            isTracking = false;
        }
    }

    private void OnDestroy()
    {
        carCount--;
    }
}
