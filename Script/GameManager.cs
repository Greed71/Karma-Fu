using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Score scoreEasy, scoreMedium, scoreHard;
    private static GameManager _instance;
    public UnitHealth playerHealth = new(100, 100);
    public UnitHealth enemyHealth = new(100, 100);
    public bool gameIsPaused{get; set;}
    public int w = 0;
    public string playerName;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    Debug.Log("Nessun GameManager nella scena");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
