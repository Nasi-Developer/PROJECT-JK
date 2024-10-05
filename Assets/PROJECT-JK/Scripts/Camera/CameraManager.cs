using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JK
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; } = null;
        public Vector3 DesiredPoint { get; private set; } = Vector3.zero;
        public LayerMask layermask;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));

            if(Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layermask))
            {
                DesiredPoint = hitInfo.point;
            }

            else
            {
                ray.GetPoint(1000f);
            }
        }
    }
}
