using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


namespace JK
{
    [System.Serializable]
    public class CharacterDefaultData
    {
        public int Level = 1;
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
        public float AttackSpeed;
        public float JumpForce = 5f;
    }

  

    public class CharacterBase : MonoBehaviour
    {
        public CharacterDefaultData characterDefaultData;
        public bool IsGrounded;
        public LayerMask GroundLayer;
        public float VerticalVelocity;

        protected Animator animator;
        protected UnityEngine.CharacterController characterController;
        protected float CurrentVelocity;
        protected float RotationSmoothTime = 0.12f;

        private float GroundOffset = 0.1f;
        private float GroundRadius = 0.1f;
        private bool isstrafe;
        private bool iscrouch;
        private bool issprint;
        private bool ispossibleattack = true;
        private bool ispossiblejump = true;
        private bool ispossibleroll = true;
        private bool Jumping = false;
        private bool Falling = false;
        private bool Landing = false;
        private float Height;

        

        public bool IsStrafe
        {
            get => isstrafe;
            set => isstrafe = value;
        }

        public bool IsCrouch
        {
            get => iscrouch;
            set => iscrouch = value;
        }

        public bool IsSprint
        {
            get => issprint;
            set => issprint = value;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<UnityEngine.CharacterController>();
            characterDefaultData = new CharacterDefaultData();
        }

        private void Update()
        {
            VerticalMove();
        }

        public void HorizontalMove(Vector2 input, float Yangle)
        {
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);

            Vector3 Direction = new Vector3(input.x, 0, input.y).normalized;
            float Angle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Yangle;
            float Rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref CurrentVelocity, RotationSmoothTime);


            if (!isstrafe)
            {
                animator.SetFloat("Strafe", 0);
                transform.rotation = Quaternion.Euler(0, Rotation, 0);
            }

            else
            {
                animator.SetFloat("Strafe", 1f);
            }

            Vector3 FinalDirection = Quaternion.Euler(0, Angle, 0) * Vector3.forward;

            if (input.magnitude <= 0.1f)
            {
                FinalDirection = Vector3.zero;
                animator.SetFloat("Speed", 0f);
            }

            else
            {
                if (issprint)
                {
                    animator.SetFloat("Speed", 3f);
                }

                else
                {
                    animator.SetFloat("Speed", 1f);
                }
            }

            characterController.Move(FinalDirection.normalized * Time.deltaTime * characterDefaultData.MovementSpeed);
        }

        public void VerticalMove()
        {
            Vector3 SpherePosition = transform.position + (Vector3.down * GroundOffset);
            Debug.Log(SpherePosition);

            if(IsGrounded = Physics.CheckSphere(SpherePosition, GroundRadius, GroundLayer))
            {
                animator.SetBool("IsGrounded", IsGrounded);

                if (Landing)
                {
                    Landing = false;
                    Jumping = false;
                    Falling = false;
                    animator.SetBool("Jumping", false);
                    animator.SetBool("Falling", false);
                    animator.SetBool("Landing", false);
                }
            }

            else
            {
                animator.SetBool("IsGrounded", IsGrounded);
                VerticalVelocity += Physics.gravity.y * Time.deltaTime;

                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, GroundLayer))
                {
                    Height = hit.distance;

                    if (Jumping && !Falling && VerticalVelocity < 0)
                    {
                        Jumping = false;
                        Falling = true;
                        animator.SetBool("Jumping", false);
                        animator.SetBool("Falling", true);
                    }

                    if (Falling && !Landing && Height < 0.25f)
                    {
                        Falling = false;
                        Landing = true;
                        animator.SetBool("Landing", true);
                        animator.SetBool("Falling", false);
                    }
                }

                else
                {
                    Height = 10f;

                    if(!Jumping && !Falling && !Landing)
                    {
                        Falling = true;
                        animator.SetBool("Falling", true);
                        animator.SetTrigger("FallingLoop");
                    }

                    if(Jumping && !Falling)
                    {
                        Jumping = false;
                        Falling = true;
                        animator.SetBool("Jumping", true);
                        animator.SetBool("Falling", true);
                        animator.SetTrigger("FallingLoop");
                    }
                }
            }

            characterController.Move(new Vector3(0, VerticalVelocity, 0) * Time.deltaTime);
        }

        public void Roll()
        {
            throw new NotImplementedException();
        }

        public void Jump()
        {
            if (!ispossiblejump || !IsGrounded)
            {
                return;
            }

            animator.SetTrigger("JumpTrigger");
            animator.SetBool("Jumping", true);
            VerticalVelocity = Mathf.Sqrt(-2 * Physics.gravity.y * characterDefaultData.JumpForce);
            Jumping = true;
            
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
