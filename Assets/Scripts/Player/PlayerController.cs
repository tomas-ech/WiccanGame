using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerStats playerStats;
    private Animator playerAnimator;

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
        playerAnimator = GameObject.Find("PlayerModel").GetComponent<Animator>();
        rb3d = GetComponent<Rigidbody>();
        
        rb3d.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        MyInput();
        PlayerMovementAnimations();
        PlayerAbilitiesAnimation();
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
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isOnGround && (horizontalInput !=0 || verticalInput > 0))
        {
            rb3d.AddForce(moveDirection.normalized * playerStats.characterSpeed * 10f, ForceMode.Force);
        }
        else if (isOnGround && (horizontalInput !=0 || verticalInput < 0))
        {
            rb3d.AddForce(moveDirection.normalized * playerStats.characterSpeed/2 * 10f, ForceMode.Force);
        }
        else{
            rb3d.AddForce(moveDirection.normalized * airMultipliear *10f, ForceMode.Force);
        }

        if (Input.GetKey(jumpKey) && readyToJump && isOnGround)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCD);
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

        if (isOnGround) {rb3d.drag = groundDrag;}
        else {rb3d.drag = 0;}
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
        if (other.gameObject.CompareTag("Water"))
        {
            playerAnimator.SetBool("IsSwimming", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            playerAnimator.SetBool("IsSwimming", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            playerAnimator.SetBool("IsSwimming", false);
        }
    }

    private void PlayerMovementAnimations()
    {
        //Movimiento verticales
        if (verticalInput > 0)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else if (verticalInput < 0)
        {
            playerAnimator.SetBool("WalkBack", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
            playerAnimator.SetBool("WalkBack", false);
        }
        //Movimientos horizontales
        if (horizontalInput > 0)
        {
            playerAnimator.SetBool("RightWalk", true);
        }
        else if (horizontalInput < 0)
        {
            playerAnimator.SetBool("LeftWalk", true);
        }
        else
        {
            playerAnimator.SetBool("RightWalk", false);
            playerAnimator.SetBool("LeftWalk", false);
        }
        //Salto
        if (Input.GetKey(jumpKey))
        {
            StartCoroutine(JumpAnimation());
        }
       
    }
    private void PlayerAbilitiesAnimation()
    {
        //Primary Attack
        if (Input.GetMouseButtonDown(0)) {StartCoroutine(BombAttack());}
        //Secundary Attack
        if (Input.GetMouseButtonDown(1)) {StartCoroutine(RockAttack());}
    }

    IEnumerator JumpAnimation()
    {
        playerAnimator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(1);
        playerAnimator.SetBool("IsJumping", false);
    }
    IEnumerator RockAttack()
    {
        rb3d.constraints = RigidbodyConstraints.FreezePosition;
        playerAnimator.SetBool("Rocks", true);
        yield return new WaitForSeconds(2);
        playerAnimator.SetBool("Rocks", false);
        rb3d.constraints = ~RigidbodyConstraints.FreezePosition;
    }
    IEnumerator BombAttack()
    {
        rb3d.constraints = RigidbodyConstraints.FreezePosition;
        playerAnimator.SetBool("BombThrow", true);
        yield return new WaitForSeconds(1);
        playerAnimator.SetBool("BombThrow", false);
        rb3d.constraints = ~RigidbodyConstraints.FreezePosition;
    }
}
