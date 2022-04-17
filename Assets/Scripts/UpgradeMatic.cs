using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeMatic : MonoBehaviour
{
    public GameObject robot;
    GameObject player;
    GameObject canvas;
    GameObject healthtext;
    GameObject dmgtext;
    GameObject astext;

    private void Start()
    {
        player = GameObject.Find("Player");
        canvas = transform.Find("Canvas").gameObject;
        
    }

    public void increaseDamage()
    {
        if (player.GetComponent<playerStats>().money - 50 > 0)
        {
            player.GetComponent<playerStats>().money -= 50;
            player.GetComponent<playerStats>().updateMoney();

            robot.GetComponent<Stats>().maxhealth+=5;
            robot.GetComponent<Stats>().health += 5;
        }
    }
    public void increaseHP()
    {
        if (player.GetComponent<playerStats>().money - 50 > 0)
        {
            player.GetComponent<playerStats>().money -= 50;
            player.GetComponent<playerStats>().updateMoney();
            robot.GetComponent<Stats>().damage += 5;
        }
    }
    public void increaseAS()
    {
        if (player.GetComponent<playerStats>().money - 50 > 0)
        {
            player.GetComponent<playerStats>().money -= 50;
            player.GetComponent<playerStats>().updateMoney();
            robot.GetComponent<Stats>().attackspeed += 0.1f;
        }
    }

    public void enableCanvas()
    {
        canvas.SetActive(!canvas.activeSelf);
    }
}
