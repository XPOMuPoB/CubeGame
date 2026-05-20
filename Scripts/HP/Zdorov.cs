using UnityEngine;

public class Zdorov : MonoBehaviour
{
    [Header("Модели сердец (в порядке от 1-го здоровья)")]
    [SerializeField] private GameObject[] heartObjects;

    private Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogError("Player не найден на сцене!");
        }
    }

    private void Update()
    {
        if (player == null) return;

        for (int i = 0; i < heartObjects.Length; i++)
        {
            heartObjects[i].SetActive(i < player.CurrentHealth);
        }
    }
}