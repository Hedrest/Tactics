using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
    public int keys;
   // public int grenades;
  //  public  Image grenadeImage;
   // public Image keyImage;
    public GameObject keyUI;

	// Use this for initialization
	void Start () {
        keys = 0;
        //grenades = 0;
        
        
	}
	
	// Update is called once per frame
	void Update () {
       
        if (KeyItem.keyCount >= 1)
        {
              
            keyUI.SetActive(true);
           
        }else if (KeyItem.keyCount <= 0)
        {
          
            keyUI.SetActive(false);
           
        }
    }
   /* void UpdateGrenades()
    {
        if (KeyItem.keyCount >= 1)
        {
            grenadeImage.enable;
        }
        else if (KeyItem.keyCount <= 0)
        {
            grenadeImage.disable;
        }
    }*/
}
