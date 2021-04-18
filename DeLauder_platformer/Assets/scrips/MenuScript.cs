using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void guid()
    {
        SceneManager.LoadScene("GuidPage");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
