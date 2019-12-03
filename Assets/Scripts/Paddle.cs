using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D playerPaddle;
    public float height;
    private float _speed;
    private string _input;
    public bool isRight;

//    [SerializeField] private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerPaddle = GetComponent<Rigidbody2D>();
        _speed = 800f;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;
        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.TopRight.x, 0) + (Vector2.left * transform.localScale.x);
        }
        else
        {
            pos = new Vector2(GameManager.BottomLeft.x, 0) + (Vector2.right * transform.localScale.x);
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
                move = Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                move = -Time.deltaTime * _speed;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                move = Time.deltaTime * _speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                move = -Time.deltaTime * _speed;
            }
        }

        if (transform.position.y < GameManager.BottomLeft.y + (height / 2f) + 0.2f && move < 0f)
        {
            move = 0;
        }

        if (transform.position.y > GameManager.TopRight.y - (height / 2f) - 0.2f && move > 0f)
        {
            move = 0;
        }

        playerPaddle.velocity = move * Vector2.up;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            this.GetComponent<Animator>().SetTrigger("isHit");
        }
    }
}