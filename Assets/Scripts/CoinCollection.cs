using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "coin")
        {
            Coin++;
            Debug.Log(Coin);
            Destroy(collider.gameObject);
        }
    }
}

