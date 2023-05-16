using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerStats playerStats;

    [Header("Movement Stats")]
    public float jumpForce;
    public float jumpCD;
    private float airMultipliear = 20;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    private Rigidbody rb3d;
    private Vector3 moveDirection;
    public Transform orientation;
    public GameObject playerModel;
    public Transform modelView;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float groundDrag;
    private bool readyToJump;
    [HideInInspector] public bool isOnGround = false;
    
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb3d = GetComponent<Rigidbody>();
        rb3d.freezeRotation = true;

        readyToJump = true;

    }

    private void Update()
    {
        MyInput();

        if (isOnGround)
        {
            rb3d.drag = groundDrag;
        }
        else{
            rb3d.drag = 0;
        }

        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Movimiento del jugador
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        playerModel.transform.rotation = orientation.transform.rotation;

        if (Input.GetKey(jumpKey) && readyToJump && isOnGround)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCD);
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isOnGround)
        {
            rb3d.AddForce(moveDirection.normalized * playerStats.characterSpeed * 10f, ForceMode.Force);
        }
        else{
            rb3d.AddForce(moveDirection.normalized * airMultipliear *10f, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb3d.velocity.x, 0f, rb3d.velocity.z);

        if (flatVel.magnitude > playerStats.characterSpeed )
        {
            Vector3 limitedVel = flatVel.normalized *playerStats.characterSpeed ;
            rb3d.velocity = new Vector3(limitedVel.x, rb3d.velocity.y, limitedVel.z);
        }

    }
    private void Jump()
    {
        rb3d.velocity = new Vector3(rb3d.velocity.x, 0f, rb3d.velocity.z);

        rb3d.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Permite al jugador saltar si esta en collision con el tag Ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //Evita doble saltos al salir de la collision con Ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
