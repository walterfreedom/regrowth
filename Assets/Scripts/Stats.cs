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
    public float maxspeed;
    public float speed;
    public int range;
    public List<Status> statuslist;
    public List<string> enemylist;
    
}

public class Status
{
    string name;
    float duration;
    string strength;
    public Status(string n, float dur, string str)
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

            return true;
        }
        else
        {
            //duration = 0;
            //a.speed = baseSpeed;

            //return false;
        }


        return true;
    }
    public void applyEffect(int strength, string Statustype, GameObject target)
    {

    }
}

