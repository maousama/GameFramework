using Assets.Scripts.MultipleLang;
using Assets.Scripts.UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : View
{
    public Text tittleText, newText, continueText, optionText, quitText;

    protected override void Awake()
    {
        base.Awake();

        tittleText.Bind("Terrain Generator");
        newText.Bind("New");
        continueText.Bind("Continue");
        optionText.Bind("Option");
        quitText.Bind("Quit");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        tittleText.Unbind();
        newText.Unbind();
        continueText.Unbind();
        optionText.Unbind();
        quitText.Unbind();
    }

    public void NewButtonOnClick()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        Manager.Instance.CloseFrame(frame);
        asyncOperation.allowSceneActivation = true;
        asyncOperation.completed += delegate (AsyncOperation async) { Manager.Instance.OpenFrame("LoadingView"); };
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
