using UnityEngine;
using UnityEngine.InputSystem;

public class DinoBehaviour : MonoBehaviour
{
    public bool IS_ALIVE = true;

    private bool IS_GROUNDED;

    [SerializeField] private Rigidbody2D dinoRigidbody; // I

    [SerializeField] private float _jumpHeight = 10f;

    [SerializeField] private float _rayLegth = 1f;

    [SerializeField] private LayerMask _groundLayer;
    private void Start()
    {
        Time.timeScale = 1.0f;
        IS_ALIVE = true;

        dinoRigidbody = GetComponent<Rigidbody2D>();

        if (dinoRigidbody == null) { Debug.LogError("Dino Rigidbody Absent"); }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IS_GROUNDED)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        RaycastHit2D playerRaycast = Physics2D.Raycast(transform.position, Vector2.down, _rayLegth, _groundLayer);
        IS_GROUNDED = playerRaycast.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("POTRACHENO");
            Time.timeScale = 0f;
            IS_ALIVE = false;
        }
    }
    public void Jump()
    {
        if (IS_GROUNDED && IS_ALIVE)
        {
            dinoRigidbody.linearVelocity = new Vector2(dinoRigidbody.linearVelocity.x, _jumpHeight);
        }
    }
}
