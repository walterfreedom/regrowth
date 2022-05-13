using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walterfreedom : MonoBehaviour
{
  
    private void Update()
    {
        
    }
    public void DesertedIsland()
    {
        if (GameObject.Find("Player").GetComponent<playerStats>().money >= 500)
        {


            GameObject.Find("Player").GetComponent<playerStats>().money -= 500;
            gameObject.GetComponent<moveScene>().movescene();
            transform.Find("Image").Find("base").gameObject.SetActive(false);
            transform.Find("Image").Find("quest").gameObject.SetActive(true);
        }
        
    }

    public void returnHome()
    {
        transform.Find("Image").Find("base").gameObject.SetActive(true);
        transform.Find("Image").Find("quest").gameObject.SetActive(false);
    }
}


