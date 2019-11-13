using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresden : Vehicle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalcSteeringForces()
    {
        if (Input.GetKey("W") || Input.GetKey(KeyCode.UpArrow)) ultimateForce += transform.forward * maxSpeed;

        if (Input.GetKey("S") || Input.GetKey(KeyCode.DownArrow)) ultimateForce -= transform.forward * maxSpeed;

        if (Input.GetKey("D") || Input.GetKey(KeyCode.RightArrow)) ultimateForce += transform.right * maxSpeed;

        if (Input.GetKey("A") || Input.GetKey(KeyCode.LeftArrow)) ultimateForce -= transform.right * maxSpeed;
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }
}
