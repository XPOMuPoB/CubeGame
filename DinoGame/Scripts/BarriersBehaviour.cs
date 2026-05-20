using UnityEngine;
using UnityEngine.UIElements;

public class BarriersBehaviour : MonoBehaviour
{

[SerializeField] private float _barrierMovementSpeed = 5f;

[SerializeField] private float _screenExit = -10f;

[SerializeField] private Rigidbody2D barrierRigidbody;

private void Start()
{
    barrierRigidbody = GetComponent<Rigidbody2D>();
}

private void Update()
{
    // Move barrier to the left
    transform.Translate(Vector2.left * _barrierMovementSpeed * Time.deltaTime);

    // Destroy when barrier goes off-screen
    if (transform.position.x < _screenExit)
    {
        Destroy(gameObject);
    }
}
}
