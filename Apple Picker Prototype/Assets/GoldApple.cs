using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldApple : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float bottomY = -16f;

    void Update()
    {
        if(transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
}
