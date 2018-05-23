using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour {
    public static int keyCount;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyCount += 1;
            Destroy(gameObject);
        }
    }
	
}
