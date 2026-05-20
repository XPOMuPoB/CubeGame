using System.Collections;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    Rigidbody body;

    [SerializeField] private float maxRandomForceValue, startRollingForce;
    [SerializeField] private float resetDuration = 1f;

    private float forceX, forceY, forceZ;
    public int diceFaceNum;

    private Vector3 startPosition;
    private Quaternion startRotation; // добавили для поворота

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation; // запоминаем поворот
    }

    public void RollDice()
    {
        diceFaceNum = 0;
        forceX = Random.Range(1f, startRollingForce);
        forceY = Random.Range(1f, startRollingForce);
        forceZ = Random.Range(1f, startRollingForce);

        body.AddForce(Vector3.up * maxRandomForceValue);
        body.AddTorque(forceX, forceY, forceZ);
    }

    public void TeleportToStart()
    {
        body.isKinematic = true;
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        body.isKinematic = false;
    }

    public IEnumerator SmoothReset()
    {
        // Останавливаем физику и замораживаем кубик
        body.isKinematic = true;
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        Vector3 currentPos = transform.position;

        float elapsed = 0f;
        while (elapsed < resetDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / resetDuration;
            transform.position = Vector3.Lerp(currentPos, startPosition, t);
            yield return null;
        }

        transform.position = startPosition;
        body.isKinematic = false;
    }
}