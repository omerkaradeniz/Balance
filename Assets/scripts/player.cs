using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public  bool mouseDown;
    public  float timeMouseDown;


    GameObject left, right;
    public float speed;
    Rigidbody2D rb;

    bool leftCollision, rightCollision = false;

    void Start()
    {

        left = GameObject.FindWithTag("left");
        right = GameObject.FindWithTag("right");
        rb = GetComponent<Rigidbody2D>();

    }



    public void basili()
    {

        mouseDown = true;
    }

    public void cekildi()
    {
        mouseDown = false;
        Invoke("se", 0.6f);
       // timeMouseDown = 0;
    }

    void se()
    {
        timeMouseDown = 0;
    }



    void Update()
    {
       
            


        if (rightCollision)
            {

            if (mouseDown)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                timeMouseDown += Time.deltaTime;

            }


            if (!mouseDown)
            {
                rb.AddForce(new Vector2(-timeMouseDown, 20 * timeMouseDown));
                // rb.constraints = RigidbodyConstraints2D.None;
                //transform.Translate(new Vector2(-1 * Time.deltaTime, 2 * Time.deltaTime));
                // rb.AddForceAtPosition(new Vector2(-timeMouseDown, 20 * timeMouseDown), new Vector2(Screen.width/3,Screen.height-2));
                rb.gravityScale = 1f;
            }


            left.transform.Translate(new Vector2(0, speed * Time.deltaTime));
                transform.SetParent(right.transform);
                right.transform.Translate(new Vector2(0, -speed * Time.deltaTime));

/*
            if (Input.GetKey(KeyCode.A))
            {
                rb.constraints = RigidbodyConstraints2D.None;
                //transform.Translate(new Vector2(-1 * Time.deltaTime, 2 * Time.deltaTime));
                rb.AddForce(new Vector2(-1, 8));
                rb.gravityScale = 0.4f;
            }

    */

        }


            if (leftCollision)
            {


            if (mouseDown)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                timeMouseDown += Time.deltaTime;

            }


            if (!mouseDown)
            {

                // rb.constraints = RigidbodyConstraints2D.None;
                //transform.Translate(new Vector2(-1 * Time.deltaTime, 2 * Time.deltaTime));
                rb.AddForce(new Vector2(timeMouseDown, 20 * timeMouseDown));
                rb.gravityScale = 1f;
            }


            left.transform.Translate(new Vector2(0, -speed * Time.deltaTime));
                transform.SetParent(left.transform);
                right.transform.Translate(new Vector2(0, speed * Time.deltaTime));
/*
                if (Input.GetKey(KeyCode.A))
                {
                rb.constraints = RigidbodyConstraints2D.None;
                //transform.Translate(new Vector2(1 * Time.deltaTime, 2 * Time.deltaTime));
                rb.AddForce(new Vector2(1, 8));
                rb.gravityScale = 0.4f;
                }

    */

            }
    


        if (!leftCollision && !rightCollision)
        {
            transform.Translate(new Vector2(0, -5 * speed * Time.deltaTime));
        }

    
    }




    void OnCollisionEnter2D(Collision2D col)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        
        if (col.gameObject.CompareTag("left"))
        {
            leftCollision = true;
            rightCollision = false;
        }

        if (col.gameObject.CompareTag("right"))
        {
            rightCollision = true;
            leftCollision = false;
            
        }


        if (col.gameObject.CompareTag("gameOver"))
        {
            SceneManager.LoadScene("main");

        }
    }


   

}
