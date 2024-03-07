using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{

    private int Coin = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "coin")
        {
            Destroy(collider.gameObject);
        }
    }
}
