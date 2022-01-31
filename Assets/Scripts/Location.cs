using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public GameObject referenceArea;

    GameObject cam;
    float xMin, xMax, zMin, zMax;
    

    // Start is called before the first frame update
    void Start()
    {
        Transform transformReferenceArea = referenceArea.transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        xMin = transformReferenceArea.position.x - transformReferenceArea.localScale.x / 2;
        //Debug.Log("xMin : " + xMin);
        xMax = transformReferenceArea.position.x + transformReferenceArea.localScale.x / 2;
        //Debug.Log("xMax : " + xMax);
        zMin = transformReferenceArea.position.z - transformReferenceArea.localScale.z / 2;
        //Debug.Log("zMin : " + zMin);
        zMax = transformReferenceArea.position.z + transformReferenceArea.localScale.z / 2;
        //Debug.Log("zMax : " + zMax);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (cam.GetComponent<Vision>().obj == null)
        {
            if ((transform.position.x < xMin || transform.position.x > xMax) || (transform.position.z < zMin && transform.position.z > zMax))
            {
                GetComponent<Outline>().enabled = true;
                GetComponent<Outline>().OutlineColor = Color.red;
            }
            else
                GetComponent<Outline>().enabled = false;
        }
    }
    */

    public bool IsInGoodArea()
    {
        if ((transform.position.x >= xMin & transform.position.x <= xMax) && (transform.position.z >= zMin && transform.position.z <= zMax))
            return true;
        else
            return false;
    }
}
