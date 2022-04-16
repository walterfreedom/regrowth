using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventorySlot : MonoBehaviour
{
    public List<GameObject> storedItems;
    public playerStats pstats;
    public string inventoryType = "storage";
        
    private void Start()
    {
        pstats = GameObject.Find("Player").GetComponent<playerStats>();
        //Button button = gameObject.GetComponent<Button>();
        //button.onClick.AddListener(dropItem);
        storedItems = new List<GameObject>();
        gameObject.GetComponent<Button>().onClick.AddListener(pickputItem);
    }

    public void dropItem()
    {
        if (gameObject.transform.root.GetComponent<playerStats>().isShopping)
        {
            if (storedItems.Count > 0)
            {
                var itemcount = gameObject.transform.Find("Text (TMP)");
                int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                if (previousnum > 1)
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum - 1).ToString();
                }
                else
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = "";
                }

                gameObject.transform.root.GetComponent<playerStats>().money += storedItems[0].GetComponent<pickle>().value;
                gameObject.transform.root.GetComponent<playerStats>().updateMoney();
                Destroy(storedItems[0]);

                storedItems.RemoveAt(0);


                if (storedItems.Count < 1)
                {
                    gameObject.transform.Find("item").GetComponent<Image>().sprite = default;
                    gameObject.transform.Find("item").GetComponent<Image>().color = Color.white;
                }
            }

        }
        else
        {
            if (storedItems.Count > 0)
            {
                var itemcount = gameObject.transform.Find("Text (TMP)");
                int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                if (previousnum > 1)
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum - 1).ToString();
                }
                else
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = "";
                }

                storedItems[0].transform.position = gameObject.transform.parent.parent.position;
                storedItems[0].active = true;
                storedItems.RemoveAt(0);

                if (storedItems.Count < 1)
                {
                    gameObject.transform.Find("item").GetComponent<Image>().sprite = default;
                    gameObject.transform.Find("item").GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
    public void pickputItem()
    {
        if (pstats.tempitems.Count != 0)
        {

            if (storedItems.Count != 0)
            {
                if (storedItems[0].GetComponent<pickle>().itemname== pstats.tempitems[0].GetComponent<pickle>().itemname)
                {
                    if (storedItems[0].GetComponent<pickle>().stacksize >= pstats.tempitems.Count + storedItems.Count)
                    {
                        additem(pstats.tempitems);
                        pstats.templistClear();
                    }
                    else
                    {
                        var itemstotransfer = new List<GameObject>();
                        var itemstokeep = new List<GameObject>();
                        
                        for(int i = 0; i< storedItems[0].GetComponent<pickle>().stacksize- storedItems.Count; i++)
                        {
                            if(i<pstats.tempitems.Count)
                            itemstotransfer.Add(pstats.tempitems[i]);
                            else
                            {
                                break;
                            }
                            
                        }
                        for(int j= storedItems[0].GetComponent<pickle>().stacksize - storedItems.Count; j < pstats.tempitems.Count;j++)
                        {
                            if (j < pstats.tempitems.Count)
                                itemstokeep.Add(pstats.tempitems[j]);
                            else
                            {
                                break;
                            }
                        }
                        additem(itemstotransfer);
                        pstats.templistClear();
                        pstats.templistAdd(itemstokeep);
                    }
                }
                else
                {
                  
                    var a = new List<GameObject>();
                    a.AddRange(storedItems);
                    clearSlot();
                    additem(pstats.tempitems);
                    pstats.templistClear();
                    pstats.templistAdd(a);
                }
                
            }
            else
            {
                additem(pstats.tempitems);
                pstats.templistClear();
            }
        }
        else
        {

            if (storedItems.Count != 0)
            {
                pstats.templistAdd(storedItems);
                clearSlot();
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

    public void holdItem()
    {
        gameObject.transform.parent.parent.GetComponent<playerStats>();
    }
    public void dropItem(Vector3 position)
    {
        if (gameObject.transform.root.GetComponent<playerStats>().isShopping)
        {
            if (storedItems.Count > 0)
            {
                var itemcount = gameObject.transform.Find("Text (TMP)");
                int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                if (previousnum > 1)
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum - 1).ToString();
                }
                else
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = "";
                }

                gameObject.transform.root.GetComponent<playerStats>().money += storedItems[0].GetComponent<pickle>().value;
                gameObject.transform.root.GetComponent<playerStats>().updateMoney();
                Destroy(storedItems[0]);

                storedItems.RemoveAt(0);


                if (storedItems.Count < 1)
                {
                    gameObject.transform.Find("item").GetComponent<Image>().sprite = default;
                    gameObject.transform.Find("item").GetComponent<Image>().color = Color.white;
                }
            }

        }
        else
        {
            if (storedItems.Count > 0)
            {
                var itemcount = gameObject.transform.Find("Text (TMP)");
                int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                if (previousnum > 1)
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum - 1).ToString();
                }
                else
                {
                    itemcount.gameObject.GetComponent<TMP_Text>().text = "";
                }

                storedItems[0].transform.position = position;
                storedItems[0].active = true;
                storedItems.RemoveAt(0);

                if (storedItems.Count < 1)
                {
                    gameObject.transform.Find("item").GetComponent<Image>().sprite = default;
                    gameObject.transform.Find("item").GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
    public void additem(List<GameObject> items)
    {
        if (storedItems.Count == 0)
        {
            storedItems.AddRange(items);
            transform.Find("item").GetComponent<Image>().sprite = items[0].GetComponent<SpriteRenderer>().sprite;
            transform.Find("item").GetComponent<Image>().color = items[0].GetComponent<SpriteRenderer>().color;
            transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = storedItems.Count.ToString();

            //Todo
            //if (inventoryType=="helmet")
            //{
                
            //}
        }
        else
        {
            storedItems.AddRange(items);
            transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = storedItems.Count.ToString();
        }


    }

    public void clearSlot(){
        transform.Find("item").GetComponent<Image>().sprite = default;
        transform.Find("item").GetComponent<Image>().color = default;
        transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = "";
        storedItems.Clear();
    }
}


