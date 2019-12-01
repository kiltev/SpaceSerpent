using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int leftPlayerState, rightPlayerState, minState = 2, maxState = 8;
    public Snake snake;
    public Paddle paddle;

    public static Vector2 BottomLeft;
    public static Vector2 TopRight;
    // Start is called before the first frame update
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        snake = Instantiate(Resources.Load<Snake>("Snake"));
        leftPlayerState = minState;
        rightPlayerState = minState;
        Paddle rightPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));
        Paddle leftPaddle =  Instantiate(Resources.Load<Paddle>("PaddleMainBrick"));

        rightPaddle.Init(true, rightPlayerState);
        leftPaddle.Init(false, leftPlayerState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
