using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energysource : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Stats>().charging = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Stats>().charging = false;
        }
    }
}
