using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Game.Characters.States
{
    /// <summary>
    /// 状态
    /// </summary>
    internal abstract class State
    {
        static State()
        {
            var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsClass && type.IsSubclassOf(typeof(State)) && !type.IsAbstract
                        select type;
            types.ToList().ForEach((type) =>
            {
                State instance = (State)Activator.CreateInstance(type);
                nameToInstance.Add(instance.Name, instance);
                nameToType.Add(instance.Name, type);
            });
        }

        private static Dictionary<string, Type> nameToType = new Dictionary<string, Type>();
        private static Dictionary<string, State> nameToInstance = new Dictionary<string, State>();

        internal static State CreateInstanceByName(string name)
        {
            return (State)Activator.CreateInstance(nameToType[name]);
        }

        public StateMachine stateMachine;

        /// <summary>
        /// 在当前状态经过的时间
        /// </summary>
        protected float time;

        private float startTime;

        internal abstract string Name { get; }
        internal abstract string[] AnimationClipNames { get; }

        internal void Start()
        {
            startTime = Time.time;
            OnStart();
        }
        internal void Update()
        {
            time = Time.time - startTime;
            OnUpdate();
        }
        internal void Exit()
        {
            startTime = 0;
            time = 0;
            OnExit();
        }
        
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnExit();

    }
}
