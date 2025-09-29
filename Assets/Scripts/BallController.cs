//Jared Crow

//using System;                     <- Don't use this one. If conflicts w/ Unity's imports.
//using System.Diagnostics;         <- Same with this.
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    //Initializations:
    public Rigidbody2D rb;                  //Used for collisions.
    public float ballSpeed = 8f;           //Making the variable public lets you edit it in Unity's editor.
    public float maxInitialAngle = 3.0f;    //Max angle in radians the ball can be lanched at.
    private float startY = 4f;              //Starting height of the ball.

    //Subscriptions:
    private void OnEnable(){
        GameManager.Instance.OnScoreChanged += Reset;        //Observer.
        GameManager.Instance.OnGameStart += Serve;    //Replaces ball.Serve(); from GameUIController.
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetBall();
    }

    //Making sure ball doesn't get too far out of bounds:
    void Update(){
        if(rb.position.y > 10 || rb.position.y < -10){
            ResetBall();
            Invoke("Serve", 1);
        }
    }

    //Calls the ResetBall() method. Needed because ResetBall() has different parameters than OnScoreChanged.
    public void Reset(int unused1, int unused2){    //OnScoreChanged requires 2 parameters, regardless of if they are used or not.
        ResetBall();
    }

    public void Serve() {
        //Generate random (x, y) vector:
        Vector2 direction = Vector2.left;
        if (Random.value > 0.5) { 
            direction = Vector2.right;    
        }
        direction.y = Random.Range(-maxInitialAngle, maxInitialAngle);

        //Final velocity is a random speed horizontally and vertically:
        rb.linearVelocity = direction * ballSpeed;   
    }

    //Scoring:
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision != null){
            GameManager.Instance.SetScores(collision.tag);      //Subscriber version of: gameManager.SetScores(collision.tag);
        }
        if(!GameManager.Instance.CheckWin()){                   //Subscriber version of: !gameManager.CheckWin()
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

    //When ball collides with paddles, or a wall:
    private void OnCollisionEnter2D(Collision2D collision){
        
        float spdGain = 1.1f;

        //collision.colider.GetComponent(PaddleController);
        PaddleController paddle = collision.collider.GetComponent<PaddleController>();

        rb.linearVelocity = new Vector2(rb.linearVelocity.x * spdGain, rb.linearVelocity.y * spdGain); 
    }

    //Unsubscribe to avoid leaks:
    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= Reset;
        GameManager.Instance.OnGameStart -= Serve;   
    }
}