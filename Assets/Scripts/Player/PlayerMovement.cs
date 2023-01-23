using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _boxCollider;
    private float _wallJumpCooldown;
    private float _horizontalInput;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if(_horizontalInput > 0.01f)
            transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
        else if(_horizontalInput < -0.01f)
            transform.localScale = new Vector3((float)-0.5, (float)0.5, 1);
        
        // Wall jump logic
        if(_wallJumpCooldown > 0.2f)
        {
            _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);

            if(OnWall() && !IsGrounded())
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
                _body.gravityScale = 7;

            if(Input.GetKey(KeyCode.UpArrow))
                Jump();
        }
        else
            _wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
        }
        else if(OnWall() && !IsGrounded())
        {
            if(_horizontalInput == 0)
            {
                var localScale = transform.localScale;
                _body.velocity = new Vector2(-Mathf.Sign(localScale.x) * 10, 0);
                localScale = new Vector3(-Mathf.Sign(localScale.x) * (float)1.5,
                    localScale.y,
                    localScale.z);
                transform.localScale = localScale;
            }
            else
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            _wallJumpCooldown = 0;
        }
    }

    private bool IsGrounded()
    {
        var bounds = _boxCollider.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            bounds.center,
            bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        var bounds = _boxCollider.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            bounds.center,
            bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            wallLayer);
        return raycastHit.collider != null;
    }
}