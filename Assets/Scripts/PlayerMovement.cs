using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpDelay = 0.75f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CapsuleCollider2D bc;
    [SerializeField] private float jumpValue = 0.0f;
    [SerializeField] private PhysicsMaterial2D bounceMat, normalMat;


    private float dirX;
    private float dirY;
    private Rigidbody2D rb;
    public bool canJump = true;
    private float lastJumpTime;

    private PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        lastJumpTime = -jumpDelay;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        //chỉ cho di chuyển khi không nhảy và đang chạm đất
        if (jumpValue == 0 && IsGrounded())
        {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }

        Jump();
        if (IsGrounded())
        {
            dirY = 0;
            playerAnimation.SetJumpingAnimation(dirY);
        }

        if (jumpValue > 0.0f && !Input.GetKey("space"))
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }



    }
    private void Jump()
    {
        //khi ấn nút nhảy thì bắt đầu tụ lực
        if (Input.GetKey("space") && IsGrounded() && canJump)
        {
            jumpValue += 0.5f;
        }

        //một khi đã ấn nút nhảy thì sẽ khiến nhân vật đứng lại ngay lập tức
        if (Input.GetKeyDown("space") && IsGrounded() && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpValue >= 25f && IsGrounded())
        {
            float tempx = dirX * speed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        //thả nút nhảy thì thực hiện nhảy
        if (Input.GetKeyUp("space"))
        {
            lastJumpTime = Time.time;
            if (IsGrounded())
            {
                rb.velocity = new Vector2(dirX * speed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
            //Set the animator MoveY
            dirY = rb.velocity.y;
            playerAnimation.SetJumpingAnimation(rb.velocity.y);
        }
        // }

    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0.0f;
    }



    public void Movement()
    {
        //Horizontal and Vertial is default setting 
        //Project settings => Input Manager => Horizontal, Vertical, Jump,...
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX == 0f && dirY == 0f)
        {
            //If player isnt moving => set Idle animation
            playerAnimation.SetIdleAnimation(false);
        }
        else
        {
            playerAnimation.SetIdleAnimation(true);
        }
        playerAnimation.SetMovingAnimation(dirX);

    }


    private bool IsGrounded()
    {
        //kiểm tra 
        return Physics2D.OverlapCircle(bc.bounds.center, bc.bounds.size.x, groundLayer);
        // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        // Lấy kích thước của Collider2D
        Vector2 size = bc.bounds.size;

        // Lấy vị trí của Collider2D
        Vector3 position = bc.bounds.center;

        Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(groundCheck.position, 0.2f);

        // Vẽ hình vuông chỉ với các đường viền tại vị trí của Collider2D và có kích thước bằng với kích thước của Collider2D
        Gizmos.DrawWireCube(position, size);

    }

}
