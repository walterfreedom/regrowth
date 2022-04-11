using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftButton : MonoBehaviour
{
    public blueprint bp;
    public void craftitem(GameObject player)
    {

        
        playerStats pstats = player.GetComponent<playerStats>();
        List<GameObject> tempinventory=null;
        List<GameObject> itemstodestroylist = new List<GameObject>();
        int x = 0;
        foreach (var material in bp.materials)
        {
          

            foreach (var item in pstats.inventory)
            {
                
                if (item.GetComponent<inventorySlot>().storedItems.Count > 0)
                {
                    Debug.Log("comparing " + material.GetComponent<pickle>().itemname + " to " + item.GetComponent<inventorySlot>().storedItems[0].GetComponent<pickle>().itemname);
                    if (material.GetComponent<pickle>().itemname == item.GetComponent<inventorySlot>().storedItems[0].GetComponent<pickle>().itemname)
                    {
                        itemstodestroylist.Add(item.GetComponent<inventorySlot>().storedItems[0]);
                        Debug.Log("found match");
                        x++;
                    }
                }
                
            }
        }

        if (itemstodestroylist.Count == bp.materials.Count)
        {
            foreach (var item in itemstodestroylist)
            {
                item.GetComponent<inventorySlot>().dropItem();
                Destroy(item);
            }
            var a = Instantiate(bp.output[0]);
            player.GetComponent<playerStats>().addtoinventory(a);
        }
        
    }
}
