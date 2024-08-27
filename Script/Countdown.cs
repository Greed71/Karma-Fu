using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    public GameOverMenu gameOverMenu;
    float timeRemaining = 3;

    void Start()
    {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        if(PlayerPrefs.GetInt("TutorialShown") == 1)
            StartCoroutine(StartCountdown());
    }

    void Update(){
        if(!GameManager.Instance.gameIsPaused)
        {
            if(timeRemaining > 0){
                Time.timeScale = 0;
            }
            else{
                Time.timeScale = 1;
            }        
        }
    }

    public IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            timerText.text = timeRemaining.ToString("F0");
            yield return new WaitForSecondsRealtime(1);
            if(!GameManager.Instance.gameIsPaused)
                timeRemaining--;
        }
        timerText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        timerText.gameObject.SetActive(false);
    }
}
