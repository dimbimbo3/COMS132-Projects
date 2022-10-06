using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAlong : MonoBehaviour
{
    private void LateUpdate()
    {
        countItHigher cih = this.gameObject.GetComponent<countItHigher>();
        if(cih != null)
        {
            float tX = cih.currentNum / 10f;
            Vector3 tempLoc = pos;
            tempLoc.x = tX;
            pos = tempLoc;
        }
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
}
