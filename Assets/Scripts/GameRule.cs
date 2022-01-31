using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    public int nbObjectsSelect;

    GameObject[] objectsMoveable;
    GameObject[] objectsMoveableSelected;
    GameObject objVisu;

    private void Awake()
    {
        int idSelect;
        objectsMoveable = GameObject.FindGameObjectsWithTag("ObjectMoveable");
        // Randomination du nombre de d'objet selectionnés - un peu beaucoup - 
        //nbObjectsSelect = Random.Range(1, objectsMoveable.Length);
        nbObjectsSelect = Random.Range(1, 11);

        objectsMoveableSelected = new GameObject[nbObjectsSelect];

        for (int i = 0; i < nbObjectsSelect; i++)
        {
            idSelect = Random.Range(0, objectsMoveable.Length);
            objectsMoveableSelected[i] = objectsMoveable[idSelect];

           // Debug.Log("nom : " + objectsMoveableSelected[i].name);
            //Debug.Log("location départ : " + objectsMoveableSelected[i].GetComponent<Location>().referenceArea);
            objectsMoveableSelected[i].GetComponent<Location>().referenceArea = RandomLocation(objectsMoveableSelected[i]);
            //Debug.Log("location modifié : " + objectsMoveableSelected[i].GetComponent<Location>().referenceArea);

            RemoveObjectFromTab(idSelect);

            objectsMoveableSelected[i].GetComponent<Outline>().OutlineColor = Color.green;
            objectsMoveableSelected[i].GetComponent<Outline>().enabled = true;
        }
    }

    void Start()
    {
        for(int i=0; i<nbObjectsSelect; i++)
            Debug.Log("nom : " + objectsMoveableSelected[i].name);
        /*int idSelect;
        objectsMoveable = GameObject.FindGameObjectsWithTag("ObjectMoveable");
        // Randomination du nombre de d'objet selectionnés
        //nbObjectsSelect = Random.Range(1, objectsMoveable.Length);
        nbObjectsSelect = Random.Range(1, 5);

        objectsMoveableSelected = new GameObject[nbObjectsSelect];        

        for (int i=0; i<nbObjectsSelect; i++)
        {
            idSelect = Random.Range(0, objectsMoveable.Length);
            objectsMoveableSelected[i] = objectsMoveable[idSelect];

            Debug.Log("nom : " + objectsMoveableSelected[i].name);
            Debug.Log("location départ : " + objectsMoveableSelected[i].GetComponent<Location>().referenceArea);
            objectsMoveableSelected[i].GetComponent<Location>().referenceArea = RandomLocation(objectsMoveableSelected[i]);
            Debug.Log("location modifié : " + objectsMoveableSelected[i].GetComponent<Location>().referenceArea);

            RemoveObjectFromTab(idSelect);

            objectsMoveableSelected[i].GetComponent<Outline>().OutlineColor = Color.green;
            objectsMoveableSelected[i].GetComponent<Outline>().enabled = true;
        }*/
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
                            Debug.Log("nom : " + objectsMoveableSelected[j].name + ", green");
                            objectsMoveableSelected[j].GetComponent<Outline>().enabled = true;
                            objectsMoveableSelected[j].GetComponent<Outline>().OutlineColor = Color.green;
                        }
                        /*else
                        {
                            objectsMoveable[i].GetComponent<Outline>().enabled = true;
                            objectsMoveable[i].GetComponent<Outline>().OutlineColor = Color.red;
                        }
                        */
                    }
                }
                else
                    objectsMoveable[i].GetComponent<Outline>().enabled = false;
            }
        }
        
    }

    void RemoveObjectFromTab(int id)
    {
        for(int i=id; i<objectsMoveable.Length-1; i++)
        {
            objectsMoveable[i] = objectsMoveable[i++];
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
