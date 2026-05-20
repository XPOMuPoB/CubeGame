using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Text EndGameText;
    [SerializeField] Text CastText;

    public void Setup(int CountCast, string LossText)
    {
        gameObject.SetActive(true);
        EndGameText.text = $"{LossText}";
        CastText.text = "Всего бросков: " + CountCast;
    }
    public void Restart(int NumGame)
    {
        SceneManager.LoadScene(NumGame);
    }

    public void Menu()
    {
        SceneManager.LoadScene("_Menu");
    }
}
