using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickle : MonoBehaviour
{
    public string id="";
    public int stacksize = 4;
    public string itemname;
    public int value = 100;
    public bool infiniteitemspawn;
    public string category;
    public int durability = 100;
    public List<string> itemperks;
    public int slotindex;
    public string ownerID;

    private void Awake()
    {
        
            id = System.Guid.NewGuid().ToString();
        GameObject.Find("Astarpath").GetComponent<savesystem>().saveables.Add(id,gameObject);
    }
    public void damageItem(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    public picklesave createSaveData()
    {
        print(this.ownerID);
        return new picklesave(this);
    }
    private void OnDestroy()
    {
        GameObject.Find("Astarpath").GetComponent<savesystem>().saveables.Remove(id);
    }

    public void loadData(picklesave data)
    {
        if (data.ownerID != "")
        {
            ownerID = data.ownerID;
            gameObject.active = false;
           
            var owner = GameObject.Find("Astarpath").GetComponent<savesystem>().saveables[ownerID];
            if (owner.tag=="Player")
            {
                List<GameObject> item = new List<GameObject>();
                item.Add(gameObject);
                owner.GetComponent<playerStats>().inventory[data.slotindex].GetComponent<inventorySlot>().additem(item);
            }
            else
            {
                owner.GetComponent<chest>().storeditems[data.slotindex].data.Add(gameObject);   
            }
        }

        else
        {
            gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        }
    }
}
[System.Serializable]
public class picklesave
{
    public string itemname;
    public int slotindex;
    public string ownerID;
    public float[] position = new float[3];
    public picklesave(pickle pickle)
    {
        itemname = pickle.gameObject.name;
        slotindex = pickle.slotindex;
        ownerID = pickle.ownerID;
        position[0] = pickle.gameObject.transform.position.x;
        position[1] = pickle.gameObject.transform.position.y;
        position[2] = pickle.gameObject.transform.position.z;
    }
}
