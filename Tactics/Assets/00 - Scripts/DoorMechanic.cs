using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanic : MonoBehaviour {
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Player" && KeyItem.keyCount >= 1)
        {
            animator.SetBool("character_nearby", true);
            KeyItem.keyCount--;
        }
        else
        {
            animator.SetBool("character_nearby", false); 
        }
    }
}
