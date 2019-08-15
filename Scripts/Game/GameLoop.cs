using Assets.Scripts.UIFramework;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 作为游戏住循环控制组件请把它挂在初始场景中,请不要做过多的额外操作
/// </summary>
public class GameLoop : MonoBehaviour
{
    public static bool hasStartUp = false;

    private void Awake()
    {
        if (hasStartUp) throw new Exception("You have already start up game! Please check GameLoop component in the scene!");
        else
        {
            gameObject.name = "GameLoop";
        }
        //SceneManager.LoadScene();
    }

    // Use this for initialization
    void Start()
    {
        UIManager.Instance.OpenFrame("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
