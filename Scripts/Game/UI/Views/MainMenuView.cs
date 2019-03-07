using Assets.Scripts.MultipleLang;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    public Transform verticalLayoutGroup;
    public List<KeyValuePair<Text, string>> textToBindString;

    protected override void Awake()
    {
        base.Awake();
        //foreach (KeyValuePair<Text, string> pair in textToBindString) pair.Key.Bind(pair.Value);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
