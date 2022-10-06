using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI features

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;

    [Header("Set in Inspector")]
    public float leftAndRightEdge = 20f;

    void Start()
    {
        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the Text Component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>();
        // Set the starting number of points to 0
        scoreGT.text = "0";
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

        //Prevent basket from moving past screen edge
        if (this.transform.position.x < -leftAndRightEdge)
        {
            pos.x = -leftAndRightEdge;
            this.transform.position = pos;
        }
        else if (this.transform.position.x > leftAndRightEdge)
        {
            pos.x = leftAndRightEdge;
            this.transform.position = pos;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        //Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);

            // Parse the text of the scoreGT into an int
            int score = int.Parse(scoreGT.text);
            // Add points for catching the apple
            score += 100;
            // Convert the score back to a string and display it
            scoreGT.text = score.ToString();

            // Track the high score
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
        //Destroys the basket if it hits a poison apple & deduct 250 points
        else if (collidedWith.tag == "PoisonApple")
        {
            Destroy(collidedWith);
            int score = int.Parse(scoreGT.text);
            score -= 250;
            scoreGT.text = score.ToString();

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleDestroyed();
        }
        //Adds 500 points for catching the gold apple
        else if (collidedWith.tag == "GoldApple") {
            Destroy(collidedWith);
            int score = int.Parse(scoreGT.text);
            score += 500;
            scoreGT.text = score.ToString();

            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
    }
}
