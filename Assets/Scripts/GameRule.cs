using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    public int nbObjectsSelect;
    public GameObject endscreen;

    GameObject[] objectsMoveable;
    GameObject[] objectsMoveableSelected;
    GameObject objVisu;
    GameObject crosshair;

    private void Awake()
    {
        crosshair = GameObject.Find("Crosshair");
        Time.timeScale = 1f;

        int idSelect;
        objectsMoveable = GameObject.FindGameObjectsWithTag("ObjectMoveable");

        for (int i = 0; i < objectsMoveable.Length; i++)
        {
            objectsMoveable[i].GetComponent<Pickup>().heldObj = false;
            //objectsMoveable[i].GetComponent<Pickup>().heldFull = false;
        }

        //Debug.Log("nombre objets bougeables : " + objectsMoveable.Length);
        nbObjectsSelect = Random.Range(1, 10);
        //nbObjectsSelect = Random.Range(1, 2);

        objectsMoveableSelected = new GameObject[nbObjectsSelect];

        for (int i = 0; i < nbObjectsSelect; i++)
        {
            idSelect = Random.Range(0, objectsMoveable.Length);
            objectsMoveableSelected[i] = objectsMoveable[idSelect];
            objectsMoveableSelected[i].GetComponent<Location>().referenceArea = RandomLocation(objectsMoveableSelected[i]);

            RemoveObjectFromTab(idSelect);

            objectsMoveableSelected[i].GetComponent<Outline>().OutlineColor = Color.green;
            objectsMoveableSelected[i].GetComponent<Outline>().enabled = true;
        }
    }

    private void Update()
    {
        objVisu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Vision>().obj;
        if(objVisu != null)
        {
            objVisu.GetComponent<Outline>().OutlineColor = Color.white;
        }
        else
        {
            for (int i = 0; i < objectsMoveable.Length; i++)
            {
                if (!objectsMoveable[i].GetComponent<Location>().IsInGoodArea())
                {
                    objectsMoveable[i].GetComponent<Outline>().enabled = true;
                    objectsMoveable[i].GetComponent<Outline>().OutlineColor = Color.red;

                    for (int j = 0; j < nbObjectsSelect; j++)
                    {
                        if(objectsMoveable[i] == objectsMoveableSelected[j])
                        {
                            //Debug.Log("nom : " + objectsMoveableSelected[j].name + ", green");
                            objectsMoveableSelected[j].GetComponent<Outline>().enabled = true;
                            objectsMoveableSelected[j].GetComponent<Outline>().OutlineColor = Color.green;
                        }
                    }
                }
                else
                    objectsMoveable[i].GetComponent<Outline>().enabled = false;
            }
        }
        EndGame();
    }

    void RemoveObjectFromTab(int id)
    {
        for(int i=id; i<objectsMoveable.Length-1; i++)
        {
            objectsMoveable[i] = objectsMoveable[i++];
        }
    }

    void EndGame()
    {
        int cpt = 0;
        for (int i = 0; i < objectsMoveable.Length; i++)
        {
            if (objectsMoveable[i].GetComponent<Location>().IsInGoodArea())
                cpt++;
        }

        if (cpt == objectsMoveable.Length)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            crosshair.SetActive(false);
            endscreen.SetActive(true);
            //Debug.Log("EndGame");
        }
    }

    GameObject RandomLocation(GameObject obectSelected)
    {
        GameObject[] locationTab = GameObject.FindGameObjectsWithTag("Area");
        GameObject save;
        int locationSelected;
        
        for(int i=0; i<locationTab.Length; i++)
        {
            if(locationTab[i] == obectSelected.GetComponent<Location>().referenceArea)
            {
                save = locationTab[locationTab.Length - 1];
                locationTab[locationTab.Length - 1] = locationTab[i];
                locationTab[i] = save;
            }
        }

        locationSelected = Random.Range(0, locationTab.Length - 1);
        return locationTab[locationSelected];
    }
}
