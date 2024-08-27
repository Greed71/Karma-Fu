using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    public GameObject tutorialMenuUI;
    public Toggle dontShowAgainToggle;
    public TextMeshProUGUI tutorialText;
    Countdown countdown;

    void Start()    
    {     
        countdown = FindObjectOfType<Countdown>();  
        if (PlayerPrefs.GetInt("TutorialShown") == 1)
        {
            Time.timeScale = 1f;
            tutorialMenuUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            tutorialMenuUI.SetActive(true);
        }
    }

    public void Continue()
    {
        tutorialMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (dontShowAgainToggle.isOn)
        {
            PlayerPrefs.SetInt("TutorialShown", 1);
            PlayerPrefs.Save();
        }
        StartCoroutine(countdown.StartCountdown());
    }
}
