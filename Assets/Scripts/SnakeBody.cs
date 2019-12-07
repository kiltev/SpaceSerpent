using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public int placeInBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int partNum)
    {
        placeInBody = partNum;
    }
    
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("HeadTip") && placeInBody > 5)
//        {
//            Debug.Log("Snake collided with itself!");
////            Snake parent = other.transform.parent.transform.gameObject.GetComponent(typeof(Snake)) as Snake;
//            Snake parent = other.GetComponentInParent<Snake>();
//            parent.ResetAfterCollision(placeInBody);
//        }
//    }
}