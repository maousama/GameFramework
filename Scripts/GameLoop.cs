using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 作为游戏住循环控制组件请把它挂在初始场景中,请不要做过多的额外操作
/// </summary>
public class GameLoop : MonoBehaviour
{
    private void Awake()
    {
        gameObject.name = "GameLoop";
        if (GameObject.Find("GameLoop")) throw new Exception("There is already a GameLoop in this scene, you can create anohter one!!");
        GameLoop[] gameLoops = gameObject.GetComponents<GameLoop>();
        if (gameLoops.Length > 1) throw new Exception("There are multiple components on the GameLoop! Please remove the extra components!");
        return;
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
