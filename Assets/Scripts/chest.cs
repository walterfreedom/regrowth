using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chest : MonoBehaviour
{

    public List<chestdata> storeditems=new List<chestdata>();
    playerStats pstats;
    GameObject chestUI;
    GameObject chestButton;
    private void Start()
    {
        
        pstats = GameObject.Find("Player").GetComponent<playerStats>();
        chestUI = pstats.chestUI;
       
        for (int i = 0; i < 26; i++)
        {
            storeditems.Add(new chestdata(new List<GameObject>()));   
        }
    }

    public void openChest()
    {
        //THATS VERY BUGGY FIX IT
        //IT MIGHT DUPLICATE ITEMS, AND IT ERASE IETMS YOU PUT IN IT
        //you left there ferret
        int x = 0;
        foreach (var item in storeditems)   
        {
            var slot=chestUI.transform.Find("Button (" + (x + 1)+ ")");
            slot.GetComponent<inventorySlot>().clearSlot();
            if (item.data.Count != 0)
            {
                slot.GetComponent<inventorySlot>().additem(item.data);
            }
            
            x++;
            
        }
        chestUI.active = true;
    }

    public void closeChest()
    {
        int x = 0;
        foreach (var item in storeditems)
        {
            var slot = chestUI.transform.Find("Button (" + (x + 1) + ")");
            
            item.data = slot.GetComponent<inventorySlot>().storedItems;

            x++;

        }
        chestUI.active = false;
    }



    public void pickputItem()
    {
        //if (pstats.tempitems.Count != 0)
        //{
            
        //    if (storeditems[id].data.Count != 0)
        //    {
        //        print("a");
        //        List<GameObject> a = new List<GameObject>();
        //        a.AddRange(storeditems[id].data);
        //        storeditems[id].GetComponent<inventorySlot>().clearSlot();
        //        storeditems[id].GetComponent<inventorySlot>().additem(pstats.tempitems);
        //        pstats.templistClear();
        //        pstats.templistAdd(a);
        //    }
        //    else
        //    {
        //        print("b");
        //        storeditems[id].GetComponent<inventorySlot>().additem(pstats.tempitems);
        //        pstats.templistClear();
        //    }
        //}
        //else
        //{
            
        //    if (storeditems[id].GetComponent<inventorySlot>().storedItems.Count != 0)
        //    {
        //        print("c");
        //        pstats.templistAdd(storeditems[id].GetComponent<inventorySlot>().storedItems);
        //        storeditems[id].GetComponent<inventorySlot>().clearSlot();
        //    }
        //}

        ////print("walter");
        ////if (storeditems[id].GetComponent<inventorySlot>().storedItems.Count != 0)
        ////{
        ////    //newbutton.GetComponent<Image>().sprite = item.storedItems[0].GetComponent<SpriteRenderer>().sprite;
        ////    //newbutton.GetComponent<Image>().color = item.storedItems[0].GetComponent<SpriteRenderer>().color;
        ////    newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
        ////}
        ////else
        ////{
        ////    newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
        ////}

        ////    if (pstats.tempitems.Count != 0)
        ////    {
        ////        List<GameObject> tlist = new List<GameObject>();
        ////        if (storeditems[id].storedItems.Count != 0)
        ////        {
        ////            tlist.AddRange(storeditems[id].storedItems);
        ////        }
        ////        storeditems[id].storedItems = pstats.tempitems;
        ////        buttons[id].GetComponent<Image>().sprite = pstats.tempitems[0].GetComponent<SpriteRenderer>().sprite;
        ////        buttons[id].GetComponent<Image>().color = pstats.tempitems[0].GetComponent<SpriteRenderer>().color;
        ////        pstats.tempitems = tlist;
        ////    }
        ////    else
        ////    {
        ////        if (storeditems[id].storedItems.Count != 0)
        ////        {
        ////            pstats.putItemInTempItem(storeditems[id].storedItems);
        ////            storeditems[id].storedItems.Clear();
        ////            buttons[id].GetComponent<Image>().sprite = default;
        ////            buttons[id].GetComponent<Image>().color = default;
        ////        }
        ////    }

    }
}

public class chestdata
{
    public List<GameObject> data;
    public chestdata(List<GameObject> data1)
    {
        data = data1;
    }
}
