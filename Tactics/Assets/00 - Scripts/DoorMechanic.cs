using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanic : MonoBehaviour {
    public Animator animator;
    public Light lt;
    public AudioClip GreenMusicClip;
    public AudioSource GreenMusicSource;
    public AudioClip RedMusicClip;
    public AudioSource RedMusicSource;
    public AudioClip DoorMusicClip;
    public AudioSource DoorMusicSource;

    private void OnTriggerEnter(Collider other)
    {
        GreenMusicSource.clip = GreenMusicClip;
        RedMusicSource.clip = RedMusicClip;
        DoorMusicSource.clip = DoorMusicClip;
        if (other.gameObject.tag == "Player" && KeyItem.keyCount >= 1)
        {
            
            animator.SetBool("character_nearby", true);
            lt.color = Color.green;
            GreenMusicSource.Play();
            KeyItem.keyCount--;
            DoorMusicSource.Play();
        }
        else
        {
            animator.SetBool("character_nearby", false);
            lt.color = Color.red;
            RedMusicSource.Play();
        }
    }

}
