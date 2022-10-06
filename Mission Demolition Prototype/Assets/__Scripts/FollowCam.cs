using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; //The static point of interest
    static public float shotTimer;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;  //The desired Z pos of the camera

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 destination;
        //If there is no poi, return to P:[0,0,0]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //Get the position of the poi
            destination = POI.transform.position;
            //If poi is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                shotTimer += Time.deltaTime;//timer increases every 1 second

                //if it is sleeping (that is, not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    //return to default view
                    POI = null;

                    shotTimer = 0;

                    //in the next update
                    return;
                }
                //prevents sleep from taking too long
                else if (shotTimer >= 6)
                {
                    //return to default view
                    POI = null;

                    shotTimer = 0;

                    //in the next update
                    return;
                }
            }
        }

        //Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Force desitination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
        //Set the orthographicSize of the Camera to keep Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
