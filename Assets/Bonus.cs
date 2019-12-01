using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init()
    {
        transform.position = new Vector2(Random.Range(GameManager.BottomLeft.x, GameManager.TopRight.x),
            Random.Range(GameManager.BottomLeft.y, GameManager.TopRight.y));
    }
}