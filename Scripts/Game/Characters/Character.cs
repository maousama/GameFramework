using Assets.Scripts.Game.Characters.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Characters
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(StateMachine))]
    internal abstract class Character : MonoBehaviour
    {
        internal new Rigidbody rigidbody;
        internal new CapsuleCollider collider;
        internal StateMachine stateMachine;

        internal Action<int, int> OnHPChange;
        internal Action<int, int> OnAGLChange;
        internal Action<float, float> OnHeightChange;
        internal Action<float, float> OnRadiusChange;
        internal Action<float, float> OnWeightChange;

        private int hp;
        private int agl;
        private float height;
        private float radius;
        private float weight;

        /// <summary>
        /// 影响碰撞块的高度
        /// </summary>
        internal float Height
        {
            get => height;
            set
            {
                OnHeightChange(height, value);
                height = value;
            }
        }
        /// <summary>
        /// 影响碰撞快的半径
        /// </summary>
        internal float Radius
        {
            get => radius;
            set
            {
                OnRadiusChange(radius, value);
                radius = value;
            }
        }
        /// <summary>
        /// 影响质量
        /// </summary>
        internal float Weight
        {
            get => weight;
            set
            {
                OnWeightChange(weight, value);
                weight = value;
            }
        }
        /// <summary>
        /// 影响血量
        /// </summary>
        internal int HP
        {
            get => hp;
            set
            {
                OnHPChange?.Invoke(hp, value);
                hp = value;
            }
        }
        /// <summary>
        /// 影响移动速度
        /// </summary>
        internal int AGL
        {
            get => agl;
            set
            {
                OnAGLChange?.Invoke(agl, value);
                agl = value;
            }
        }


        private void InitComponents()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();

            OnHeightChange += delegate (float oldValue, float newValue) { collider.height = newValue; };
            OnRadiusChange += delegate (float oldValue, float newValue) { collider.radius = newValue; };
            OnWeightChange += delegate (float oldValue, float newValue) { rigidbody.mass = newValue; };
        }

        protected abstract void InitStateMachine();

        protected virtual void InitAttributes(int hp, int agl, float height, float radius, float weight)
        {
            Height = height;
            Radius = radius;
            HP = hp;
            AGL = agl;
            Height = height;
        }
    }
}
