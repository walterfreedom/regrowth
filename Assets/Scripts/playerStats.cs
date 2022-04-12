using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    public int allowedFollowers = 1;
    public List<GameObject> followerList;
    public List<GameObject> inventory;
    public List<blueprint> crafting;

    public int money=1000;
    public bool isShopping;
    GameObject player;
    List<GameObject> inventoryslots;
    Button closeshop;
    Button sellmode;
    TMP_Text moneytext;
    Camera camera;
    GameObject selectedslot;
    public GameObject shopcanvas;
    public int score=0;
    List<GameObject> stufftodestory = new List<GameObject>();

    GameObject oxygencanvas;
    public GameObject craftingUI;
    private GameObject craftingslot;
    public GameObject chestUI;

    public List<GameObject> tempitems;
    bool inventoryMode;

    private void Start()
    {
        craftingslot = gameObject.transform.Find("Canvas").Find("craftslot").gameObject;
        shopcanvas = gameObject.transform.Find("shopcanvas").gameObject;
        camera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
        player = gameObject;
        sellmode = gameObject.transform.Find("shopcanvas").Find("sellmode").GetComponent<Button>();
        moneytext = gameObject.transform.Find("Canvas").Find("money").GetComponent<TMP_Text>();
        oxygencanvas = gameObject.transform.Find("Canvas").Find("oxygen").gameObject;

        sellmode.onClick.AddListener(setSellmode);

        updateMoney();
        //get inventory slots 
        var canvas = transform.Find("Canvas");
        var a =canvas.GetComponentInChildren<Transform>();
        foreach(Transform child in a)
        {
            //look at child of child
            if (child.gameObject.tag == "InventorySlot")
            {
                inventory.Add(child.gameObject);
            }
        }
        selectedslot = inventory[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //check if first inv slot is full from "inventory slot" script that you will create
            //if its not put the gameobject into it like slot.item=gameobject and set object to deactive
            //when you click on slot you drop the item? 

            Vector2 mousePosition = Input.mousePosition;
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, mousePosition, 2);
            var stats = player.GetComponent<playerStats>();
            
        }
        if (Input.GetMouseButtonDown(0)&&!isShopping&&selectedslot.GetComponent<inventorySlot>().storedItems.Count!=0)
        {
            if(selectedslot.GetComponent<inventorySlot>().storedItems[0].TryGetComponent<blueprint>(out blueprint blueprint))
            {
                crafting.Add(blueprint);
                var a = selectedslot.GetComponent<inventorySlot>().storedItems[0];
                selectedslot.GetComponent<inventorySlot>().dropItem();
                  Destroy(a);
            }
            else if (selectedslot.GetComponent<inventorySlot>().storedItems[0].GetComponent<pickle>().category == "weapon")
            {
                //this will be attack script for player
            }
            else
            {
                Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                selectedslot.GetComponent<inventorySlot>().dropItem(mousePosition);
            }
            
        }
        if (Input.GetKeyDown("e"))
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, mousePosition - gameObject.transform.position, 2);
            var stats = player.GetComponent<playerStats>();
            foreach (var hit in hits)
            {            
                if (hit.transform.gameObject.TryGetComponent<pickle>(out pickle pickle1))
                {
                    if (pickle1.infiniteitemspawn)
                    {
                        var copyitem = Instantiate(hit.transform.gameObject);
                        copyitem.GetComponent<pickle>().infiniteitemspawn = false;
                        gameObject.GetComponent<playerStats>().addtoinventory(copyitem);
                    }
                    else
                    {
                        gameObject.GetComponent<playerStats>().addtoinventory(hit.transform.gameObject);
                    }
                    
                }

                if(hit.transform.gameObject.TryGetComponent<chest>(out chest chest))
                {
                    chest.openChest(gameObject);
                }


                if (hit.transform.gameObject.TryGetComponent<shoopKeeper>(out shoopKeeper shop))
                {
                    List<GameObject> ShoppingList = new List<GameObject>();
                    ShoppingList.AddRange(shop.inventory);

                    //yay shopping! consoooooooom!
                    int x = -110;
                    int y = 120;
                    int itemorder = 1;

                    foreach (var item in ShoppingList)
                    {

                        var frame = shopcanvas.transform.Find("tempbutton");
                        var shopping = Instantiate(frame, shopcanvas.transform);
                        stufftodestory.Add(shopping.gameObject);
                        shopping.name = "shopbutton" + itemorder;
                        shopping.gameObject.active = true;
                        shopping.Find("Image").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                        shopping.Find("Image").GetComponent<Image>().color = item.GetComponent<SpriteRenderer>().color;
                        shopping.Find("Text (TMP)").GetComponent<TMP_Text>().text = item.GetComponent<pickle>().name;
                        shopping.GetComponent<shopscript>().storedItems.Add(item);
                        shopping.transform.localPosition = new Vector2(x, y);
                        x += 110;
                        itemorder++;
                        if (itemorder % 3 == 1)
                        {
                            y -= 100;
                            x = -110;
                        }
                    }
                    openShop();
                }

                if (hit.transform.gameObject.TryGetComponent<Greenify>(out Greenify greenify)) {
                    greenify.unGreen();
                    print("trying to ungreen");
                }

            }

        }
       
        if (Input.GetKeyDown("i"))
        {
          
            if (craftingUI.active)
            {
                craftingUI.active = false;
                foreach(Transform child in craftingUI.transform)
                {
                    Destroy(child.gameObject);
                }
                inventoryMode = false;
            }
            else
            {
                inventoryMode = true;
                float x = 0;
                float y = 200;
                foreach(blueprint bp in crafting)
                {
                   
                    var newcraft= Instantiate(craftingslot, craftingUI.transform);
                    var material = newcraft.transform.Find("recipe");
                    newcraft.transform.Find("output").gameObject.GetComponent<Image>().sprite=bp.output[0].GetComponent<SpriteRenderer>().sprite;
                    newcraft.transform.Find("output").gameObject.GetComponent<Image>().color = bp.output[0].GetComponent<SpriteRenderer>().color;
                    foreach (var ingredient in bp.materials)
                    {
                        
                        GameObject recipebox = Instantiate(material,newcraft.transform).gameObject;
                        recipebox.GetComponent<Image>().sprite = ingredient.GetComponent<SpriteRenderer>().sprite;
                        recipebox.GetComponent<Image>().color = ingredient.GetComponent<SpriteRenderer>().color;
                    }
                    newcraft.active = true;
                    newcraft.transform.position =new Vector2(newcraft.transform.position.x,craftingUI.transform.position.y+y);
                    newcraft.transform.Find("craftbutton").GetComponent<craftButton>().bp = bp;
                    y -= 50;
                }
                craftingUI.active = true;
            }
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedslot.GetComponent<Image>().color = Color.white; ;
            selectedslot = inventory[0];
            selectedslot.GetComponent<Image>().color = Color.cyan;
            
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedslot.GetComponent<Image>().color = Color.white;
            selectedslot = inventory[1];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedslot.GetComponent<Image>().color = Color.white; ;
            selectedslot = inventory[2];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedslot.GetComponent<Image>().color = Color.white; ;
            selectedslot = inventory[3];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedslot.GetComponent<Image>().color = Color.white; 
            selectedslot = inventory[4];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedslot.GetComponent<Image>().color = Color.white;
            selectedslot = inventory[5];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }

        if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedslot.GetComponent<Image>().color = Color.white;
            selectedslot = inventory[6];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }

        if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedslot.GetComponent<Image>().color = Color.white;
            selectedslot = inventory[7];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedslot.GetComponent<Image>().color = Color.white;
            selectedslot = inventory[8];
            selectedslot.GetComponent<Image>().color = Color.cyan;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            var a = inventory.Find(x => x == selectedslot);
            if (inventory.IndexOf(a) + 1 <=8)
            {
                int newslot = inventory.IndexOf(a) + 1;
                selectedslot.GetComponent<Image>().color = Color.white;
                selectedslot = inventory[newslot];
                selectedslot.GetComponent<Image>().color = Color.cyan;
            }
            else
            {
                int newslot = 0;
                selectedslot.GetComponent<Image>().color = Color.white;
                selectedslot = inventory[newslot];
                selectedslot.GetComponent<Image>().color = Color.cyan;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            var a = inventory.Find(x => x == selectedslot);
            if(inventory.IndexOf(a) - 1 >= 0)
            {
                int newslot = inventory.IndexOf(a) - 1;
                selectedslot.GetComponent<Image>().color = Color.white;
                selectedslot = inventory[newslot];
                selectedslot.GetComponent<Image>().color = Color.cyan;
            }
            else{
                int newslot = 8;
                selectedslot.GetComponent<Image>().color = Color.white;
                selectedslot = inventory[newslot];
                selectedslot.GetComponent<Image>().color = Color.cyan;
            }
            
        }
    }
    public void closeShop()
    {
        if (stufftodestory.Count != 0)
        {
            foreach (var frame in stufftodestory)
            {
                Destroy(frame);
            }
        }
        shopcanvas.active = false;
        isShopping = false;
    }

    void addblueprint(blueprint blueprint)
    {

    }

    public void putItemInTempItem(List<GameObject> items)
    {

    }

    public void putItemInTempItem(GameObject item)
    {
        if (tempitems.Count == 0)
        {
            tempitems.Add(item);
        }
        else if (tempitems[0].GetComponent<pickle>().itemname == item.GetComponent<pickle>().itemname)
        {
            tempitems.Add(item);
        }
        else
        {
           
        }
    }
    void openShop()
    {
        shopcanvas.active = true;
        isShopping = true;
    }
    public void increaseScore(int scoreincrease)
    {
        score += scoreincrease;
        transform.Find("Canvas").transform.Find("score").GetComponent<TMP_Text>().text="Amogus: "+score.ToString();
    }

    #region Survival


    public void updateoxygen(int updatedoxygen)
    {
        oxygencanvas.GetComponent<TMP_Text>().text = updatedoxygen.ToString();
    }

    #endregion
    public void setSellmode()
    {
        if (isShopping)
        {
            isShopping = false;
        }
        else
        {
            isShopping = true;
        }
    }

    public void updateMoney()
    {
        moneytext.text = money.ToString();
    }

    private void updateCrafting()
    {
        
    }

    public void addtoinventory(GameObject itemtoadd)
    {
        bool foundslot = false;
        pickle pickle1 = itemtoadd.GetComponent<pickle>();
        foreach (GameObject item in inventory)
        {
            //item is an independent inventory slot, not the item inside it.
            var storeditems = item.GetComponent<inventorySlot>().storedItems;
            if (storeditems.Count > 0)
            {
                if (storeditems[0].GetComponent<pickle>().name == pickle1.name && storeditems.Count < pickle1.stacksize)
                {
                    item.GetComponent<inventorySlot>().storedItems.Add(itemtoadd);
                    item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                    item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                    var itemcount = item.transform.Find("Text (TMP)");
                    int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                    itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum + 1).ToString();

                    itemtoadd.active = false;
                    foundslot = true;
                    break;
                }
            }
        }
        if (!foundslot)
        {
            foreach (GameObject item in inventory)
            {
                var storeditems = item.GetComponent<inventorySlot>().storedItems;

                if (storeditems.Count == 0)
                {
                    if (pickle1.infiniteitemspawn)
                    {
                        item.GetComponent<inventorySlot>().storedItems.Add(itemtoadd);
                        item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                        item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                        var itemcount = item.transform.Find("Text (TMP)");
                        itemcount.gameObject.GetComponent<TMP_Text>().text = 1.ToString();
                        itemtoadd.active = false;
                        foundslot = true;
                        break;
                    }
                    else
                    {
                        item.GetComponent<inventorySlot>().storedItems.Add(itemtoadd);
                        item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                        item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                        var itemcount = item.transform.Find("Text (TMP)");
                        itemcount.gameObject.GetComponent<TMP_Text>().text = 1.ToString();
                        itemtoadd.active = false;
                        foundslot = true;
                        break;
                    }      
                }
            }

        }
    }

}

public class InventoryItem
{
    private int amount;
    private GameObject item;
    private bool isEmpty;
    public InventoryItem(int amount1,GameObject item1, bool empty)
    {
        amount = amount1;
        item = item1;
        isEmpty = empty;
    }
}
 