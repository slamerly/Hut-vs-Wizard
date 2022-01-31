using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        RaycastHit hit;
        //si Mathf.infinyt, plus precis
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*20, Color.blue);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.CompareTag("ObjectMoveable"))
            {
                obj = hit.transform.gameObject;
                obj.GetComponent<Outline>().enabled = true;
            }
            else
            {
                //if (obj != null && obj.GetComponent<Outline>().OutlineColor != Color.green && obj.GetComponent<Outline>().OutlineColor != Color.red)
                if (obj != null)
                {
                    obj.GetComponent<Outline>().enabled = false;
                    obj = null;
                }
            }


            // ramaser objet
            /*if ((Input.GetMouseButtonDown(1)) && (obj != null))
                Destroy(obj);
            */
        }
    }
}
