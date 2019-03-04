using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Transform verticalLayoutGroup;

    // Start is called before the first frame update
    void Start()
    {
        verticalLayoutGroup.GetChild(0).GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Selectable.allSelectables.Count; i++)
        {
            Debug.Log(Selectable.allSelectables[i].name);
        }

    }

    public void NewGameButtonOnClick()
    {

    }
    public void ContinueButtonOnClick()
    {

    }
    public void OptionButtonOnClick()
    {

    }
    public void QuitButtonOnClick()
    {

    }
}
