using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{

    private PlayerStats playerStats;

    [Header("Movement Stats")]
    public float jumpForce;
    public float jumpCD;
    private float airMultipliear = 20;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    public Rigidbody rb3d;
    private Vector3 moveDirection;
    public Transform orientation;
    public GameObject playerModel;
    public Transform modelView;

    [Header("KeyBinds")]
    private KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float groundDrag;
    private bool readyToJump;
    [HideInInspector] public bool isOnGround = false;

    [Header("Animator")]
    public Animator playerAnimator;

    [Header("Player Camera")]
    public GameObject playerCamera;

    [Header("Player Network")]
    public static PlayerController localPlayer;
    [SyncVar] public string matchId;
    private NetworkMatch networkMatch;

    void Start()
    {
        networkMatch = GetComponent<NetworkMatch>();
        playerStats = GetComponent<PlayerStats>();
        rb3d = GetComponent<Rigidbody>();
        rb3d.freezeRotation = true;

        readyToJump = true;

        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
            MainMenu.sharedInstance.SpawnPlayerUIPrefab(this);
        }
        else
        {
            localPlayer = this;
        }
    }

    // HOST
    public void HostGame()
    {
        string id = MainMenu.GetRandomId();
        CmdHostGame(id);
    }

    [Command]
    public void CmdHostGame(string id)
    {
        matchId = id;
        if (MainMenu.sharedInstance.HostGame(id, gameObject))
        {
            Debug.Log("Lo que dijo el Ruso :)");
            networkMatch.matchId = id.ToGuid();
            TargetHostGame(true, id);
        }
        else
        {
            Debug.Log("Lo que dijo el Ruso x2 :(");
            TargetHostGame(false, id);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string id)
    {
        matchId = id;
        Debug.Log($"ID {matchId} == {id}");
        MainMenu.sharedInstance.HostSuccess(success, id);
    }

    // JOIN
    public void JoinGame(string inputId)
    {
        CmdJoinGame(inputId);
    }

    [Command]
    public void CmdJoinGame(string id)
    {
        matchId = id;
        if (MainMenu.sharedInstance.JoinGame(id, gameObject))
        {
            Debug.Log("Lo que dijo el Ruso x3 :)");
            networkMatch.matchId = id.ToGuid();
            TargetJoinGame(true, id);
        }
        else
        {
            Debug.Log("Lo que dijo el Ruso x4 :(");
            TargetJoinGame(false, id);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string id)
    {
        matchId = id;
        Debug.Log($"ID {matchId} == {id}");
        MainMenu.sharedInstance.JoinSuccess(success, id);
    }

    // BEGIN
    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        MainMenu.sharedInstance.BeginGame(matchId);
        Debug.Log("Creo que el ruso dijo que va comenzar");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }

    [TargetRpc]
    public void TargetBeginGame()
    {
        if(MainMenu.sharedInstance.inGame == false)
        {
            Debug.Log($"ID {matchId} == entro");
            DontDestroyOnLoad(gameObject);
            MainMenu.sharedInstance.inGame = true;
            transform.localScale = new Vector3(1, 1, 1);
            playerCamera.GetComponent<Camera>().enabled = true;
            rb3d.isKinematic = false;
            StartCoroutine(StartCombatScene());
        }
    }

    public IEnumerator StartCombatScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Update()
    {
        if(!isLocalPlayer) { return;  }

        MyInput();
        PlayerMovementAnimations();
        PlayerAbilitiesAnimation();

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
        if (!isLocalPlayer) { return; }
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

    private void PlayerMovementAnimations()
    {
        if (verticalInput > 0)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

        if (horizontalInput > 0)
        {
            playerAnimator.SetBool("RightWalk", true);
        }
        else
        {
            playerAnimator.SetBool("RightWalk", false);
        }

        if (horizontalInput < 0)
        {
            playerAnimator.SetBool("LeftWalk", true);
        }
        else
        {
            playerAnimator.SetBool("LeftWalk", false);
        }

        if (isOnGround == false)
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }
    }

    private void PlayerAbilitiesAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("BombThrow", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerAnimator.SetBool("BombThrow", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(RockAttack());
        }
    }

    IEnumerator RockAttack()
    {
        playerAnimator.SetBool("Rocks", true);
        yield return new WaitForSeconds(1);
        playerAnimator.SetBool("Rocks", false);
    }
}
