using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonApple : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float bottomY = -16f;

    void Update()
    {
        if(transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            //Adding 100 points to score for avoiding the poison apple
            GameObject scoreGO = GameObject.Find("ScoreCounter");
            Text scoreGT = scoreGO.GetComponent<Text>();
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();
        }
    }
}
