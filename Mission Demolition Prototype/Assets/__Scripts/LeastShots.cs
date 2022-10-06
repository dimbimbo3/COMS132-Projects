using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeastShots : MonoBehaviour
{
    static public int shots = 20;

    [Header("Set in Inspector")]
    public bool reset = false;

    [Header("Set Dynamically")]
    public int count = 0;

    void Awake()
    {
        NextLevel();
    }

    void NextLevel()
    {
        //Reset time to 0 for next level
        shots = 20;
        //Advance count for next level fastest time
        if (count <= MissionDemolition.levelMax)
        {
            count++;
        }
        // If the PlayerPrefs (LeastShots + shots) already exists, read it
        if (PlayerPrefs.HasKey("LeastShots" + count))
        {
            shots = PlayerPrefs.GetInt("LeastShots" + count, shots);
        }
        // Assign the high score to (LeastShots + shots)
        PlayerPrefs.SetInt("LeastShots" + count, shots);
    }

    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "Least Shots Taken: " + shots;
        // Update the PlayerPrefs (LeastShots + count) if necessary
        if (shots < PlayerPrefs.GetInt("LeastShots" + count))
        {
            PlayerPrefs.SetInt("LeastShots" + count, shots);
        }
        //If reset is true, reset the high score for level
        if (reset)
        {
            shots = 20;
            PlayerPrefs.SetInt("LeastShots" + count, shots);
        }
        //Check if the next level has started
        if ((MissionDemolition.level - count) != -1)
            NextLevel();
    }
}
