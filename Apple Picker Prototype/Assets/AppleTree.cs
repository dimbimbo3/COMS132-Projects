using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.1f;
    public float secondsBetweenAppleDrops = 1f;
    public GameObject poisonApplePrefab;
    public GameObject goldApplePrefab;

    private int appleCount = 0;

    void Start()
    {
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        appleCount = Random.Range(1, 11);
        //If count is 10, then golden apple falls
        //Else if even count then normal apple or odd count for poison apple
        if (appleCount == 10) {
            GameObject apple = Instantiate<GameObject>(goldApplePrefab);
            apple.transform.position = transform.position;
            Invoke("DropApple", secondsBetweenAppleDrops);
        }
        else if (appleCount % 2 == 0)
        {
            GameObject apple = Instantiate<GameObject>(applePrefab);
            apple.transform.position = transform.position;
            Invoke("DropApple", secondsBetweenAppleDrops);
        }
        else
        {
            GameObject apple = Instantiate<GameObject>(poisonApplePrefab);
            apple.transform.position = transform.position;
            Invoke("DropApple", secondsBetweenAppleDrops);
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if(pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }
}
