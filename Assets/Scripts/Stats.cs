using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string name;
    public string faction;
    public string race;

    public int health;
    public int damage;
    public float speed;
    public float basespeed;
    public int range;
    public List<Status> statuslist;
    public List<string> enemylist;

    public int oxygen = 120;
    public int maxox = 120;
    public bool breathable = true;
    public float maxtemp = 32.0f;
    public float mintemp = 5.0f;

    public float currenttemp = 18.0f;
    public float ambtemp = 0.0f;
    public List<float> templist;

    public bool needsair = true;
    public bool tempsensitive = true;
    private void Start()
    {
        templist.Add(0);
        if (needsair)
            statuslist.Add(new Status("breath", -1, 1));
        if (tempsensitive)
            statuslist.Add(new Status("temp", -1, 1));
            InvokeRepeating("statuscheck", 0, 1.0f);
    
    }

    void statuscheck()
    {
        foreach (Status status in statuslist)
        {
            status.applyEffect(status, gameObject.GetComponent<Stats>());
            //Debug.Log("statusname= " + status.stname);
        }
    }
}

public class Status:Stats
{
    public string stname;
    float duration;
    int strength;
    public Status(string n, float dur, int str)
    {
        stname = n;
        strength = str;
        duration = dur;
    }

    

    public bool applyEffect(Status status, Stats stats)
    {

        if (status.duration - Time.deltaTime > 0)
        {

            if (status.stname == "stun")
            {
                status.duration -= Time.deltaTime;
                stats.speed = 0;
            }

            duration -= 1;
            return true;
        }
        else if (duration == -1)
        {
            if (status.stname == "breath")
            {
                
                if (stats.breathable)
                {
                    if (stats.oxygen + 10 < stats.maxox)
                    {
                        stats.oxygen += 10;
                    }
                    else
                    {
                        stats.oxygen = stats.maxox;
                    }
                }
                else
                {

                    stats.oxygen--;
                }
                if (stats.gameObject.tag == "Player")
                {
                    var pstats = stats.gameObject.GetComponent<playerStats>();
                    pstats.updateoxygen(stats.oxygen);
                }

            }
            if(status.stname== "temp")
            {
                var newtemp=0.0f;
                foreach(float tempsource in stats.templist)
                {
                    newtemp += (stats.ambtemp + tempsource) * stats.templist.Count;
                }
                Debug.Log(newtemp);
            }

            return true;
        }
        else
        {
            if (status.stname == "stun")
            {
                stats.speed = stats.basespeed;
                stats.statuslist.Remove(status);
            }

            return false;
        }

    }
    public void applyEffect(int strength, string Statustype, GameObject target)
    {

    }
}

