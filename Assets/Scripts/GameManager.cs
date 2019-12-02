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
    Color colorStart = Color.red;
    Color colorEnd = Color.green;

    private float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        snake = Instantiate(Resources.Load<Snake>("SnakeHead"));
        snake.GetComponent<Snake>().gameManager = this;
//        leftPlayerState = minState;
//        rightPlayerState = minState;
        rightPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));
        leftPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));
        rightPaddle.Init(isRightPaddle);
        leftPaddle.Init(isLeftPaddle);
        rightPaddle.transform.Rotate(0,0,180);
//        Color32 color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);  White Color in Hex
//        rightPaddle.GetComponent<Renderer>().material.SetColor("_Color", color);
//        leftPaddle.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rightPaddle.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp); // color right paddle
        Renderer[] rightChildrenRenderer = rightPaddle.GetComponentsInChildren<Renderer>();
        foreach (var r in rightChildrenRenderer)
        {
            r.material.color = Color.Lerp(colorStart, colorEnd, lerp); // color the children of right paddle the same colors
        }
        leftPaddle.GetComponent<Renderer>().material.color = Color.Lerp(colorEnd, colorStart, lerp);
        Renderer[] leftChildrenRenderer = leftPaddle.GetComponentsInChildren<Renderer>();
        foreach (var r in leftChildrenRenderer)
        {
            r.material.color = Color.Lerp(colorEnd, colorStart, lerp); // color the children of right paddle the same colors
        }
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

        Vector2 topPos, bottomPos;
        Paddle addedTop = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        Paddle addedBottom = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        topPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + (paddle.transform.localScale.y *2  + state * addedTop.transform.localScale.y));
        bottomPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y - (paddle.transform.localScale.y *2 + state * addedBottom.transform.localScale.y));
        addedTop.Init(playerSide);
        addedBottom.Init(playerSide);
//        paddle.height += addedTop.height * 2;
        addedTop.transform.position = topPos;
        addedBottom.transform.position = bottomPos;
        if (paddle.isRight)
        {
            addedBottom.transform.Rotate(0, 0, 180);
            addedTop.transform.Rotate(0, 0, 180);
        }
        addedBottom.transform.SetParent(paddle.transform);
        addedTop.transform.SetParent(paddle.transform);
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
