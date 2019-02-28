using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTT1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => { UIFramework.Manager.Instance.OpenFrame("T1"); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
