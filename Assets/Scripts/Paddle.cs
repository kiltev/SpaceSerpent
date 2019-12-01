using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D playerPaddle;
    private float _speed;

    private float _height;

    private string _input;
    public int size;
    public bool isRight;
        // Start is called before the first frame update
    void Start()
    {
        playerPaddle = GetComponent<Rigidbody2D>();
        _speed = 800f;
        _height = transform.localScale.y;
    }

    public void Init(bool isRightPaddle, int size)
    {
        this.size = size;
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;
        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.TopRight.x,0) + (Vector2.left * transform.localScale.x);
        }
        else
        {
            pos = new Vector2(GameManager.BottomLeft.x,0) + (Vector2.right * transform.localScale.x);
        }
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float move = 0f;
        if (isRight)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                move =  Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                move =  -Time.deltaTime * _speed;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                move =  Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                move =  -Time.deltaTime * _speed;
            }
        }

        if (transform.position.y < GameManager.BottomLeft.y + _height / 1.5f && move < 0f)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.TopRight.y - _height / 1.6f && move > 0f)
        {
            move = 0;
        }
        playerPaddle.velocity = move * Vector2.up;
    }

    void increasePaddle()
    {
        Vector2 topPos = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y) / 2);
        Vector2 bottomPos = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y) / 2);
        Paddle addedTop = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        Paddle addedBottom = Instantiate(Resources.Load<Paddle>("PaddleBrick"));
        addedTop.Init(this.isRight, 1);
        addedBottom.Init(this.isRight, 1);
        this.size += 2;
        addedBottom.transform.SetParent(this.transform);
        addedTop.transform.SetParent(this.transform);
    }
}
