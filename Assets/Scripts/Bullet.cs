using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public List<string> Enemylist=new List<string>();
    public GameObject shooter;
    public int damage;
    bool nothit = true;
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity=transform.up*20;
        Invoke("plsdontflyforever", 10);

    }
    public void setEnemylist(List<string> EnemylistToset)
    {
        Enemylist = EnemylistToset;
    }
    public void setShooter(GameObject shooterToSet)
    {
        shooter = shooterToSet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
            if (Enemylist.Contains(collision.gameObject.tag))
            {
                if (nothit)
                {
                    nothit = false;
                    collision.gameObject.GetComponent<Stats>().DamageOrKill(damage, collision.gameObject, 5, shooter);
                    Destroy(gameObject);
                }
            }
       
    }


    private void plsdontflyforever()
    {
        Destroy(gameObject);
    }
}
            