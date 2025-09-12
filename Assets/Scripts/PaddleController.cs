using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int playerID;
    public float ySpeed = 200f;


    // Update is called once per frame
    void Update()
    {
        float input = GetAxisValue();       //Get value of input.
        MovePaddle(input);                  //Move this paddle.
    }

    private float GetAxisValue(){
        float axisValue = 0f;
        //If this is player 1, use input for player 1:
        if(playerID == 1){
            axisValue = Input.GetAxis("PaddlePlayer1");
        }
        //If this is player 2, use input for player 1:
        if(playerID == 2){
            axisValue = Input.GetAxis("PaddlePlayer2");
        }
        return axisValue;
    }

    private void MovePaddle(float axisValue){
        Vector2 velocity = rb.linearVelocity;
        velocity.y = axisValue * ySpeed;

        rb.linearVelocity = velocity;
    }
}
