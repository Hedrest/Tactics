using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roofblock : MonoBehaviour {

    //public GameObject ;
   
   

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
        }
        
    }
}
