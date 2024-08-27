using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour, IMenu
{
    public GameObject menuUI, playUI, optionUI, difficultyUI,p1UI,errorUI, scoreUI;
    public Button playButton, optionButton, quitButton, backButton, onePlayerButton, twoPlayerButton, backButton2, easyButton, mediumButton, hardButton,femaleButton,maleButton,backButton3,errorButton, scoreButton, resetLBButton, creditsButton, resetButton;
    public List<Button> menuOptions;
    private int selectedOption = 0;
    PlayerControls controls;
    bool isConnected = false;
    public LayoutLeaderboard easy,medium,hard;
    

    void Start()
    { 
        easy.SetLeaderBoard();
        medium.SetLeaderBoard();
        hard.SetLeaderBoard();
        menuOptions = new List<Button> { playButton, optionButton, scoreButton, quitButton, creditsButton };
        selectedOption = 0;
    }

    void Update(){
        if(Gamepad.current != null && !isConnected)
        {
            isConnected = true;
        }
        else if(Gamepad.current == null && isConnected)
        {
            isConnected = false;
        }
    }

    void Awake()
    {   
        controls = new PlayerControls();
        controls.Gameplay.Navigate.performed += ctx => Navigate(ctx.ReadValue<Vector2>());
        controls.Gameplay.Select.performed += ctx => Select();
        
    }

    public void Select()
    {
        Button selectedButton = menuOptions[selectedOption];
        selectedButton.onClick.Invoke();
    }


    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void Navigate(Vector2 direction)
    {
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
    }

    public void Score()
    {
        menuUI.SetActive(false);
        scoreUI.SetActive(true);
        menuOptions = new List<Button> { resetLBButton ,backButton };
        selectedOption = 0;
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        playUI.SetActive(true);
        menuOptions = new List<Button> { onePlayerButton, twoPlayerButton, backButton2};
        selectedOption = 0;
    }

    public void Option(){
        menuUI.SetActive(false);
        optionUI.SetActive(true);
        menuOptions = new List<Button> { backButton, resetButton };
        selectedOption = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back(){
        menuUI.SetActive(true);
        optionUI.SetActive(false);
        playUI.SetActive(false);
        scoreUI.SetActive(false);
        menuOptions = new List<Button> { playButton, optionButton, quitButton };
        selectedOption = 0;
    }

    public void Easy(){
        PlayerPrefs.SetInt("Difficulty", 0);
        SceneManager.LoadScene("Stage AI");
    }

    public void Medium(){
        PlayerPrefs.SetInt("Difficulty", 1);
        SceneManager.LoadScene("Stage AI");
    }

    public void Hard(){
        PlayerPrefs.SetInt("Difficulty", 2);
        SceneManager.LoadScene("Stage AI");
    }

    public void TwoPlayer(){
        if(!isConnected)
        {
            Error();
        }
        else
        {
            PlayerPrefs.SetInt("GameMode", 1);
            PlayerPrefs.SetInt("PC", 0);
            PlayerPrefs.SetInt("CC", 0);
            SceneManager.LoadScene("Stage");
        }
    }

    public void Male(){
        PlayerPrefs.SetInt("Character", 0);
        p1UI.SetActive(false);
        difficultyUI.SetActive(true);
        menuOptions = new List<Button> { easyButton, mediumButton, hardButton, backButton };
        selectedOption = 0;
    }

    public void Female(){
        PlayerPrefs.SetInt("Character", 1);
        p1UI.SetActive(false);
        difficultyUI.SetActive(true);
        menuOptions = new List<Button> { easyButton, mediumButton, hardButton, backButton };
        selectedOption = 0;
    }

    public void OnePlayer(){
        PlayerPrefs.SetInt("GameMode", 0);
        playUI.SetActive(false);
        p1UI.SetActive(true);
        menuOptions = new List<Button> { femaleButton, maleButton, backButton3};
        selectedOption = 0;
    }

    public void Back2(){
        difficultyUI.SetActive(false);
        playUI.SetActive(true);
        menuOptions = new List<Button> { onePlayerButton, twoPlayerButton, backButton2};
        selectedOption = 0;        
    }

    public void Back3(){
        p1UI.SetActive(false);
        playUI.SetActive(true);
        menuOptions = new List<Button> { onePlayerButton, twoPlayerButton, backButton2};
        selectedOption = 0;
    }

    public void Error(){
        errorUI.SetActive(true);
        menuOptions = new List<Button> { errorButton};
        selectedOption = 0;
    }

    public void ErrorBack(){
        errorUI.SetActive(false);
        menuOptions = new List<Button> { onePlayerButton, twoPlayerButton, backButton2};
        selectedOption = 0;
    }

    public void ResetLB(){
        easy.score.DeleteData();
        medium.score.DeleteData();
        hard.score.DeleteData();
        easy.SetLeaderBoard();
        medium.SetLeaderBoard();
        hard.SetLeaderBoard();
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Reset(){
        PlayerPrefs.DeleteAll();
    }
}
