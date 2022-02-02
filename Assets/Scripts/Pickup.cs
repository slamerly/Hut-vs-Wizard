using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject player, objectContainer, cam;

    public float pickUpRange = 5;
    public float dropForwardForce, dropUpwardForce;
    public bool heldObj = false;
    public static bool heldFull = false;

    private Vector3 saveScaleObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objectContainer = GameObject.FindGameObjectWithTag("ObjectContainer");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        heldFull = heldObj;
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if (this.gameObject == cam.GetComponent<Vision>().obj)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!heldFull)
                {
                    if (!heldObj && distanceToPlayer.magnitude < pickUpRange)
                        PickUpObject();
                }
            }
        }

        if (heldObj && Input.GetKeyDown(KeyCode.Mouse1))
            DropObject();
    }

    private void PickUpObject()
    {
        heldObj = true;
        heldFull = true;

        saveScaleObject = transform.localScale;

        transform.SetParent(objectContainer.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = saveScaleObject;

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshCollider>().isTrigger = true;
    }

    private void DropObject()
    {
        heldObj = false;
        heldFull = false;

        transform.SetParent(null);

        transform.localScale = saveScaleObject;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<MeshCollider>().isTrigger = false;

        GetComponent<Rigidbody>().AddForce(cam.transform.forward * dropForwardForce, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(cam.transform.up * dropUpwardForce, ForceMode.Impulse);
    }
}
