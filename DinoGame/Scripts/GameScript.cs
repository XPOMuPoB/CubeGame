using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] private DinoBehaviour dinoScript;
    [SerializeField] private Text ScoreDisplay;
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject ScreenGameOver;
    [SerializeField] private Text ScoreGameOver;
    private DinoBehaviour _dino;

    private bool isGameOver = false;

    private int playerScore = 0;
    private float scoreTimer = 0f;

    private void Start()
    {
        Camera camera = Camera.main;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.85f, 0.85f, 0.85f);
    }
    private void Update()
    {
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            playerScore += 1;
            scoreTimer = 0f;
            UpdateScoreText();
        }

        if(!isGameOver && dinoScript != null && dinoScript.IS_ALIVE == false)
        {
            GameOver();
        }
    }
    public void UpdateScoreText()
    {
        ScoreDisplay.text = "Score: " + playerScore.ToString();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        ScoreGameOver.text = "Score: " + playerScore.ToString();
        ScoreText.SetActive(false);
        ScreenGameOver.SetActive(true);
    }
}
