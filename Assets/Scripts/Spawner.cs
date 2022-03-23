using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    shopQueue shopQueue;
    public GameObject customer;
    bool canspawn=true;
    public int day;
    public int waveamount=1;
    GameObject daycounter;
    float interval=5.0f;
    public float waittime = 10.0f;
    bool isday;
    public int customersToServe=0;
    private void Start()
    {
        daycounter = GameObject.Find("day");
        shopQueue = GameObject.Find("shop1").GetComponent<shopQueue>();
    }
  
    public void nextday()
    {

        waveamount = (int)(4 + 2* day) ;
        customersToServe = waveamount;
        isday = true;
        interval -= 0.5f;
        InvokeRepeating("spawncustomer", interval,interval);

        daycounter.GetComponent<TMP_Text>().text = "Day: "+day.ToString();
        waittime += 1.1f;
        GameObject.Find("skipday").gameObject.active = false;
    }
    void spawncustomer()
    {
        if (isday)
        {
            if (shopQueue.customers.Count < 5 && waveamount > 0)
            {
                shopQueue.customers.Add(Instantiate(customer));
                waveamount -= 1;
            }   
        }
    }
    public void servedCustomerCheck()
    {
        customersToServe--;
        if (customersToServe == 0)
        {
            day++;
            isday = false;
            GameObject.Find("Player").transform.Find("Canvas").Find("skipday").gameObject.active = true;
        } 
    }
}
