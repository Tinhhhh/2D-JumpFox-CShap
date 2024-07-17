using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpValue = 0.0f;
    [SerializeField] private CapsuleCollider2D cc;
    [SerializeField] private PhysicsMaterial2D bounceMat, normalMat;

    public static bool isInputEnabled;

    private float dirX;
    private float dirY;
    private Rigidbody2D rb;
    public bool canJump = true;
    private BoxCollider2D bc;

    private PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        isInputEnabled = true;
        cc = GetComponent<CapsuleCollider2D>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("isContinue"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);

            GemManager.instance.gems = PlayerPrefs.GetInt("Gem");
            PlayerPrefs.DeleteKey("isContinue");
            Debug.Log("Player position loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInputEnabled)
        {
            return;
        }
        if (IsGrounded())
        {
            canJump = true;
            Movement();
        }
        else
        {
            Debug.Log("ko tren mat dat");
        }
        Jump();

        //chỉ cho di chuyển khi không nhảy và đang chạm đất
        if (jumpValue == 0 && IsGrounded())
        {
            bc.sharedMaterial = normalMat;
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }

        if (IsGrounded())
        {
            dirY = 0;
            playerAnimation.SetJumpingAnimation(dirY);
        }

        if (jumpValue > 0.0f)
        {
            playerAnimation.setHoldJumpAnimation(true);
        }
        else
        {
            playerAnimation.setHoldJumpAnimation(false);
        }

    }
    private void Jump()
    {
        //khi ấn nút nhảy thì bắt đầu tụ lực
        if (Input.GetKey("space") && IsGrounded() && canJump)
        {
            jumpValue += 0.5f;
            changeMaterial();

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
            if (IsGrounded())
            {
                rb.velocity = new Vector2(dirX * speed, jumpValue);
                jumpValue = 0.0f;
            }

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
            playerAnimation.SetMovingAnimation(dirX);

        }

    }


    private bool IsGrounded()
    {
        //kiểm tra 
        // return Physics2D.OverlapCircle(cc.bounds.center, cc.bounds.size.y, groundLayer);
        return Physics2D.OverlapBox(cc.bounds.center, cc.bounds.size, 0f, groundLayer);
        // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void changeMaterial()
    {
        bc.sharedMaterial = bounceMat;
    }

    private void OnDrawGizmos()
    {
        // Lấy kích thước của Collider2D
        Vector2 size = cc.bounds.size;

        // Lấy vị trí của Collider2D
        Vector3 position = cc.bounds.center;

        Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(groundCheck.position, 0.2f);

        // Vẽ hình vuông chỉ với các đường viền tại vị trí của Collider2D và có kích thước bằng với kích thước của Collider2D
        Gizmos.DrawWireCube(position, size);

    }

}
