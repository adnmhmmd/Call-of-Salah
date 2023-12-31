using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform Player, ViewPoint;
    public float RotationSpeed;
    public GameObject TPSCamera;
    bool TPSMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        ViewPoint.forward = viewDir.normalized;

        if (TPSMode)
        {
            Vector3 InputDir = ViewPoint.forward * verticalInput + ViewPoint.right * horizontalInput;

            if (InputDir != Vector3.zero)
            {
                Player.forward = Vector3.Slerp(Player.forward, InputDir.normalized, Time.deltaTime * RotationSpeed);
            }
        }
    }

    public void CameraModeChanger(bool TPS, bool AIM)
    {
        if (TPS)
        {
            TPSCamera.SetActive(true);
        }
    }
}
