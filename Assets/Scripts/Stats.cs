using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string name;
    public string faction;
    public string race;

    public int health;
    public int maxhealth;
    public int damage;
    public float speed;
    public float basespeed;
    public int range;
    public List<Status> statuslist = new List<Status>();
    public List<string> enemylist;
    public bool canAttack = true;
    public float attackspeed=1.0f;

    public int oxygen = 120;
    public int maxox = 120;
    public bool breathable = true;
    public float maxtemp = 32.0f;
    public float mintemp = 5.0f;

    public float currenttemp = 18.0f;
    public float ambtemp = 0.0f;
    public List<float> templist;

    public bool needsair = true;
    public bool airprotected = false;
    public bool tempsensitive = true;

    public GameObject helmet;
    public GameObject body;
    public GameObject feet;

    public GameObject whattodrop;
    bool usedefault = false;
    int goldvalue = 50;

    private void Start()
    {
        if (whattodrop == null)
        {
            whattodrop = GameObject.Find("coin");
            usedefault = true;
        }

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

   public void attackset(){
        canAttack = !canAttack;
        Invoke("attackset", attackspeed);
    }

    public void equipHelmet(GameObject helmettoequip)
    {
        helmet = helmettoequip;
    }

    public void unEquipHelmet()

    {
        helmet = null;
    }

    public void DamageOrKill(int damage, GameObject ItemToDealDamage, float knockback, GameObject attacker)
    {
        //if health wont drop to or below zero after attack
        if (health - damage > 0)
        {
            //decrease health by damage
            health -= damage;
            Vector2 knockbackDirection = new Vector2(ItemToDealDamage.transform.position.x - attacker.transform.position.x, ItemToDealDamage.transform.position.y - attacker.transform.position.y).normalized;
            //ItemToDealDamage.GetComponent<Rigidbody2D>().AddForce(knockback*knockbackDirection*100);
           

            Status a = new Status("stun",1,1);
            statuslist.Add(a);


        }
        else
        {
            droploot(gameObject.transform.position);
            Destroy(ItemToDealDamage.gameObject);
        }
    }

    void droploot(Vector2 position)
    {
        var drop = Instantiate(whattodrop, position, gameObject.transform.rotation);
        if (usedefault)
        {
            drop.GetComponent<coinScript>().value = goldvalue;
        }
    }
}

public class Status
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

                else if (stats.helmet != null)
                {

                    if (stats.helmet.TryGetComponent<pickle>(out pickle pickle))
                    {
                        
                        if (pickle.itemperks != null)
                        {
                            if (stats.oxygen + 1 < stats.maxox)
                            {
                                stats.oxygen += 1;

                            }
                            else
                            {
                                stats.oxygen = stats.maxox;

                            }
                            stats.helmet.GetComponent<pickle>().damageItem(1);
                        }

                    }


                    else
                    {
                        stats.oxygen--;
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
            if (status.stname== "temp")
            {
                var newtemp=0.0f;
                foreach(float tempsource in stats.templist)
                {
                    newtemp += (stats.ambtemp + tempsource) * stats.templist.Count;
                }
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

