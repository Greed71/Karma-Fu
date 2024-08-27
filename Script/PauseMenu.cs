using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, settingsMenuUI, escMenuUI, errorUI;
    public List<Button> menuOptions;
    private int selectedOption = 0;
    public Color normalColor, highlightedColor;
    public Button resumeButton, settingsButton, backButton, quitButton, menuButton, quitButton2, backButton2, errorButton;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(Gamepad.current == null)
        {
            Error();
        }
    }

    public void Navigate(Vector2 direction)
    {
        // Rimuovi l'evidenziazione dall'opzione del menu correntemente selezionata
        ColorBlock colors = menuOptions[selectedOption].GetComponent<Button>().colors;
        colors.normalColor = normalColor;
        menuOptions[selectedOption].GetComponent<Button>().colors = colors;

        if (direction.y > 0)
        {
            // Naviga verso l'alto
            selectedOption--;
            if (selectedOption < 0)
            {
                selectedOption = menuOptions.Count - 1;
            }
        }
        else if (direction.y < 0)
        {
            // Naviga verso il basso
            selectedOption++;
            if (selectedOption >= menuOptions.Count)
            {
                selectedOption = 0;
            }
        }
        // Evidenzia l'opzione del menu ora selezionata
        colors = menuOptions[selectedOption].GetComponent<Button>().colors;
        colors.normalColor = highlightedColor;
        menuOptions[selectedOption].GetComponent<Button>().colors = colors;
    }

    public void Select()
    {
        Button selectedButton = menuOptions[selectedOption];
        selectedButton.onClick.Invoke();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuOptions = new List<Button> { resumeButton, settingsButton, quitButton };
        selectedOption = 0;
        Time.timeScale = 0f;
        GameManager.Instance.gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.gameIsPaused = false;
    }

    public void Quit(){
        Application.Quit();
    }

    public void Menu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Esc(){
        pauseMenuUI.SetActive(false);
        escMenuUI.SetActive(true);
        menuOptions = new List<Button> { menuButton, quitButton2 , backButton2};
        selectedOption = 0;
        Time.timeScale = 0f;
        GameManager.Instance.gameIsPaused = true;
    }

    public void Settings(){
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        menuOptions = new List<Button> { backButton };
        selectedOption = 0;
        Time.timeScale = 0f;
        GameManager.Instance.gameIsPaused = true;
    }

    public void Back(){
        settingsMenuUI.SetActive(false);
        escMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        menuOptions = new List<Button> { resumeButton, settingsButton, quitButton };
        selectedOption = 0;
    }
    
    public void Error(){
        errorUI.SetActive(true);
        menuOptions = new List<Button> { errorButton};
        selectedOption = 0;
        Time.timeScale = 0f;
        GameManager.Instance.gameIsPaused = true;
    }

    public void ErrorBack(){
        errorUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.gameIsPaused = false;
    }
}