using UnityEngine;

/// <summary>
/// This class is responsible for calculating if the users can move the camera around.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    private Vector3 CameraPosition;
    [SerializeField] private float CameraSpeed;
    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;

    //Will tell the user they can move the camera and will fix the camera to the center of the grid
    private void Start()
    {
        Debug.Log("can move camera");
        CameraPosition.x = (float)width / 2 - 0.5f;
        CameraPosition.y = (float)height / 2 - 0.5f;
        CameraPosition.z = -10;
        
    }

    //This will allow the users to move the camera
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            CameraPosition.y += CameraSpeed / 10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            CameraPosition.y -= CameraSpeed / 10;
        }
        if (Input.GetKey(KeyCode.A))
        {
            CameraPosition.x -= CameraSpeed / 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CameraPosition.x += CameraSpeed / 10;
        }

        this.transform.position = CameraPosition;
    }
}
