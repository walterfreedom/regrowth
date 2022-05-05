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
    Stats stats;

    public List<GameObject> tempitems;
    public GameObject tempImage;
    bool inventoryMode;
    chest lastchest;


    public Sprite selected;
    public Sprite defaultspr;

    //ITEM E SHOULD CALL PICKPUTITEM()
    //PALYER ID RESETS

    private void Awake()
    {
        tempImage = gameObject.transform.Find("Canvas").Find("TempImage").gameObject;
        craftingslot = gameObject.transform.Find("Canvas").Find("craftslot").gameObject;
        shopcanvas = gameObject.transform.Find("shopcanvas").gameObject;
        camera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
        player = gameObject;
        sellmode = gameObject.transform.Find("shopcanvas").Find("sellmode").GetComponent<Button>();
        moneytext = gameObject.transform.Find("Canvas").Find("money").GetComponent<TMP_Text>();
        oxygencanvas = gameObject.transform.Find("Canvas").Find("energybar").Find("bar").gameObject;
        stats = gameObject.GetComponent<Stats>();
        sellmode.onClick.AddListener(setSellmode);
        //gameObject.transform.Find("Canvas").Find("inventory").gameObject.SetActive(false);
        updateMoney();
        //get inventory slots 
        var canvas = transform.Find("Canvas");
        var a =canvas.transform.GetComponentInChildren<Transform>();
        foreach(Transform child in a)
        {
            //look at child of child
            if (child.gameObject.tag == "InventorySlot")
            {
                inventory.Add(child.gameObject);
            }
        }
        foreach(Transform child in canvas.Find("inventory").GetComponentInChildren<Transform>())
        {
            if (child.tag == "InventorySlot")
            {
                inventory.Add(child.gameObject);
            }
        }
        selectedslot = inventory[0];
    }

    private void Update()
    {
        tempImage.transform.position = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.F))
        {
            var AIcontrol = GameObject.Find("AIcontrol");
            AIcontrol.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            followerList[0].GetComponent<AImovement>().followerCommand = true;
            followerList[0].GetComponent<AImovement>().aIDestination.target = AIcontrol.transform;
            followerList[0].GetComponent<AImovement>().AIPath.endReachedDistance = 0;
        }
        if (Input.GetMouseButtonDown(0)&&!isShopping&&selectedslot.GetComponent<inventorySlot>().storedItems.Count!=0)
        {
            if(selectedslot.GetComponent<inventorySlot>().storedItems[0].TryGetComponent<blueprint>(out blueprint blueprint))
            {
                crafting.Add(blueprint);
                var a = selectedslot.GetComponent<inventorySlot>().storedItems[0];
                selectedslot.GetComponent<inventorySlot>().dropItem();
                  Destroy(a);
                selectedslot.GetComponent<inventorySlot>().clearSlot(); 
            }
            else if (selectedslot.GetComponent<inventorySlot>().storedItems[0].GetComponent<pickle>().category == "weapon"&&stats.canAttack)
            {
                //this will be attack script for player
                var mscript = gameObject.GetComponent<movement>();
                var gun = selectedslot.GetComponent<inventorySlot>().storedItems[0];
                gun.GetComponent<weaponstats>().shoot(stats.enemylist,gameObject,mscript.gun.transform);
               
                //stats.attackset();

            }
            //else
            //{
            //    Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            //    mousePosition.z = 0;
            //    selectedslot.GetComponent<inventorySlot>().dropItem(mousePosition);
            //}
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gameObject.GetComponent<Stats>().energy -= Time.deltaTime*4;
            
            gameObject.GetComponent<movement>().speed = 12;
        }
        else
        {
            gameObject.GetComponent<movement>().speed = 6;
        }
        if (Input.GetKeyDown("g"))
        {
            if (followerList.Count > 0)
            {
                foreach(var follower in followerList)
                {
                    follower.GetComponent<AImovement>().isFollowingPlayer = !follower.GetComponent<AImovement>().isFollowingPlayer;
                    follower.GetComponent<AImovement>().owner = gameObject;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject.Find("Astarpath").GetComponent<savesystem>().SaveGameObjects();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject.Find("Astarpath").GetComponent<savesystem>().loadGameObjects();
        }
        if (Input.GetKeyDown("e"))
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, mousePosition - gameObject.transform.position, 2);
            var stats = player.GetComponent<playerStats>();
            foreach (var hit in hits)
            {   
                if(hit.transform.gameObject.TryGetComponent<car>(out car car))
                {
                    hit.transform.Find("Camera").gameObject.SetActive(true);
                }
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

                else if(hit.transform.gameObject.TryGetComponent<UpgradeMatic>(out UpgradeMatic upgrade))
                {
                    upgrade.enableCanvas();
                }

                if (chestUI.active)
                {
                    lastchest.closeChest();
                    lastchest = null;
                }
                if (hit.transform.gameObject.TryGetComponent<chest>(out chest chest))
                {
                    chest.openChest();
                    lastchest = chest;
                }
                
                if(hit.transform.TryGetComponent<upgradeBase>(out upgradeBase upgradeBase))
                {
                    upgradeBase.canvasToggle();
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
                        shopping.Find("Img").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                        shopping.Find("Img").GetComponent<Image>().color = item.GetComponent<SpriteRenderer>().color;
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
            if (!inventoryMode)
            {
                transform.Find("Canvas").Find("inventory").gameObject.active = true;
                inventoryMode = true;
            }
            else
            {
                transform.Find("Canvas").Find("inventory").gameObject.active = false;
                inventoryMode = false;
            }
           
        }
       
        if (Input.GetKeyDown("c"))
        {
            craftingToggle();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            chooseSlot(0);
            
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            chooseSlot(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            chooseSlot(2);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            chooseSlot(3);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            chooseSlot(4);
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            chooseSlot(5);
        }

        if (Input.GetKey(KeyCode.Alpha7))
        {
            chooseSlot(6);
        }

        if (Input.GetKey(KeyCode.Alpha8))
        {
            chooseSlot(7);
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            chooseSlot(8);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            var a = inventory.Find(x => x == selectedslot);
            if (inventory.IndexOf(a) + 1 <=8)
            {
                int newslot = inventory.IndexOf(a) + 1;
                chooseSlot(newslot);
            }
            else
            {
                int newslot = 0;
                chooseSlot(newslot);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            var a = inventory.Find(x => x == selectedslot);
            if(inventory.IndexOf(a) - 1 >= 0)
            {
                int newslot = inventory.IndexOf(a) - 1;
                chooseSlot(newslot);
            }
            else{
                int newslot = 8;
                chooseSlot(newslot);
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

    public void craftingToggle()
    {
        if (craftingUI.active)
        {
            craftingUI.active = false;
            foreach (Transform child in craftingUI.transform)
            {
                if(child.name!="closopen")
                Destroy(child.gameObject);
            }
            inventoryMode = false;
        }
        else
        {
            inventoryMode = true;

            float y = 200;
            foreach (blueprint bp in crafting)
            {

                var newcraft = Instantiate(craftingslot, craftingUI.transform);
                var material = newcraft.transform.Find("recipeslot");
                material.gameObject.SetActive(false);
                float x = -750;
                newcraft.transform.Find("outputslot").Find("output").gameObject.GetComponent<Image>().sprite = bp.output[0].GetComponent<SpriteRenderer>().sprite;
                newcraft.transform.Find("outputslot").Find("output").gameObject.GetComponent<Image>().color = bp.output[0].GetComponent<SpriteRenderer>().color;
                foreach (var ingredient in bp.materials)
                {

                    GameObject recipebox = Instantiate(material, newcraft.transform).gameObject;
                    recipebox.transform.position = new Vector2(newcraft.transform.position.x + x, newcraft.transform.position.y);
                    recipebox.transform.Find("recipe").GetComponent<Image>().sprite = ingredient.GetComponent<SpriteRenderer>().sprite;
                    recipebox.transform.Find("recipe").GetComponent<Image>().color = ingredient.GetComponent<SpriteRenderer>().color;
                    recipebox.SetActive(true);
                    x += 150;
                }
                newcraft.active = true;
                newcraft.transform.position = new Vector2(newcraft.transform.position.x, craftingUI.transform.position.y + y);
                newcraft.transform.Find("craftbutton").GetComponent<craftButton>().bp = bp;
                y -= 50;
            }
            craftingUI.active = true;
        }
    }
    public void templistClear()
    {
        tempitems.Clear();
        tempImage.active = false;
    }
    
    void chooseSlot(int index)
    {
        selectedslot.GetComponent<Image>().sprite = defaultspr;
        selectedslot = inventory[index];
        selectedslot.GetComponent<Image>().sprite= selected;
        var storeditm = selectedslot.GetComponent<inventorySlot>().storedItems;
        if (storeditm.Count != 0)
        {
            if (storeditm[0].GetComponent<pickle>().category == "weapon")
            {
                gameObject.GetComponent<movement>().gun.GetComponent<SpriteRenderer>().sprite =
               storeditm[0].GetComponent<SpriteRenderer>().sprite;
                gameObject.GetComponent<movement>().gun.GetComponent<SpriteRenderer>().flipX = true;
                stats.damage = storeditm[0].GetComponent<weaponstats>().damage;
            }
        }

        else {
            gameObject.GetComponent<movement>().gun.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    public void templistAdd(List<GameObject> itemstoadd)
    {
        print(tempitems.Count);
        if (tempitems.Count == 0)
        {
            tempitems.AddRange(itemstoadd);
            tempImage.GetComponent<Image>().sprite = itemstoadd[0].GetComponent<SpriteRenderer>().sprite;
            tempImage.GetComponent<Image>().color = itemstoadd[0].GetComponent<SpriteRenderer>().color;
            tempImage.active = true;
        }
        
    }

    
    void openShop()
    {
        shopcanvas.SetActive(!shopcanvas.activeSelf);
    }
    public void increaseScore(int scoreincrease)
    {
        score += scoreincrease;
        transform.Find("Canvas").transform.Find("score").GetComponent<TMP_Text>().text="Amogus: "+score.ToString();
    }

    #region Survival


    public void updateoxygen(float updatedoxygen)
    {
        oxygencanvas.GetComponent<Slider>().value= updatedoxygen;
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
    public void clearInventory()
    {
        foreach(var inventoryItem in inventory)
        {
            inventoryItem.GetComponent<inventorySlot>().clearSlot();
        }
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
                    List<GameObject> itemstoadd = new List<GameObject>();
                    itemstoadd.Add(itemtoadd);
                    item.GetComponent<inventorySlot>().additem(itemstoadd);
                    //item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                    //item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                    //var itemcount = item.transform.Find("Text (TMP)");
                    //int previousnum = int.Parse(itemcount.gameObject.GetComponent<TMP_Text>().text);
                    //itemcount.gameObject.GetComponent<TMP_Text>().text = (previousnum + 1).ToString();

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
                        List<GameObject> itemstoadd = new List<GameObject>();
                        itemstoadd.Add(itemtoadd);
                        item.GetComponent<inventorySlot>().additem(itemstoadd);
                        //item.GetComponent<inventorySlot>().storedItems.Add(itemtoadd);
                        //item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                        //item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                        //var itemcount = item.transform.Find("Text (TMP)");
                        //itemcount.gameObject.GetComponent<TMP_Text>().text = 1.ToString();
                        itemtoadd.active = false;
                        foundslot = true;
                        break;
                    }
                    else
                    {
                        List<GameObject> itemstoadd = new List<GameObject>();
                    itemstoadd.Add(itemtoadd);
                    item.GetComponent<inventorySlot>().additem(itemstoadd);
                        //item.GetComponent<inventorySlot>().storedItems.Add(itemtoadd);
                        //item.transform.Find("item").GetComponent<Image>().sprite = itemtoadd.GetComponent<SpriteRenderer>().sprite;
                        //item.transform.Find("item").GetComponent<Image>().color = itemtoadd.GetComponent<SpriteRenderer>().color;
                        //var itemcount = item.transform.Find("Text (TMP)");
                        //itemcount.gameObject.GetComponent<TMP_Text>().text = 1.ToString();
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
 