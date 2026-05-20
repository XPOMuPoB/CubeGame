using UnityEngine;

public class ZdorovEnemy : MonoBehaviour
{
    [Header("Модели сердец (в порядке от 1-го здоровья)")]
    [SerializeField] private GameObject[] heartObjects;

    private Enemy enemy;

    private void Update()
    {
        enemy = FindAnyObjectByType<Enemy>();

        if (enemy == null) return;

        for (int i = 0; i < heartObjects.Length; i++)
        {
            heartObjects[i].SetActive(i < enemy.CurrentHealth);
        }
    }
}