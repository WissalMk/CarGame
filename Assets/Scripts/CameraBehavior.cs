using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target; // Drag the player car here
    public Vector3 offset;
    public float cameraHeight = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Update camera position (including height)
            Vector3 newPosition = target.position + offset;
            newPosition.y = target.position.y + cameraHeight;
            transform.position = newPosition;

            // Look at the target
            transform.LookAt(target);
        }
    }
}
