using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    public SnakeBody bodyPrefab;
    private float _dis;
    private Transform _cur;
    private Transform _prev;
    private Rigidbody2D _snakeHead;
    private float _speed;
    private float _radius;
    private Vector2 _direction;
//    private Vector2 _pos;
    private int _bodySize;

    // Start is called before the first frame update
    void Start()
    {
        _snakeHead = GetComponent<Rigidbody2D>();
        _direction = Vector2.one.normalized;
        _radius = transform.localScale.x / 2;
        _speed = 500f;
        _bodySize = 0;
//        _pos = _snakeHead.transform.position;
        bodyParts.Add(transform);
        GetComponent<Rigidbody2D>().velocity = _direction * Time.deltaTime * _speed;
    }

    // Update is called once per frame
    void Update()
    {
//        transform.Translate(_direction * _speed * Time.deltaTime);
        
        // Bounce off top and bottom.
//        if (transform.position.y < GameManager.BottomLeft.y + _radius && _direction.y < 0)
//        {
//            _direction.y = -_direction.y;
//        }
//        if (transform.position.y > GameManager.TopRight.y - _radius && _direction.y > 0)
//        {
//            _direction.y = -_direction.y;
//        }
        
        // Game over
        if (transform.position.x < GameManager.BottomLeft.x + _radius)
        {
            Debug.Log("Right player wins!");
            Time.timeScale = 0;
        }
        if (transform.position.x > GameManager.TopRight.x - _radius)
        {
            Debug.Log("Left player wins!");
            Time.timeScale = 0;
        }
//        for (int i = 1; i < bodyParts.Count; i++)
//        {
//            _cur = bodyParts[i];
//            _prev = bodyParts[i - 1];
//
//            _dis = Vector2.Distance(_prev.position, _cur.position);
//            Vector2 newPos = _prev.position;
//            newPos.y = bodyParts[0].position.y;
//            float T = Time.deltaTime * _dis / minDistance * _speed;
//            if (T > 0.5f)
//            {
//                T = 0.5f;
//            }
//
//            _cur.position = Vector3.Slerp(_cur.position, newPos, T);
//        }
    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.tag == "Paddle")
//        {
//            bool isRight = other.GetComponent<Paddle>().isRight;
//            if (isRight && _direction.x > 0)
//            {
//                _direction.x = -_direction.x;
//            }
//            if (!isRight && _direction.x < 0)
//            {
//                _direction.x = -_direction.x;
//            }
//        }
//        if (other.tag == "Wall")
//        {
//            if (transform.position.y < GameManager.BottomLeft.y + _radius && _direction.y < 0)
//            {
//                _direction.y = -_direction.y;
//            }
//            if (transform.position.y > GameManager.TopRight.y - _radius && _direction.y > 0)
//            {
//                _direction.y = -_direction.y;
//            }
//        }
//    }
    
    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Paddle"))
        {
            float initialSpeed = _snakeHead.velocity.magnitude;
            Vector2 vel;
            vel.x = _snakeHead.velocity.x;
            vel.y = (_snakeHead.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            _snakeHead.velocity = vel.normalized * initialSpeed;
//            SnakeBody node = Instantiate(Resources.Load<SnakeBody>("Body"));
//            node.transform.position = _nodes[-1].transform.position;
        }

        if (coll.collider.CompareTag("Wall"))
        {
            Time.timeScale = 0;
        }
    }

//    private void AddBodyPart()
//    {
//        Transform newBodyPart = Instantiate(Resources.Load<SnakeBody>("Body"), 
//                bodyParts[-1].position, 
//                bodyParts[-1].rotation).transform;
//        newBodyPart.SetParent(transform);
//        bodyParts.Add(newBodyPart);
//    }
}
