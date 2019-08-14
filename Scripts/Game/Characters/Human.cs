using Assets.Scripts.Game.Characters.States;
using System;
using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Game.Characters
{
    internal class Human : Character
    {
        protected override void InitStateMachine()
        {

        }
    }
    internal class HumanIdle : Idle
    {
        private string[] animationClipNames = new string[] { "Human_Idle" };

        internal override string[] AnimationClipNames { get { return animationClipNames; } }

        protected override void OnExit()
        {

        }
        protected override void OnStart()
        {
            stateMachine.animation.CrossFade(animationClipNames[0], 0.1f);
            stateMachine.animation.wrapMode = WrapMode.Loop;
        }
        protected override void OnUpdate()
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                stateMachine.ChangeState(Run.name);
            }
        }
    }
    internal class HumanRun : Run
    {
        internal override string[] AnimationClipNames => throw new NotImplementedException();

        protected override void OnExit()
        {
            throw new NotImplementedException();
        }

        protected override void OnStart()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdate()
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                stateMachine.ChangeState(Idle.name);
            }
        }
    }
}
