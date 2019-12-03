using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    private int _bodySize;
    private Transform _cur;
    private Vector2 _direction;
    private float _dis;
    private float _increaseSpeedInterval;
    private float _initialSpeed;
    private bool _localLastFail;
    private Transform _prev;
    private float _radius;
    private Rigidbody2D _snakeHead;
    public List<Transform> currentBodyParts;
    public List<Transform> bodyParts = new List<Transform>();
    public SnakeBody bodyPrefab;
    public GameManager gameManager;
    private readonly bool leftPlayer = false;
    public float minDistance;
    public int lastTarget=0;
    private readonly bool rightPlayer = true;
    public float speed;
    private int rightPaddle;
    private int leftPaddle;
    // Start is called before the first frame update
    private void Start()
    {
        _dis = 0.5f;
        minDistance = 1f;
        _increaseSpeedInterval = 3f;
        _snakeHead = GetComponent<Rigidbody2D>();
        //        _direction = new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f)).normalized;
        _direction = RandomDirection().normalized;
        _radius = transform.localScale.x / 2;
        speed = 500f;
        _initialSpeed = 500f;
        _bodySize = 0;
        bodyParts.Add(transform);
        _snakeHead.velocity = _direction * Time.deltaTime * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        var velocity = _snakeHead.velocity;
        var angle = Vector2.SignedAngle(velocity, Vector2.up);
        _snakeHead.transform.rotation = Quaternion.Euler(0, 0, -angle);
//        Debug.Log("angle: " + angle + ", rotation: " + _snakeHead.transform.rotation.z);

        IncreaseSpeed();
        MoveSnake();
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("LPaddle") || coll.collider.CompareTag("RPaddle"))
        {
            var initialSpeed = _snakeHead.velocity.magnitude;
//            Debug.Log("speed: " + initialSpeed);
            Vector2 vel;
            vel.x = _snakeHead.velocity.x;
            vel.y = _snakeHead.velocity.y / 2 + coll.collider.attachedRigidbody.velocity.y / 3;
            _snakeHead.velocity = vel.normalized * Time.deltaTime * speed;
//            AddBodyPart();
            if (transform.position.x > 0)
                _localLastFail = false;
            else
                _localLastFail = true;
            if (coll.collider.tag == "RPaddle")
            {
                rightPaddle = 1;
                lastTarget = rightPaddle;
            }
            else
            {
                leftPaddle = 2;
                lastTarget = leftPaddle;
            }
        }

//        if (coll.collider.CompareTag("Wall"))
        //            Time.timeScale = 0;
//            ResetSnake();

        if (coll.collider.CompareTag("RWall"))
        {
            gameManager.PointHandler(rightPlayer);
            ResetSnake();
        }

        if (coll.collider.CompareTag("LWall"))
        {
            gameManager.PointHandler(leftPlayer);
            ResetSnake();
        }
    }


    private void ResetSnake()
    {
        speed = _initialSpeed;
        for (var i = 0; i < bodyParts.Count; i++) bodyParts[i].position = Vector2.zero;

//        if (_localLastFail)
        if (GameManager.lastFail)
        {
            _direction = new Vector2(Random.Range(-0.7f, -0.1f), Random.Range(-0.7f, 0.7f)).normalized;
//            _localLastFail = false;
            GameManager.lastFail = false;
        }
        else
        {
            _direction = new Vector2(Random.Range(0.1f, 0.7f), Random.Range(-0.7f, 0.7f)).normalized;
//            _localLastFail = true;
            GameManager.lastFail = true;
        }

        _snakeHead.velocity = _direction * Time.deltaTime * speed;
        lastTarget = 0;
    }


    private void MoveSnake()
    {
        for (var i = 1; i < bodyParts.Count; i++)
        {
            _cur = bodyParts[i];
            _prev = bodyParts[i - 1];
            _cur.rotation = _prev.rotation;
            _dis = Vector2.Distance(_prev.position, _cur.position);
            Vector2 newPos = _prev.position;
//            Debug.Log("euler z: " + _cur.eulerAngles.z);
//            newPos.y = bodyParts[i - 1].position.y;
//            float T = Time.deltaTime * _dis / minDistance * speed;
//            if (T > 0.5f)
//            {
//                T = 0.5f;
//            }
            _cur.position = Vector3.Lerp(_cur.position, newPos, 0.3f);
//            _cur.position = PositionCalc(_prev, _prev.eulerAngles.z);
        }
    }

    private void IncreaseSpeed()
    {
        if (Time.time > _increaseSpeedInterval)
        {
            speed += 50f;
            var normVelocity = _snakeHead.velocity.normalized;
            _snakeHead.velocity = normVelocity * Time.deltaTime * speed;
            _increaseSpeedInterval += 2f;
        }
    }



    private void AddBodyPart()
    {
//        Transform newBodyPart = Instantiate(Resources.Load<SnakeBody>("SnakeBody")).transform;
        var angle = bodyParts[_bodySize].eulerAngles.z;
        var newBodyPart = Instantiate(Resources.Load<SnakeBody>("SnakeBody"),
            bodyParts[_bodySize].position,
            bodyParts[_bodySize].rotation).transform;
//        newBodyPart.SetParent(transform);
        bodyParts.Add(newBodyPart);
        _bodySize += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus"))
        {
            Debug.Log("You Hit The Bonus!");
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            AddBodyPart();
            StartCoroutine(animateSnake());
        }
    }

    public IEnumerator animateSnake()  //Animation Coroutine
    {
        float delay = 0.7f * Time.deltaTime;
        this.GetComponent<Animator>().SetTrigger("ate");
        currentBodyParts = new List<Transform>(bodyParts);
        foreach (var bodyPart in currentBodyParts)
        {
            yield return new WaitForSeconds(delay);
            bodyPart.GetComponent<Animator>().SetTrigger("ate");
            delay += 0.7f * Time.deltaTime;
        }
    }

    private Vector2 RandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        while ((y / x) > 2f)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
        }

        return new Vector2(x, y);
    }

}
