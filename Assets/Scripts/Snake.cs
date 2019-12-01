using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameManager gameManager;
    public List<Transform> bodyParts = new List<Transform>();
    public float minDistance;
    public SnakeBody bodyPrefab;
    private float _dis;
    private Transform _cur;
    private Transform _prev;
    private Rigidbody2D _snakeHead;
    public float speed;
    private float _radius;
    private Vector2 _direction;
    private float _increaseSpeedInterval;
    private int _bodySize;

    private bool rightPlayer = true;
    private bool leftPlayer = false;
    //    private Vector2 _pos;
    
    // Start is called before the first frame update
    void Start()
    {
//        gameManager = FindObjectOfType<GameManager>();
        minDistance = 1f;
        _increaseSpeedInterval = 3f;
        _snakeHead = GetComponent<Rigidbody2D>();
        _direction = Vector2.one.normalized;
        _radius = transform.localScale.x / 2;
        speed = 500f;
        _bodySize = 0;
//        _pos = _snakeHead.transform.position;
        bodyParts.Add(transform);
        _snakeHead.velocity = _direction * Time.deltaTime * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = _snakeHead.velocity;
        float angle = Vector2.SignedAngle(velocity, Vector2.up);
        _snakeHead.transform.rotation = Quaternion.Euler(0 ,0, -angle);
//        Debug.Log("angle: " + angle + ", rotation: " + _snakeHead.transform.rotation.z);



        if (Time.time > _increaseSpeedInterval)
        {
            speed += 20f;
            Vector2 normVelocity = _snakeHead.velocity.normalized;
            _snakeHead.velocity = normVelocity * Time.deltaTime * speed;
            _increaseSpeedInterval += 2f;
        }
        
        
        for (int i = 1; i < bodyParts.Count; i++)
        {
            _cur = bodyParts[i];
            _prev = bodyParts[i - 1];

            _dis = Vector2.Distance(_prev.position, _cur.position);
            Vector2 newPos = _prev.position;
            newPos.y = bodyParts[0].position.y;
//            float T = Time.deltaTime * _dis / minDistance * speed;
//            if (T > 0.5f)
//            {
//                T = 0.5f;
//            }

            _cur.position = Vector3.Slerp(_cur.position, newPos, 0.2f);
        }
    }
    
    
    private void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Paddle"))
        {
            float initialSpeed = _snakeHead.velocity.magnitude;
//            Debug.Log("speed: " + initialSpeed);
            Vector2 vel;
            vel.x = _snakeHead.velocity.x;
            vel.y = (_snakeHead.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 5);
            _snakeHead.velocity = vel.normalized * Time.deltaTime * speed;
            AddBodyPart();
        }

        if (coll.collider.CompareTag("RWall"))
        {
            gameManager.PointHandler(rightPlayer);
        }
        if (coll.collider.CompareTag("LWall"))
        {
            gameManager.PointHandler(leftPlayer);
        }

        //        if (coll.collider.CompareTag("Wall"))
        //        {
        //            Time.timeScale = 0;
        //        }
    }



    //    private void OnTriggerEnter2D (Collider2D other) {
        //        if(other.CompareTag("Paddle"))
        //        {
        //            float initialSpeed = _snakeHead.velocity.magnitude;
        //            Vector2 vel;
        //            vel.x = _snakeHead.velocity.x;
        //            vel.y = (_snakeHead.velocity.y / 2) + (other.attachedRigidbody.velocity.y / 3);
        //            _snakeHead.velocity = vel.normalized * initialSpeed;
        ////            SnakeBody node = Instantiate(Resources.Load<SnakeBody>("Body"));
        ////            node.transform.position = _nodes[-1].transform.position;
        //            Debug.Log("velocity: " + _snakeHead.velocity);
        //        }

        //        if (coll.collider.CompareTag("TBWall"))
        //        {
        //            
        //        }
        //        if (other.CompareTag("Wall"))
        //        {
        //            Time.timeScale = 0;
        //        }
        //    }

        private void AddBodyPart()
    {
//        Transform newBodyPart = Instantiate(Resources.Load<SnakeBody>("SnakeBody")).transform;
        Transform newBodyPart = Instantiate(Resources.Load<SnakeBody>("SnakeBody"), 
            bodyParts[_bodySize].position, 
            bodyParts[_bodySize].rotation).transform;
        newBodyPart.SetParent(transform);
        bodyParts.Add(newBodyPart);
        _bodySize += 1;
    }
}


// Bounce off top and bottom - was in update
//        if (transform.position.y < GameManager.BottomLeft.y + _radius && _direction.y < 0)
//        {
//            _direction.y = -_direction.y;
//        }
//        if (transform.position.y > GameManager.TopRight.y - _radius && _direction.y > 0)
//        {
//            _direction.y = -_direction.y;
//        }

// Game over - was in update.
//        if (transform.position.x < GameManager.BottomLeft.x + _radius)
//        {
//            Debug.Log("Right player wins!");
//            Time.timeScale = 0;
//        }
//        if (transform.position.x > GameManager.TopRight.x - _radius)
//        {
//            Debug.Log("Left player wins!");
//            Time.timeScale = 0;
//        }

//    Can probably be deleted.
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