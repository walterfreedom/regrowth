using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventorySlot : MonoBehaviour
{
    public List<GameObject> storedItems;

    private void Start()
    {
        //Button button = gameObject.GetComponent<Button>();
        //button.onClick.AddListener(dropItem);
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
    public bool additem(List<GameObject> items)
    {
        if (storedItems.Count == 0)
        {
            storedItems.AddRange(items);
            transform.GetComponent<Image>().sprite = items[0].GetComponent<SpriteRenderer>().sprite;
            transform.GetComponent<Image>().color = items[0].GetComponent<SpriteRenderer>().color;
            return true;

        }
        else
        {
            return false;
        }
    }

    public void clearSlot(){
        transform.GetComponent<Image>().sprite = default;
        transform.GetComponent<Image>().color = default;
        storedItems.Clear();
    }
}


