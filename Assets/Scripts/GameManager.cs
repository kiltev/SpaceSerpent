using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        Paddle rightPaddle =  Instantiate(Resources.Load<Paddle>("Paddle"));
        Paddle leftPaddle =  Instantiate(Resources.Load<Paddle>("Paddle"));

        rightPaddle.Init(true);
        leftPaddle.Init(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
