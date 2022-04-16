using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickle : MonoBehaviour
{
    public int stacksize = 4;
    public string itemname;
    public int value = 100;
    public bool infiniteitemspawn;
    public string category;
    public int durability = 100;
    public List<string> itemperks;
   
    public void damageItem(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
