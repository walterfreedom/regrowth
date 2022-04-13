using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chest : MonoBehaviour
{

    public List<GameObject> storeditems;
    playerStats pstats;
    GameObject chestUI;
    GameObject chestButton;
    private void Start()
    {
        pstats = GameObject.Find("Player").GetComponent<playerStats>();
        chestUI = pstats.chestUI;
        chestButton = chestUI.transform.Find("tempbutton").gameObject;

        int x = 0;
        float xaxis = 0;
        float yaxis = 0;

        for (int i = 0; i <= 20; i++)
        {
            GameObject button = Instantiate(chestButton,chestUI.transform);
            int index = i;
            //delegate only captures the latest i, because it gets called after the for loop ends.
            button.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(index); });
            //inventory slot for data, will be upgraded to scriptableobject
            button.AddComponent<inventorySlot>();
            storeditems.Add(button);
            button.transform.position += new Vector3(xaxis, yaxis,0);
            x++;
            xaxis += 100;
            if (x % 5 == 0)
            {
                xaxis -= 500;
                yaxis -= 100;
            }
        }
    }

    public void openChest(GameObject Player)
    {
        foreach (var item in storeditems)
        {
            item.active = true;
            
        }
        chestUI.active = true;
    }



    public void pickputItem(int id)
    {
        if (pstats.tempitems.Count != 0)
        {
            
            if (storeditems[id].GetComponent<inventorySlot>().storedItems.Count != 0)
            {
                print("a");
                List<GameObject> a = new List<GameObject>();
                a.AddRange(storeditems[id].GetComponent<inventorySlot>().storedItems);
                storeditems[id].GetComponent<inventorySlot>().clearSlot();
                storeditems[id].GetComponent<inventorySlot>().additem(pstats.tempitems);
                pstats.templistClear();
                pstats.templistAdd(a);
            }
            else
            {
                print("b");
                storeditems[id].GetComponent<inventorySlot>().additem(pstats.tempitems);
                pstats.templistClear();
            }
        }
        else
        {
            
            if (storeditems[id].GetComponent<inventorySlot>().storedItems.Count != 0)
            {
                print("c");
                pstats.templistAdd(storeditems[id].GetComponent<inventorySlot>().storedItems);
                storeditems[id].GetComponent<inventorySlot>().clearSlot();
            }
        }

        //print("walter");
        //if (storeditems[id].GetComponent<inventorySlot>().storedItems.Count != 0)
        //{
        //    //newbutton.GetComponent<Image>().sprite = item.storedItems[0].GetComponent<SpriteRenderer>().sprite;
        //    //newbutton.GetComponent<Image>().color = item.storedItems[0].GetComponent<SpriteRenderer>().color;
        //    newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
        //}
        //else
        //{
        //    newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
        //}

        //    if (pstats.tempitems.Count != 0)
        //    {
        //        List<GameObject> tlist = new List<GameObject>();
        //        if (storeditems[id].storedItems.Count != 0)
        //        {
        //            tlist.AddRange(storeditems[id].storedItems);
        //        }
        //        storeditems[id].storedItems = pstats.tempitems;
        //        buttons[id].GetComponent<Image>().sprite = pstats.tempitems[0].GetComponent<SpriteRenderer>().sprite;
        //        buttons[id].GetComponent<Image>().color = pstats.tempitems[0].GetComponent<SpriteRenderer>().color;
        //        pstats.tempitems = tlist;
        //    }
        //    else
        //    {
        //        if (storeditems[id].storedItems.Count != 0)
        //        {
        //            pstats.putItemInTempItem(storeditems[id].storedItems);
        //            storeditems[id].storedItems.Clear();
        //            buttons[id].GetComponent<Image>().sprite = default;
        //            buttons[id].GetComponent<Image>().color = default;
        //        }
        //    }

    }
}
