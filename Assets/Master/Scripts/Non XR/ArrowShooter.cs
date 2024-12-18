using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // The arrow prefab
    private Camera playerCamera; // The player's camera (child of the player or bow object)
    public Transform CameraPivot;
    public Transform CameraHandle;
    public int poolSize = 10; // Number of arrows in the pool
    public float shootForce = 30f; // The speed of the arrow when shot

    private List<GameObject> arrowPool; // Pool of arrows

    void Start()
    {
        // Initialize the arrow pool
        arrowPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false); // Deactivate the arrow initially
            arrowPool.Add(arrow); // Add it to the pool
        }

        playerCamera = Camera.main;
    }

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            // Check if the right mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                ShootArrow();
            }
        }
    }

    private void LateUpdate()
    {
        // Update camera pivot and transfer properties from camera handle to Main Camera.
        // CameraPivot.rotation = Quaternion.Euler(PlayerInput.CurrentInput.LookRotation);
        Camera.main.transform.SetPositionAndRotation(CameraHandle.position, CameraHandle.rotation);
    }

    void ShootArrow()
    {
        // Get an inactive arrow from the pool
        GameObject arrow = GetPooledArrow();

        if (arrow != null)
        {
            // Set the arrow's position to the camera's position (center of the camera view)
            arrow.transform.position = playerCamera.transform.position;

            // Set the arrow's rotation to match the camera's forward direction
            arrow.transform.rotation = playerCamera.transform.rotation;

            // Activate the arrow
            arrow.SetActive(true);

            // Get the Rigidbody component to apply force
            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            // Reset the arrow's velocity and apply force to shoot it forward in the direction the camera is facing
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(playerCamera.transform.forward * shootForce, ForceMode.Impulse);
        }
    }

    // This method returns an inactive arrow from the pool
    GameObject GetPooledArrow()
    {
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                return arrow;
            }
        }
        return null; // No inactive arrows available
    }
}
