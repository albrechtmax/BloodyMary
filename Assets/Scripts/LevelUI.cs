using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public string nextLevel;
    public string explanationLevel;

    public void Start()
    {
        // to reset after retry, and after scene is actually loaded
        if (FindAnyObjectByType<Popup>() == null)
            Time.timeScale = 1.0f;
    }

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

    public void ButtonMutePressed()
    {
        var buttonMute = transform.Find("PauseMenu").Find("ButtonMute");
        var textComponent = buttonMute.GetComponentInChildren<Text>();
        if (AudioListener.volume == 0.0f)
        {
            textComponent.text = "Ton aus";
            AudioListener.volume = 1.0f;
        }
        else
        {
            textComponent.text = "Ton an";
            AudioListener.volume = 0.0f;
        }
    }

    public void OnHealthEmpty()
    {
        Time.timeScale = 0.0f;
        transform.Find("GameOverScreen").gameObject.SetActive(true);
        transform.Find("TopBar").Find("ButtonPause").gameObject.SetActive(false);
    }

    public void OnRetryPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTimeout()
    {
        Shooter shooter = FindAnyObjectByType<Shooter>();
        Time.timeScale = 0.0f;

        var gameWon = transform.Find("GameWonScreen");
        gameWon.Find("TextGameOverExplain").GetComponent<Text>().text = $"Punkte: {shooter.score}";
        gameWon.gameObject.SetActive(true);

        transform.Find("SoundWin").GetComponent<AudioSource>().Play();
    }

    public void OnNextPressed()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
