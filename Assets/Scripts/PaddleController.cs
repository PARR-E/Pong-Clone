using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;              //Used for collisions.
    public int playerID;                //Equals 1 if this is the left paddle, or 2 if this is the right paddle.
    public int ySpeed = 10;         //Speed of the paddle.


    // Update is called once per frame:
    void Update()
    {
        float input = GetAxisValue();       //Get value of input.
        MovePaddle(input);                  //Move this paddle.
    }

    //Getting input for this paddle, depending on it's ID (left or right):
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

    //Applying transformations to this paddle's position when this function is called:
    private void MovePaddle(float axisValue){
        Vector2 velocity = rb.linearVelocity;
        velocity.y = axisValue * ySpeed;

        rb.linearVelocity = velocity;
    }
}
