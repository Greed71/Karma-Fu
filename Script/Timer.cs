using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    public GameOverMenu gameOverMenu;
    float timeRemaining = 60;

    void Start()
    {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timeRemaining <= 0 && GameManager.Instance.playerHealth.health > GameManager.Instance.enemyHealth.health)
        {
            gameOverMenu.Win();
        }
        else if (timeRemaining <= 0 && GameManager.Instance.playerHealth.health < GameManager.Instance.enemyHealth.health)
        {
            gameOverMenu.GameOver();
        }
        else if(timeRemaining <= 0 && GameManager.Instance.playerHealth.health == GameManager.Instance.enemyHealth.health)
        {
            gameOverMenu.GameOver();
        }
    }
}
