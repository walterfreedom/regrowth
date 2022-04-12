using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chest : MonoBehaviour
{
    List<List<GameObject>> storeditems;
    List<GameObject> buttons;
    playerStats pstats;
    GameObject chestUI;
    GameObject chestButton;
    private void Start()
    {
        for(int x = 0; x <= 20; x++)
        {
            storeditems.Add(new List<GameObject>());
        }
    }

    public void openChest(GameObject Player)
    {
        pstats = Player.GetComponent<playerStats>();
        chestUI = pstats.chestUI;
        chestButton = chestUI.transform.Find("tempbutton").gameObject;
        int x = 0;
        foreach (var item in storeditems)
        {
            GameObject newbutton = Instantiate(chestButton, chestUI.transform);
            buttons.Add(newbutton);
            if (item.Count != 0)
            {
                newbutton.GetComponent<Image>().sprite = item[0].GetComponent<SpriteRenderer>().sprite;
                newbutton.GetComponent<Image>().color = item[0].GetComponent<SpriteRenderer>().color;
                newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
            }
            else
            {
                newbutton.GetComponent<Button>().onClick.AddListener(delegate { pickputItem(x); });
            }
            x++;
        }
    }



    public void pickputItem(int id)
    {
        if (pstats.tempitems.Count != 0)
        {
            List<GameObject> tlist = new List<GameObject>();
            if (storeditems[id].Count != 0)
            {
                tlist.AddRange(storeditems[id]);
            }
            storeditems[id] = pstats.tempitems;
            pstats.tempitems = tlist;
        }
        else
        {
            if (storeditems[id].Count != 0)
            {
                pstats.putItemInTempItem(storeditems[id]);
                storeditems[id].Clear();
                buttons[id].GetComponent<Image>().sprite = default;
                buttons[id].GetComponent<Image>().color = default;
            }
        }
        
    }
}
