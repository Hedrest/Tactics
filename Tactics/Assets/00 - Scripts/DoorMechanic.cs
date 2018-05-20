using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanic : MonoBehaviour {
    public Animator animator;
    public  new Light light;
     void Start()
    {
        light = GetComponent<Light>();
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Player" && KeyItem.keyCount >= 1)
        {
            light.color = Color.green;
            animator.SetBool("character_nearby", true);
            KeyItem.keyCount--;
        }
        else
        {
            animator.SetBool("character_nearby", false); 
        }
    }
}
