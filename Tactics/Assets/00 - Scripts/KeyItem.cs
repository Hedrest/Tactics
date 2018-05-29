using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour {
    public static int keyCount;
    public AudioClip MusicClip;
    public AudioSource MusicSource;
    void OnTriggerEnter (Collider other)
    {
        MusicSource.clip = MusicClip;
        if (other.gameObject.tag == "Player")
        {
            keyCount += 1;
            MusicSource.Play();
            Destroy(gameObject);
        }
    }
	
}
