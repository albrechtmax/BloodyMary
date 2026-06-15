using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoOver : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Title");
    }
}
