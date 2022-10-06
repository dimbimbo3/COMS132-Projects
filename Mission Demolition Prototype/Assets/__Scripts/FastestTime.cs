using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastestTime : MonoBehaviour
{
    static public float time = 0;

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
        time = 0;
        //Advance count for next level fastest time
        if (count <= MissionDemolition.levelMax)
        {
            count++;
        }
        // If the PlayerPrefs (FastestTime + count) already exists, read it
        if (PlayerPrefs.HasKey("FastestTime" + count))
        {
            time = PlayerPrefs.GetFloat("FastestTime" + count, time);
        }
        // Assign the high score to (FastestTime + count)
        PlayerPrefs.SetFloat("FastestTime" + count, time);
    }

    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "Fastest Time: " + time.ToString(".00");
        // Update the PlayerPrefs (FastestTime + count) if necessary
        if (time > PlayerPrefs.GetFloat("FastestTime" + count))
        {
            PlayerPrefs.SetFloat("FastestTime" + count, time);
        }
        //If reset is true, reset the high score for level
        if (reset)
        {
            time = 0;
            PlayerPrefs.SetFloat("FastestTime" + count, time);
        }
        //Check if the next level has started
        if ((MissionDemolition.level - count) != -1)
            NextLevel();
    }
}
