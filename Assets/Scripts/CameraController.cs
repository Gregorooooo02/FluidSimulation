using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject mainCamera;

    // Add a target for the camera to follow
    [SerializeField] private Transform target;

    // Input to rotate the camera
    [Header("Input Settings")]
    [SerializeField] private KeyCode rotateKey = KeyCode.Mouse2;
    [SerializeField] private float rotateSpeed = 5f;

    // Scroll wheel input to zoom in and out
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;

    private void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void Update() {
        // Zoom in and out
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0) {
            Vector3 direction = mainCamera.transform.position - target.position;
            float distance = direction.magnitude;

            direction.Normalize();
            direction *= Mathf.Clamp(distance - scrollWheel * zoomSpeed, minZoom, maxZoom);

            mainCamera.transform.position = target.position + direction;
        }

        // Rotate the camera
        if (Input.GetKey(rotateKey)) {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.RotateAround(target.position, Vector3.up, mouseX);
            transform.RotateAround(target.position, transform.right, -mouseY);
        }
    }
}
