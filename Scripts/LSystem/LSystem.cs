using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class LSystem : MonoBehaviour
{
    private Dictionary<char, Action> commands;
    private Dictionary<char, string> rules;
    public int times;
    public float angle;
    public string sentence;

    private float length = 2;
    private GameObject point;
    private Stack<TransformInfo> transformInfoStack;

    private void Awake()
    {
        point = Instantiate(new GameObject("Point"), transform);
        transformInfoStack = new Stack<TransformInfo>();

        commands = new Dictionary<char, Action>();
        commands.Add('F', delegate ()
        {
            //向前绘制一步
            Vector3 startPosition = point.transform.position;
            point.transform.Translate(Vector3.forward * length);
            Debug.DrawLine(startPosition, point.transform.position, Color.white, 1000);
        });
        commands.Add('f', delegate ()
        {
            //向前走一步不绘制
        });
        commands.Add('+', delegate ()
        {
            //环绕y轴旋转pitch角度
            point.transform.Rotate(Vector3.up, angle);
        });
        commands.Add('-', delegate ()
        {
            //环绕y轴旋转-pitch角度
            point.transform.Rotate(Vector3.up, -angle);
        });
        commands.Add('>', delegate ()
        {
            //环绕z轴旋转yaw角度
            point.transform.Rotate(Vector3.forward, angle);
        });
        commands.Add('<', delegate ()
        {
            //环绕z轴旋转-yaw角度
            point.transform.Rotate(Vector3.forward, -angle);
        });
        commands.Add('&', delegate ()
        {
            //环绕x轴旋转roll角度
            point.transform.Rotate(Vector3.right, angle);
        });
        commands.Add('^', delegate ()
        {
            //环绕x轴旋转-roll角度
            point.transform.Rotate(Vector3.right, -angle);
        });
        commands.Add('[', delegate ()
        {
            //将当前状态压入栈
            TransformInfo ti = new TransformInfo();
            ti.position = point.transform.position;
            ti.rotation = point.transform.rotation;
            transformInfoStack.Push(ti);
        });
        commands.Add(']', delegate ()
        {
            //从栈中取出一个状态作为当前状态
            TransformInfo ti = transformInfoStack.Pop();
            point.transform.position = ti.position;
            point.transform.rotation = ti.rotation;
        });
        commands.Add('l', delegate ()
        {
            //长一个叶子
            //向前绘制一步
            Vector3 startPosition = point.transform.position;
            point.transform.Translate(Vector3.forward * length);
            Debug.DrawLine(startPosition, point.transform.position, Color.green, 1000);
        });

        rules = new Dictionary<char, string>();
        rules.Add('A', "[&FL!A]<<<<<l[&FL!A]<<<<<<<l[&FL!A]");
        rules.Add('F', "S<<<<<F");
        rules.Add('S', "FL");
        rules.Add('L', "[lll^^{-f+f+f-|-f+f+f}]");

        times = 6;
        sentence = "A";
        angle = 45;

        StartCoroutine(Execute());
    }

    public IEnumerator Execute()
    {
        //处理语句
        ProcessSentence();
        Debug.Log(sentence);
        for (int i = 0; i < sentence.Length; i++)
        {
            char commandKey = sentence[i];
            if (commands.ContainsKey(commandKey)) commands[commandKey].Invoke();

            yield return null;
        }
    }

    private void ProcessSentence()
    {
        if (times == 0) return;
        else
        {
            times--;
            StringBuilder nextSentence = new StringBuilder();
            for (int i = 0; i < sentence.Length; i++)
            {
                char currentChar = sentence[i];
                if (rules.ContainsKey(currentChar)) nextSentence.Append(rules[currentChar]);
                else nextSentence.Append(currentChar);
            }
            sentence = nextSentence.ToString();
        }
        ProcessSentence();
    }
}
public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}