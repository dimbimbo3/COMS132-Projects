using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode
{
    idle, playing, levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //a private Singleton
    static public int level; //The current level
    static public int levelMax; //The number of levels

    [Header("Set in Inspector")]
    public Text uitLevel; //The UIText_Level Text
    public Text uitShots; //The UIText_Shots Text
    public Text uitButton; //The Text on UIButton_View
    public Text uitTimer; //UI Timer
    public Vector3 castlePos; //The place to put castles
    public GameObject[] castles; //An array of the castles
    public float lvl1Time = 60;
    public float lvl2Time = 50;
    public float lvl3Time = 40;
    public bool stop = false;

    [Header("Set Dynamically")]
    public int shotsTaken;
    public GameObject castle; //The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode
    public float timer; //timer
    public float countDown;
    public float timeCount;

    void Start()
    {
        S = this; //Definite Singleton

        level = 0;
        levelMax = castles.Length;
        timeCount = lvl1Time;
        StartLevel();
    }

    void StartLevel()
    {
        //Get rid of the old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //Reset the camera
        SwitchView("Show Both");

        //Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        timer = 0;
        stop = false;
    }

    void UpdateGUI()
    {
        //Show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        uitTimer.text = "Time: " + countDown.ToString(".00");
    }

    void Update()
    {
        if (!stop)
            timer += Time.deltaTime;
        countDown = timeCount - timer;
        UpdateGUI();

        //Check for level end
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            //set fastest time
            if (countDown > FastestTime.time)
                FastestTime.time = countDown;
            if (shotsTaken < LeastShots.shots)
                LeastShots.shots = shotsTaken;
            stop = true;
            //Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            //Zoom out
            SwitchView("Show Both");
            //Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
        else if(countDown <= 0)
        {
            stop = true;
            SwitchView("Show Both");
            StartLevel();
        }
    }

    void NextLevel()
    {
        level++;

        if (level == 1)
            timeCount = lvl2Time;
        else if (level == 2)
            timeCount = lvl3Time;

        if(level == levelMax)
        {
            SceneManager.LoadScene("_Scene_0");
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                FollowCam.shotTimer = 0;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                FollowCam.shotTimer = 0;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                FollowCam.shotTimer = 0;
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    //Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
