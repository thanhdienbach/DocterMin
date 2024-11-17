using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 5f;
    public float playerTurnSpeed = 80f;
    public float playerHorizontalInput, playerVerticalInput;

    public Rigidbody playerRigidbody;
    public float playerJumpForce;
    public float gravity = -9.8f;
    public float height = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;
    public bool playerIsGround = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMove();
        if (Input.GetButton("Jump") && PlayerIsGround())
        {
            PlayerJump();
        }

    }
    void PlayerMove()
    {

        playerHorizontalInput = Input.GetAxis("Horizontal");
        playerVerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * playerVerticalInput);
        transform.Rotate(Vector3.up, playerTurnSpeed * Time.deltaTime * playerHorizontalInput);

    }
    void PlayerJump()
    {
        playerJumpForce = Mathf.Sqrt(-2 * Physics.gravity.y * height);
        playerRigidbody.AddForce(Vector3.up * playerJumpForce, ForceMode.Force);
        playerIsGround = false;
    }


    bool PlayerIsGround()
    {
        playerIsGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        return playerIsGround;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
}
