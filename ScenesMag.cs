using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMag : MonoBehaviour
{
    public void Scenes(int numScenes)
    {
        SceneManager.LoadScene(numScenes);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
