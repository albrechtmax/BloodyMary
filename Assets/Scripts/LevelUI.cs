using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public void ButtonPausePressed()
    {
        TogglePauseState();
    }

    public void ButtonBackPressed()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void TogglePauseState()
    {
        // 1.0 and 0.0 can be represented exactly so "==" should be fine here
        if (IsPaused())
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public bool IsPaused()
    {
        return Math.Abs(Time.timeScale) <= 1e-3;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        transform.Find("PauseMenu").gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
