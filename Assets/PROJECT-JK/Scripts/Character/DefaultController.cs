using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JK
{
    public class DefaultController : MonoBehaviour
    {
        CharacterBase characterBase;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            InputManager.Singleton.Attack += Attack;
            InputManager.Singleton.Jump += Jump;
            InputManager.Singleton.Roll += Roll;
        }

        private void Update()
        {
            characterBase.IsStrafe = InputManager.Singleton.OnMouseRight;
            characterBase.IsSprint = InputManager.Singleton.OnSprintKey;
            characterBase.IsCrouch = InputManager.Singleton.OnCrouchKey;
            characterBase.HorizontalMove(InputManager.Singleton.MoveInput, Camera.main.transform.rotation.eulerAngles.y);
        }

        private void Roll()
        {
            characterBase.Roll();
        }

        private void Jump()
        {
            characterBase.Jump();
        }

        private void Attack()
        {
            characterBase.Attack();
        }
    }
}
