using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;              //Used for collisions.
    public int playerID;                //Equals 1 if this is the left paddle, or 2 if this is the right paddle.
    public int ySpeed = 10;         //Speed of the paddle (gets overriden in Unity editor).
    private Vector2 startPosition;
    public BallController ball;
    public float CPUaxis = 0.0f;
    public float CPUaxisIncrement = 0.0001f;    //This is overriden in Unity editor.
    private int CPUdirection = 0;               //1 is up, -1 is down.
    private int lastCPUdirection = 0;

    private void OnEnable()
    {
        startPosition = transform.position;
        //GameManager.Instance.OnReset += ResetPaddlePosition;
    }


    // Update is called once per frame:
    void Update()
    {
        if (playerID == 2 && GameManager.Instance.playMode == GameManager.PlayMode.PlayerVsCPU)
        {
            MoveCPU();
        }
        else
        {
            float input = GetAxisValue();       //Get value of input.
            MovePaddle(input);                  //Move this paddle.
        }
    }

    //Getting input for this paddle, depending on it's ID (left or right):
    private float GetAxisValue()
    {
        float axisValue = 0f;
        //If this is player 1, use input for player 1:
        if (playerID == 1)
        {
            axisValue = Input.GetAxis("PaddlePlayer1");
        }
        //If this is player 2, use input for player 1:
        if (playerID == 2)
        {
            axisValue = Input.GetAxis("PaddlePlayer2");
        }

        return axisValue;
    }

    //Applying transformations to this paddle's position when this function is called:
    private void MovePaddle(float axisValue)
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = axisValue * ySpeed;

        rb.linearVelocity = velocity;
    }

    //Move the CPU player.
    private void MoveCPU()
    {
        Vector2 ballPosition = ball.transform.position;
        //transform.position = new Vector2(startPosition.x, ballPosition.y);

        if (lastCPUdirection != CPUdirection)
        {
            CPUaxis = CPUaxis / 2;
            Debug.Log("Changing direction");
        }

        if (transform.position.y < ball.transform.position.y)
        {
            CPUdirection = 1;
            CPUaxis += CPUaxisIncrement;
        }
        else if (transform.position.y > ball.transform.position.y)
        {
            CPUdirection = -1;
            CPUaxis -= CPUaxisIncrement;
        }

        //Comment out this section to put the CPU on crack:
        if (CPUaxis > 1.0f)
        {
            CPUaxis = 1.0f;
        }
        else if (CPUaxis < -1.0f)
        {
            CPUaxis = -1.0f;
        }

        lastCPUdirection = CPUdirection;

        //Debug.Log("CPUaxis" + CPUaxis);
        MovePaddle(CPUaxis);
    }
    
}
