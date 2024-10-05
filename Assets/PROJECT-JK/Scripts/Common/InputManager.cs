using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JK
{

    public class InputManager : SingletonBase<InputManager>
    {
        public Vector2 MoveInput;
        public Vector2 LookInput;

        public System.Action Attack;
        public System.Action Jump;
        public System.Action Roll;

        public bool OnMouseRight;
        public bool OnCrouchKey;
        public bool OnSprintKey;

        private void Update()
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");
            MoveInput = new Vector2(Horizontal, Vertical);

            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");
            LookInput = new Vector2(MouseX, MouseY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump?.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Attack?.Invoke();
            }

            if (Input.GetMouseButton(2)) 
            {
                OnMouseRight = true;
            }

            else
            {
                OnMouseRight = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                OnCrouchKey = true;
            }

            else
            {
                OnCrouchKey = false;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                OnSprintKey = true;
            }

            else
            {
                OnSprintKey= false;
            }

        }
    }
}
