using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _boxCollider;
    private float _horizontalInput;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);
        _body.gravityScale = 7;
        
        if(Input.GetKey(KeyCode.UpArrow))
            Jump();

        if(_horizontalInput > 0.01f)
            transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
        else if(_horizontalInput < -0.01f)
            transform.localScale = new Vector3((float)-0.5, (float)0.5, 1);
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
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
    
    // POWERUPS
    private void OnTriggerEnter2D(Collider2D collision)
     {
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
             if (speed != 16f)
                 speed = 16f;
             else
                 speed = 10f;
         }
     }
}