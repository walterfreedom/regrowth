using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradeBase : MonoBehaviour
{
    public List<GameObject> levels;
    public string upgradername;
    public int currentlevel=0;
    public void checkupgrade()
    {
        var slot = gameObject.transform.Find("Canvas").Find("upslot");
        
        if (slot.GetComponent<inventorySlot>().storedItems[0].GetComponent<pickle>().itemname == upgradername && levels.Count >= currentlevel + 1)
        {
            gameObject.transform.Find("Canvas").Find("upbutton").GetComponent<Image>().color= Color.green;
            gameObject.transform.Find("Canvas").Find("upbutton").GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.transform.Find("Canvas").Find("upbutton").GetComponent<Image>().color = Color.red;
            gameObject.transform.Find("Canvas").Find("upbutton").GetComponent<Button>().interactable = false;
        }
    }
    public void upgrade(GameObject upgrader)
    {
        if (upgrader.GetComponent<pickle>().itemname == upgradername && levels.Count>=currentlevel+1)
        {

            currentlevel += 1;
            Destroy(transform.Find("spawn"));
            var newlevel= Instantiate(levels[currentlevel-1],gameObject.transform);
            newlevel.name = "spawn";
        }
    }

    public void bandaidfix()
    {
        Invoke("checkupgrade",0.3f);
    }
}
