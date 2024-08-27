using UnityEngine;

public class CharacterSelect2p : MonoBehaviour
{
    int random1, random2;
    public GameObject maleCharacter, femaleCharacter, maleEnemy, femaleEnemy;
    public TutorialMenu tutorialMenu;
    void Start()
    {
        tutorialMenu = FindObjectOfType<TutorialMenu>();
        random1 = Random.Range(1, 3);
        if (random1 == 1)
        {
            maleCharacter.SetActive(true);
        }
        else
        {
            femaleCharacter.SetActive(true);
        }
        tutorialMenu.tutorialText.text = "Player 1: Use Arrow key to move, Space to jump,\n Left Click to attack and Esc to pause\n\n" + "Player 2: Use Left Stick to move,\n X/A to jump Square/X to attack and Start to pause";
        random2 = Random.Range(1, 3);
        if (random2 == 2)
        {
            femaleEnemy.SetActive(true);
        }
        else
        {
            maleEnemy.SetActive(true);
        }
    }
}