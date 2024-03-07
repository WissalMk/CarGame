using System.Collections;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float speed = 11f; // Initial speed of the car
    public float turnSpeed = 50f; // Rotation speed
    private float originalSpeed; // Store the original speed for restoration

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        originalSpeed = speed; // Save the original speed
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            float rotation = horizontalInput * turnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            // Decrease the car's speed
            speed -= 5f; 

            // Ensure the speed doesn't go below zero
            speed = Mathf.Max(0f, speed);

            // Destroy the block
            Destroy(other.gameObject);

            // Launch the coroutine to restore the speed after a certain time
            StartCoroutine(RestoreSpeed());
        }
    }

    IEnumerator RestoreSpeed()
    {
        // Wait for a certain time (e.g., 3 seconds)
        yield return new WaitForSeconds(3f);

        // Restore the original speed
        speed = originalSpeed;
    }
}
