using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{
    public int speed=6;
    public Rigidbody2D rb;
    public Animator animator;
    //public GameObject attackpoint;


    // Start is called before the first frame update

    void start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
      
         
         //GameObject.Find("AstarPath").GetComponent<AIqueue>().players.Add(gameObject);         
        
    }
    // Update is called once per frame
    void Update()
    {
       
            
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
  

            //animator.SetFloat("Speed", Mathf.Abs(inputY) / 2 + Mathf.Abs(inputX) / 2);
            //if (Mathf.Abs(inputY) + Mathf.Abs(inputX) > 0.01f)
            //{
            //    animator.SetFloat("LastHorizontal", inputX);
            //    animator.SetFloat("LastVertical", inputY);
            //    animator.SetFloat("Horizontal", inputX);
            //    animator.SetFloat("Vertical", inputY);
            //}


            //float x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputX * 6, inputY * 6);

            //if (inputX != 0 || inputY != 0)
                //attackpoint.transform.position = new Vector2(Player.transform.position.x + Sign(inputX), Player.transform.position.y + Sign(inputY));
            //rb.AddForce(Player.transform.TransformVector(new Vector2(inputX, inputY)) * 2.0f); //snoww!!!
        }
    
        static int Sign(float number)
        {
            if (number < 0)
            {
                return -1;
            }
            else if (number > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
}


    //Unity thinks 0 is a positive number so I had to write my own function
   

    



