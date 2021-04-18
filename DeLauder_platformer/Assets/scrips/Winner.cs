using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene("MainMenu");
    }
}
