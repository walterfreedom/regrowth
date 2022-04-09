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

    public bool needsair = true;

    private void Start()
    {
        if (needsair)
            statuslist.Add(new Status("breath", -1, 1));
        InvokeRepeating("statuscheck", 0, 1.0f);
    }

    void statuscheck()
    {
        foreach (Status status in statuslist)
        {
            status.applyEffect(status, gameObject.GetComponent<Stats>());
        }
    }
}

public class Status:Stats
{
    string name;
    float duration;
    int strength;
    public Status(string n, float dur, int str)
    {
        name = n;
        strength = str;
        duration = dur;
    }

    

    public bool applyEffect(Status status, Stats stats)
    {

        if (status.duration - Time.deltaTime > 0)
        {

            if (status.name == "stun")
            {
                status.duration -= Time.deltaTime;
                stats.speed = 0;
            }

            duration -= 1;
            return true;
        }
        else if (duration == -1)
        {
            if (status.name == "breath")
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
               if(gameObject.tag=="Player")
                {
                    var pstats = gameObject.GetComponent<playerStats>();
                    pstats.updateoxygen(oxygen);
                }
            }
            if(status.name== "temp")
            {

            }

            return true;
        }
        else
        {
            if (status.name == "stun")
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

