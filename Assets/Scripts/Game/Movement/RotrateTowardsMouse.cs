using System;
using UnityEngine;

namespace Game.Movement
{
    public class RotrateTowardsMouse : MonoBehaviour
    {
        private Camera main;

        private void Awake()
        {
            if (Camera.main == null)
            {
                Debug.LogError("Main camera is not assigned");
                return;
            }

            main = Camera.main;
        }

        Ray cameraRay;              // The ray that is cast from the camera to the mouse position
        RaycastHit cameraRayHit;    // The object that the ray hits

        public void Rotate()
        {
            if (main == null)
            {
                main = Camera.main;
            }

            // Cast a ray from the camera to the mouse cursor
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If the ray strikes an object...
            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                // ...and if that object is the ground...
                if (cameraRayHit.transform.tag == "Ground")
                {
                    // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                    Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                    transform.LookAt(targetPosition);
                }
            }

            //Vector3 mousePosition = main.ScreenToWorldPoint(Input.mousePosition);
            //transform.rotation = Quaternion.LookRotation(mousePosition.normalized);
        }
    }
}