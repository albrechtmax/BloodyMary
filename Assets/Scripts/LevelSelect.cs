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
        SceneManager.LoadScene("Intro0");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Intro1");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Intro2");
    }
}
