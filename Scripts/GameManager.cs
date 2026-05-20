using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DiceRoll dice;
    public Player player;
    public Enemy currentEnemy;
    public GameOverScreen gameOverScreen;
    public Transform enemySpawnPoint;

    public float timeoutDuration = 10f;

    public GameObject EnemyBtn;
    private Animator enemyAnimator;

    public List<GameObject> enemyPrefabs;
    private int currentEnemyIndex = 0;

    public List<int> ScoreList = new();
    public int CountCast = 0;
    public int CountScore = 0;

    public bool isRolling = false;
    public void Roll()
    {
        if (isRolling) return;

        StartCoroutine(WaitForRoll());
    }

    private void Start()
    {
        SpawnNextEnemy();

        Time.timeScale = 1;
        enemyAnimator = EnemyBtn.GetComponent<Animator>();
    }

    private void SpawnNextEnemy()
    {
        if (currentEnemyIndex >= enemyPrefabs.Count)
        {
            StatisticClass();
            if (gameOverScreen != null)
            {
                gameOverScreen.Setup(CountCast, "Победа");
            }
            return;
        }

        if (currentEnemy != null)
            Destroy(currentEnemy.gameObject);

        GameObject newEnemyObj = Instantiate(enemyPrefabs[currentEnemyIndex], enemySpawnPoint.position, enemySpawnPoint.rotation);
        currentEnemy = newEnemyObj.GetComponent<Enemy>();

        currentEnemy.OnDeath += OnEnemyDied;

        currentEnemyIndex++;
    }

    private void OnEnemyDied(Enemy deadEnemy)
    {
        deadEnemy.OnDeath -= OnEnemyDied;

        if(player.CurrentHealth < 3)
        {
            player.TakeDamage(currentEnemy.transform, -1);
        }

        StartCoroutine(SpawnNextWithDelay(1f));
    }

    private System.Collections.IEnumerator SpawnNextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnNextEnemy();
    }

    private System.Collections.IEnumerator WaitForRoll()
    {
        isRolling = true;

        // ---- Бросок игрока ----
        dice.diceFaceNum = 0;
        dice.RollDice();

        float elapsed = 0f;
        while (dice.diceFaceNum == 0 && elapsed < timeoutDuration)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if (dice.diceFaceNum == 0)
        {
            Debug.LogWarning("Тайм-аут броска игрока, принудительная телепортация");
            dice.TeleportToStart();
        }

        int playerResult = dice.diceFaceNum;
        Debug.Log("Игрок выбросил: " + playerResult);
        if (playerResult != 0) ScoreList.Add(playerResult);

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(dice.SmoothReset());
        yield return new WaitForSeconds(0.5f);

        // ---- Бросок врага ----
        dice.diceFaceNum = 0;
        dice.RollDice();

        if (enemyAnimator != null) enemyAnimator.SetTrigger("Press");

        elapsed = 0f;
        while (dice.diceFaceNum == 0 && elapsed < timeoutDuration)
        {
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        if (dice.diceFaceNum == 0)
        {
            Debug.LogWarning("Тайм-аут броска врага, принудительная телепортация");
            dice.TeleportToStart();
        }

        int enemyResult = dice.diceFaceNum;
        Debug.Log("Противник выбросил: " + enemyResult);

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(dice.SmoothReset());

        CompareResults(playerResult, enemyResult);
        isRolling = false;
    }

    private void CompareResults(int playerResult, int enemyResult)
    {
        if (playerResult < enemyResult)
            player.TakeDamage(currentEnemy.transform, 1);
        else if (enemyResult < playerResult)
            currentEnemy.TakeDamage(player.transform, 1);
    }

    public void StatisticClass()
    {
        int[] ScoreArray = ScoreList.ToArray();
        CountCast = ScoreArray.Length;
        CountScore = ScoreArray.Sum();
    }

    public void ShowGameOver()
    {
        StatisticClass();
        if (gameOverScreen != null)
        {
            gameOverScreen.Setup(CountCast, "Поражение");
        }
    }

}