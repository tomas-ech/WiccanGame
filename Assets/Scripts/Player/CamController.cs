using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rbCam;
    public float rotationSpeed;

    public CameraStyle currentStyle;
    public Transform combatLootAt;

    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotar el objeto orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

        orientation.forward = viewDir.normalized;
        
        //Rotar el objeto jugador
        if (currentStyle == CameraStyle.Basic)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime;

            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

            Vector3 inputDir = orientation.forward * mouseY + orientation.right * mouseX;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }

        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLootAt = combatLootAt.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

            orientation.forward = dirToCombatLootAt.normalized;

            playerObj.forward = dirToCombatLootAt.normalized;
        }
    }
}
