using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airQuality : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<playerStats>(out playerStats stats))
        {
            stats.breathable = 0;
        }
        print("aaaa");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<playerStats>(out playerStats stats))
        {
            stats.breathable = 1;
        }
    }
}
