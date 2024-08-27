using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Button creditsButton;

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
