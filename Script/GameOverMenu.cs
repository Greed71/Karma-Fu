using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    GameObject player, enemy;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI gameOverText, scoreTextP, scoreTextC;
    bool isHappened = false;
    int w = 0,p=0;

    void Start()
    {
        w = PlayerPrefs.GetInt("PC");
        p = PlayerPrefs.GetInt("CC");
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        scoreTextP.text = PlayerPrefs.GetInt("PC").ToString();
        scoreTextC.text = PlayerPrefs.GetInt("CC").ToString();
        if (GameManager.Instance.playerHealth.health <= 0 || player.transform.position.y <= 1493 && !isHappened)
        {
            GameOver();
            isHappened = true;
        }
        if (GameManager.Instance.enemyHealth.health <= 0 || enemy.transform.position.y <= 1493 && !isHappened)
        {
            Win();
            isHappened = true;
        }
    }

    public void Exit()
    {
        PlayerPrefs.SetInt("PC", 0);
        PlayerPrefs.SetInt("CC", 0);
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 2 Wins!";
            p++;
            PlayerPrefs.SetInt("CC", p);
            Time.timeScale = 0f;
            GameManager.Instance.gameIsPaused = true;
    }

    public void Retry()
    {
        GameManager.Instance.playerHealth = new UnitHealth(100, 100);
        GameManager.Instance.enemyHealth = new UnitHealth(100, 100);
        GameManager.Instance.gameIsPaused = false;
        GameManager.Instance.RestartGame();
    }

    public void Win()
    {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 1 Wins!";
            w++;
            PlayerPrefs.SetInt("PC", w);
            Time.timeScale = 0f;
            GameManager.Instance.gameIsPaused = true;
    }
}
