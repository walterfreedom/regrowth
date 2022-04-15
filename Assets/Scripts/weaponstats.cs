using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponstats: MonoBehaviour
{
    public int damage = 10;
    public GameObject bullet;
    public void shoot(List<string> enemylist,GameObject shooter,Transform attackposition)
    {
        var newbullet= Instantiate(bullet, attackposition.position,attackposition.rotation);
         newbullet.GetComponent<Bullet>().Enemylist.AddRange(enemylist);
         newbullet.GetComponent<Bullet>().setShooter(gameObject);
        newbullet.GetComponent<Bullet>().damage = damage;
         newbullet.active = true;
    }
}
