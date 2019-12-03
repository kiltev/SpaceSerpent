using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private int padding;
    private float timeToLeave, currentTime;
    public float selfDistructMinTime = 0, selfDistructMaxTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        timeToLeave = Random.Range(selfDistructMinTime, selfDistructMaxTime) *Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToLeave)
        {
            SelfRemover();
        }
    }

    public void Init()
    {
        padding = 1;
        transform.position = new Vector2(Random.Range(GameManager.BottomLeft.x + padding, GameManager.TopRight.x - padding),
            Random.Range(GameManager.BottomLeft.y + padding, GameManager.TopRight.y - padding));
    }

    void SelfRemover()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}