using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _boxCollider;
    private float _horizontalInput;

    private float jumpCooldown = 0.5f;
    private float timeSinceJump;

    private bool isUpsideDown;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _body.gravityScale = 7;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            Jump();

        if (!isUpsideDown)
        {
            if (_horizontalInput > 0.01f)
                transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
            else if (_horizontalInput < -0.01f)
                transform.localScale = new Vector3((float)-0.5, (float)0.5, 1);
        }
        else
        {
            if (_horizontalInput > 0.01f)
                transform.localScale = new Vector3((float)-0.5, (float)0.5, 1);
            else if (_horizontalInput < -0.01f)
                transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
        }

        timeSinceJump += Time.deltaTime;
    }

    private void Jump()
    {
        //checkt of speler op een ground staat en of er genoeg tijd is verstreken sinds de laatste jump.
        //zo ja, spring!
        if (IsGrounded() && timeSinceJump > jumpCooldown)
        {
            _body.velocity = new Vector2(_body.velocity.x, isUpsideDown ? -jumpPower : jumpPower);
            timeSinceJump = 0f;
        }
    }

    private bool IsGrounded()
    {
        //Boxcast naar onder voor groundcheck. Boxcast naar boven als Player upside down is 
        Vector2 direction = isUpsideDown ? Vector2.up : Vector2.down;
        var bounds = _boxCollider.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            bounds.center,
            bounds.size,
            0,
            direction,
            0.1f,
            groundLayer);
        return raycastHit.collider != null;
    }

    // POWERUPS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            jumpPower = 15f;
            speed = 10f;
        }

        if (collision.CompareTag("Jumpboost"))
        {
            Destroy(collision.gameObject);
            if (jumpPower != 25f)
                jumpPower = 25f;
            else
                jumpPower = 15f;
        }

        if (collision.CompareTag("Speedboost"))
        {
            Destroy(collision.gameObject);
            speed = 16f;
        }

        if (collision.CompareTag("AntiGravity"))
        {
            Destroy(collision.gameObject);
            isUpsideDown = !isUpsideDown;
            _body.gravityScale = isUpsideDown ? -7 : 7;
            _body.transform.Rotate(0,0,180);
        }
    }
}