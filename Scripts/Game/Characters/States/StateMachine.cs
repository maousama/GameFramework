using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Characters.States
{
    [RequireComponent(typeof(Animation))]
    internal class StateMachine : MonoBehaviour
    {
        /// <summary>
        /// 使用改状态机的角色
        /// </summary>
        internal Character owner;
        /// <summary>
        /// 状态机的动画组件
        /// </summary>
        internal new Animation animation;

        /// <summary>
        /// 状态机的状态字典通过状态的名字查找
        /// </summary>
        private Dictionary<string, State> nameToState;
        /// <summary>
        /// 当前状态
        /// </summary>
        private State currentState;
        /// <summary>
        /// 动画名引用次数计数字典
        /// </summary>
        private Dictionary<string, int> animationClipNameReferenceCounter = new Dictionary<string, int>();

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        private void AddState(State state)
        {
            if (nameToState.ContainsKey(state.Name))
            {
                Debug.LogError("Cannot add " + state.Name + " state repeatedly in " + owner);
                return;
            }
            else
            {
                nameToState.Add(state.Name, state);
                string[] animationClipNames = state.AnimationClipNames;
                for (int i = 0; i < animationClipNames.Length; i++)
                {
                    AddAnimationClips(animationClipNames[i]);
                }
            }
        }
        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="stateName"></param>
        private void RemoveState(string stateName)
        {
            if (!nameToState.ContainsKey(stateName))
            {
                Debug.LogError("Cannot remove " + stateName + " state repeatedly in " + owner);
                return;
            }
            else
            {
                string[] animationClipNames = nameToState[stateName].AnimationClipNames;
                for (int i = 0; i < animationClipNames.Length; i++)
                {
                    RemoveAnimationClips(animationClipNames[i]);
                }
                nameToState.Remove(stateName);
            }

        }

        /// <summary>
        /// 添加动画片段
        /// </summary>
        /// <param name="animationClipNames"></param>
        private void AddAnimationClips(string animationClipName)
        {
            if (animationClipNameReferenceCounter.ContainsKey(animationClipName))
            {
                animationClipNameReferenceCounter[animationClipName]++;
            }
            else
            {
                AnimationClip animationClip = AssetsManager.AssetsAgent.GetAsset<AnimationClip>(animationClipName);
                if (animationClip == null)
                {
                    Debug.LogError(animationClipName + "not exist");
                    return;
                }
                else
                {
                    animationClipNameReferenceCounter.Add(animationClipName, 1);
                    animation.AddClip(animationClip, animationClipName);
                }
            }

        }
        /// <summary>
        /// 移除动画片段
        /// </summary>
        /// <param name="animationClipName"></param>
        private void RemoveAnimationClips(string animationClipName)
        {
            if (!animationClipNameReferenceCounter.ContainsKey(animationClipName))
            {
                Debug.LogError(animationClipName + " not in animationClipNameReferenceCounter");
                return;
            }
            animationClipNameReferenceCounter[animationClipName]--;
            if (animationClipNameReferenceCounter[animationClipName] == 0)
            {
                animationClipNameReferenceCounter.Remove(animationClipName);
                animation.RemoveClip(animationClipName);
            }
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        /// <param name="nameToState"></param>
        /// <param name="origionStateName"></param>
        internal void Init(State[] states, string origionStateName)
        {
            animation = GetComponent<Animation>();
            owner = GetComponent<Character>();

            foreach (State state in states) AddState(state);
            ChangeState(origionStateName);
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="targetStateName"></param>
        internal void ChangeState(string targetStateName)
        {
            if (currentState != null) currentState.Exit();
            currentState = nameToState[targetStateName];
            if (currentState != null) currentState.Start();
        }
    }
}
