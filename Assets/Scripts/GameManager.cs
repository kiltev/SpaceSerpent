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
//    Color32 color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);  //White Color in Hex
    Color orangeStart = new Color32(255,121, 0, 255); //FF7900
    Color orangeEnd = new Color32(255, 216, 0, 255);
    Color pinkStart = new Color32(255, 0, 255, 255); //FF00FF
    Color pinkEnd = new Color32(0, 155, 255, 255);
    Color greenStart = new Color32(0, 255, 12, 255); //00FF0C
    Color greenEnd = new Color32(0, 255, 0, 200);

    private float duration = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        snake = Instantiate(Resources.Load<Snake>("SnakeHead"));
        snake.GetComponent<Snake>().gameManager = this;
//        leftPlayerState = minState;
//        rightPlayerState = minState;
        rightPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrickR"));
        leftPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrickL"));
        rightPaddle.Init(isRightPaddle);
        leftPaddle.Init(isLeftPaddle);
//        rightPaddle.transform.Rotate(0,0,180);
        leftPaddle.gameObject.tag = "LPaddle";
        rightPaddle.gameObject.tag = "RPaddle";
//        rightPaddle.GetComponent<Renderer>().material.SetColor("_Color", color);
//        leftPaddle.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    // Update is called once per frame
    void Update()
    {

        // manage colors for paddles :

        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rightPaddle.GetComponent<Renderer>().material.color = Color.Lerp(orangeStart, orangeEnd, lerp); // color right paddle
        Renderer[] rightChildrenRenderer = rightPaddle.GetComponentsInChildren<Renderer>();
        foreach (var r in rightChildrenRenderer)
        {
            r.material.color = Color.Lerp(orangeStart, orangeEnd, lerp); // color the children of right paddle the same colors
        }
        leftPaddle.GetComponent<Renderer>().material.color = Color.Lerp(pinkStart, pinkEnd, lerp);
        Renderer[] leftChildrenRenderer = leftPaddle.GetComponentsInChildren<Renderer>();
        foreach (var r in leftChildrenRenderer)
        {
            r.material.color = Color.Lerp(pinkStart, pinkEnd, lerp); // color the children of right paddle the same colors
        }

        //manage snake colors:
        Color color1, color2;
        int idx = 1;
        if (snake.lastTarget == 1) //if snake hit the right paddle last
        {
            color1 = orangeStart;
            color2 = orangeEnd;
        }
        else if(snake.lastTarget == 2)
        {
            snake.GetComponent<Renderer>().material.color = Color.Lerp(pinkStart, pinkEnd, lerp); //color like left paddle
            foreach (var bodyPart in snake.bodyParts)
            {
                bodyPart.GetComponent<Renderer>().material.color = Color.Lerp(pinkStart, pinkEnd, lerp); // color the children of right paddle the same colors
            }
            color1 = pinkStart;
            color2 = pinkEnd;
        }
        else
        {
            color1 = greenStart;
            color2 = greenEnd;
        }
        snake.GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, lerp); // color like right paddle
        foreach (var bodyPart in snake.bodyParts)
        {
            Color reduceAlpha = new Color32(0, 0, 0, 35);
            if (idx <= 4)
            {
                color1 -= reduceAlpha;
                color2 -= reduceAlpha;
            }
            bodyPart.GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, lerp); // color the children of right paddle the same colors
            idx++;
        }
    }
    public void IncreasePaddle(bool playerSide)
    {
        Paddle paddle;
        string sideTop;
        string sideBottom;
        int state;
        if (playerSide)
        {
            paddle = rightPaddle;
            state = rightPlayerState;
            sideTop = "PaddleBrickRTop";
            sideBottom = "PaddleBrickRBottom";
        }
        else
        {
            paddle = leftPaddle;
            state = leftPlayerState;
            sideTop = "PaddleBrickLTop";
            sideBottom = "PaddleBrickLBottom";
        }

        Vector2 topPos, bottomPos;
        
        PaddleBrick addedTop = Instantiate(Resources.Load<PaddleBrick>(sideTop));
        PaddleBrick addedBottom = Instantiate(Resources.Load<PaddleBrick>(sideBottom));
        topPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + (paddle.transform.localScale.y *2  + state * addedTop.transform.localScale.y));
        bottomPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y - (paddle.transform.localScale.y *2 + state * addedBottom.transform.localScale.y));
        addedTop.Init(playerSide, paddle);
        addedBottom.Init(playerSide, paddle);
//        paddle.height += addedTop.height * 2;
        addedTop.transform.position = topPos;
        addedBottom.transform.position = bottomPos;
        if (paddle.isRight)
        {
//            addedBottom.transform.Rotate(0, 0, 180);
//            addedTop.transform.Rotate(0, 0, 180);
            addedBottom.gameObject.tag = "RPaddle";
            addedTop.gameObject.tag = "RPaddle";
        }
        else
        {
            addedBottom.gameObject.tag = "LPaddle";
            addedTop.gameObject.tag = "LPaddle";
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
