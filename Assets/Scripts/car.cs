using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public Rigidbody2D rb;
    public int drip = 0;
    public AudioClip whatsappsoundeffect;
    float speed = 6;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 v3Force = speed * transform.right;
            rb.velocity=v3Force;
            print("araba");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation *= Quaternion.Euler(Vector3.forward * -90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation *= Quaternion.Euler(Vector3.forward * 90*Time.deltaTime);
            print("araba");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Stats>(out Stats stats))
        {
            if (stats.health < 5000)
                drip++;
            stats.DamageOrKill(5000, collision.gameObject, 1, gameObject);
        }
        if(drip<=3)
        gameObject.GetComponent<AudioSource>().PlayOneShot(whatsappsoundeffect);
        else if(drip==4)
        {
            gameObject.GetComponent<AudioSource>().Play();
            speed = 16;
        }

    }


}
