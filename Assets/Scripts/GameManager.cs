using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool lastFail;
    public int leftPlayerState = 0, rightPlayerState = 0, maxState = 4;
    public Snake snake;

    public static Vector2 BottomLeft;
    public static Vector2 TopRight;

    private Paddle rightPaddle, leftPaddle;

    private bool isRightPaddle = true;
    private bool isLeftPaddle = false;

    // Start is called before the first frame update
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        snake = Instantiate(Resources.Load<Snake>("Snake"));
        snake.GetComponent<Snake>().gameManager = this;
//        leftPlayerState = minState;
//        rightPlayerState = minState;
        rightPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));
        leftPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));

        rightPaddle.Init(isRightPaddle);
        leftPaddle.Init(isLeftPaddle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreasePaddle(bool playerSide)
    {
        Paddle paddle;
        int state;
        if (playerSide)
        {
            paddle = rightPaddle;
            state = rightPlayerState;
        }
        else
        {
            paddle = leftPaddle;
            state = leftPlayerState;
        }
        Vector2 topPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + (paddle.transform.localScale.y * 0.75f + state * 0.5f));
        Vector2 bottomPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y - (paddle.transform.localScale.y * 0.75f + state * 0.5f));
        Paddle addedTop = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        Paddle addedBottom = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        addedTop.Init(playerSide);
        addedBottom.Init(playerSide);
        addedBottom.transform.SetParent(paddle.transform);
        addedTop.transform.SetParent(paddle.transform);
        addedTop.transform.position = topPos;
        addedBottom.transform.position = bottomPos;
    }

    public void PointHandler(bool playerMiss)
    {
        lastFail = playerMiss;
        if (playerMiss)
        {
            if (rightPlayerState + 1 >= maxState)
            {
                //goto left player won screen
                Time.timeScale = 0;
            }
            IncreasePaddle(playerMiss);
            rightPlayerState += 1;
            //resetSnake();
        }
        else
        {
            if (leftPlayerState + 1 >= maxState)
            {
                //goto right player won screen
                Time.timeScale = 0;
            }
            IncreasePaddle(playerMiss);
            leftPlayerState += 1;
            //resetSnake();
        }

        
        
    }

}
