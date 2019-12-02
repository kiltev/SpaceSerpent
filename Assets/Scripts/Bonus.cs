using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private int marginIncrease;

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
        marginIncrease = 2;
        transform.position = new Vector2(Random.Range(GameManager.BottomLeft.x + marginIncrease, GameManager.TopRight.x - marginIncrease),
            Random.Range(GameManager.BottomLeft.y + marginIncrease, GameManager.TopRight.y - marginIncrease));
    }
}