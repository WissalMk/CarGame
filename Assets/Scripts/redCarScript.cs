using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redCarScript : MonoBehaviour
{
    public float speed = 10f; // Initial speed of the red car
    public float turnSpeed = 150f; // Rotation speed
    private Quaternion initialRotation;
    private CarScript greenCarScript;


    void Start()
    {
        GameObject greenCarObject = GameObject.Find("GreenCar");
        greenCarScript = greenCarObject.GetComponent<CarScript>();

        // Set the initial position and rotation of the RedCar based on the GreenCar
        //transform.position = new Vector3(greenCarObject.transform.position.x - 6f, greenCarObject.transform.position.y, greenCarObject.transform.position.z);
        initialRotation = transform.rotation;

        // Set the initial speed of the RedCar based on the GreenCar's speed
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Update()
    {
        UpdateRedCarSpeed();
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void UpdateRedCarSpeed()
    {
        
        float variation = Random.Range(-1f, 1f);
        speed = greenCarScript.speed + variation;
    }


}
