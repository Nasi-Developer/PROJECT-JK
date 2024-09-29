using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JK
{
    [System.Serializable]
    public class CharacterDefaultData
    {
        public int Level;
        public int LevelBasedHP;
        public int LevelBasedAttack;
        public int LevelBasedArmor;
        public int LevelBasedAccuracy;
        //public float CriticalRate;
        //public float CriticalDamage;
        public int Ammo;
        public float FiringRange;
        public float CostRecovery;
        public float MovementSpeed = 1f;
        public int AttackSpeed;
    }

  

    public class CharacterBase : MonoBehaviour
    {
        public CharacterBase()
        {
            CharacterDefaultData characterDefaultData = new CharacterDefaultData();
        }

        protected CharacterDefaultData characterDefaultData;
        protected Animator animator;
        protected UnityEngine.CharacterController characterController;

        protected float CurrentVelocity;
        protected float RotationSmoothTime = 0.12f;

        public bool isstrafe;
        public bool iscrouch;
        public bool issprint;

        protected bool IsStrafe
        {
            get => isstrafe;
            set => isstrafe = value;
        }

        protected bool IsCrouch
        {
            get => iscrouch;
            set => iscrouch = value;
        }

        protected bool IsSprint
        {
            get => issprint;
            set => issprint = value;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<UnityEngine.CharacterController>();
        }

        public void HorizontalMove(Vector2 input, float Yangle)
        {
            Vector3 Direction = new Vector3(input.x, 0, input.y).normalized;
            float Angle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Yangle;
            float Rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref CurrentVelocity, RotationSmoothTime);


            if (!isstrafe)
            {
                animator.SetBool("IsStrafe", true);
                transform.rotation = Quaternion.Euler(0, Rotation, 0);
            }

            else
            {
                animator.SetBool("IsStrafe", false);
            }

            Vector3 FinalDirection = Quaternion.Euler(0, Angle, 0) * Vector3.forward;

            if (input.magnitude <= 0.1f)
            {
                FinalDirection = Vector3.zero;
            }

            characterController.Move(FinalDirection.normalized * Time.deltaTime * characterDefaultData.MovementSpeed);
        }

        internal void Roll()
        {
            throw new NotImplementedException();
        }

        internal void Jump()
        {
            throw new NotImplementedException();
        }

        internal void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
