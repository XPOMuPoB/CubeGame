using UnityEngine;

public class FaceDetect : MonoBehaviour
{
    DiceRoll dice;
    public static int NumCast = 0;
    private void Awake()
    {
        dice = FindAnyObjectByType<DiceRoll>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice != null && dice.diceFaceNum == 0)
        {
            // Упрощённая проверка на остановку
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            if (rb.linearVelocity.magnitude < 0.01f && rb.angularVelocity.magnitude < 0.01f)
            {
                dice.diceFaceNum = int.Parse(other.name);
                NumCast = int.Parse(other.name);
            }
        }
    }
}
