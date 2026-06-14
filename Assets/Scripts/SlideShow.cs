using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    public Sprite[] slides;
    private int slideIndex = 0;
    public string nextScene;

    void Start()
    {
        GetComponent<Image>().sprite = slides[0];
    }

    public void NextSlide()
    {
        slideIndex = (slideIndex + 1) % slides.Length;

        if (slideIndex == 0 && nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            GetComponent<Image>().sprite = slides[slideIndex];
        }
    }

    public void PreviousSlide()
    {
        slideIndex = (slideIndex + slides.Length - 1) % slides.Length;
        GetComponent<Image>().sprite = slides[slideIndex];
    }
}
