using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void ButtonBackPressed()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
