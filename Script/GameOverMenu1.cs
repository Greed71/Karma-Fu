using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu1p : MonoBehaviour
{
    public TMP_InputField inputField;
    GameObject player, enemy;
    public GameObject gameOverMenuUI, recordMenuUI;
    public TextMeshProUGUI gameOverText, scoreText;
    bool isHappened = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        scoreText.text = GameManager.Instance.w.ToString();
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
        if(gameOverText.text == "You Win!")
        {
            GameManager.Instance.w++;
        }
        gameOverMenuUI.SetActive(false);
        if(GameManager.Instance.w > 0)
            recordMenuUI.SetActive(true);
        else
            SceneManager.LoadScene("Menu");
    }

    public void Menu()
    {
        if(inputField.text != "")
        {
            GameManager.Instance.playerName = inputField.text;
        }else
        {
            GameManager.Instance.playerName = "Player";
        }
        switch(PlayerPrefs.GetInt("Difficulty"))
        {
            case 0:
                GameManager.Instance.scoreEasy.SetScore(GameManager.Instance.playerName, GameManager.Instance.w);
                break;
            case 1:
                GameManager.Instance.scoreMedium.SetScore(GameManager.Instance.playerName, GameManager.Instance.w);
                break;
            case 2:
                GameManager.Instance.scoreHard.SetScore(GameManager.Instance.playerName, GameManager.Instance.w);
                break;
        }
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Game Over!";
            Time.timeScale = 0f;
            GameManager.Instance.gameIsPaused = true;
            GameManager.Instance.w = 0;
    }

    public void Retry()
    {
        GameManager.Instance.playerHealth = new UnitHealth(100, 100);
        GameManager.Instance.enemyHealth = new UnitHealth(100, 100);
        GameManager.Instance.gameIsPaused = false;
        GameManager.Instance.RestartGame();
        if(gameOverText.text == "You Win!")
        {
            GameManager.Instance.w++;
        }
    }

    public void Win()
    {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "You Win!";
            Time.timeScale = 0f;
            GameManager.Instance.gameIsPaused = true;
    }
}
