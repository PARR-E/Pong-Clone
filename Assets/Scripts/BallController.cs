//Jared Crow
//ASK Dr. ZHENG ABOUT GETTING VS TO RECOGNIZE UNITY

//using System;                     <- Don't use this one. If conflicts w/ Unity's imports.
//using System.Diagnostics;         <- Same with this.
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    //Initializations:
    public Rigidbody2D rb;
    public GameManager gameManager;
    public float ballSpeed = 10f;          //Making the variable public lets you edit it in Unity's editor.
    public float maxInitialAngle = 3.0f;   //
    //private float startX = 0f;
    private float startY = 4f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetBall();
        //Invoke("Serve", 2);     //Call Serve method after 4 seconds.


    }

    // Update is called once per frame:
    void Update()
    {
    

    }

    public void Serve() {
        //Find the Rigidbody component:
        //rb = GetComponent<Rigidbody2D>();     //Can be done in inspector, so this isn't needed no more.

        //Generate random (x, y) vector:
        Vector2 direction = Vector2.left;
        if (Random.value > 0.5) { 
            direction = Vector2.right;    
        }
        direction.y = Random.Range(-maxInitialAngle, maxInitialAngle);

        //direction.x = Random.Range(maxInitialAngle, maxInitialAngle);
        //Vector2 direction = new Vector2(x, y);
        rb.linearVelocity = direction * ballSpeed;          //Final velocity is a random speed horizontally and vertically.


        //rigidboyd.linerVelocity(x, y)
    }

    //Scoring:
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision != null){
            gameManager.SetScores(collision.tag);
        }
        if(!gameManager.CheckWin()){
            ResetBall();
            Invoke("Serve", 1);     //Give the player some reaction time.
        }
    }

    //Resetting ball position:
    private void ResetBall()
    {
        Vector2 direction = Vector2.zero;
        direction.y = (Random.Range(-startY, startY));
        rb.transform.position = direction;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        float spdGain = 1.1f;

        //collision.colider.GetComponent(PaddleController);
        PaddleController paddle = collision.collider.GetComponent<PaddleController>();

        //if(paddle != null){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * spdGain, rb.linearVelocity.y * spdGain); 
        //}
    }
}
