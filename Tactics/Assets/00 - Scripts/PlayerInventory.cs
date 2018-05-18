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
        Debug.Log("working?");
        
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("hmmmhhh");
        if (KeyItem.keyCount >= 1)
        {
            Debug.Log("got something");   
            keyUI.SetActive(true);
            //keyImage.enable;
        }else if (KeyItem.keyCount <= 0)
        {
            Debug.Log("maybe not");
            keyUI.SetActive(false);
           // keyImage.disable;
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
