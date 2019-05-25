using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject hand;
    public float rotSpeed;
    private Vector3 startMousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 v = Input.mousePosition - startMousePos;
            Debug.Log(v);

            hand.transform.eulerAngles = hand.transform.eulerAngles + v * rotSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }
}
