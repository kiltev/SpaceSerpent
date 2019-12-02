﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBrick : MonoBehaviour
{
    
    public bool isRight;
    public float height;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init(bool isRightPaddle, Paddle parent)
    {
        height = GetComponent<BoxCollider2D>().bounds.size.y;
        parent.height += height;
//        if (parent.transform.position.y < GameManager.BottomLeft.y + (parent.height / 2f) + 0.2f)
//        {
//            parent.transform.position = new Vector2(parent.transform.position.x, GameManager.BottomLeft.y + (height / 2f) + 0.2f);
//        }
//        if (parent.transform.position.y > GameManager.TopRight.y - (parent.height / 2f) - 0.2f)
//        {
//            parent.transform.position = new Vector2(parent.transform.position.x, GameManager.TopRight.y - (height / 2f) - 0.2f);
//        }
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
}
