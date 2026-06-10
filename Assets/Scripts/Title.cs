using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void ButtonPlayPressed()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
