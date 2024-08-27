using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject maleCharacter, femaleCharacter, maleEnemy, femaleEnemy;
    int random;
    TutorialMenu tutorialMenu;

    public void Start(){
        tutorialMenu = FindObjectOfType<TutorialMenu>();
        if(PlayerPrefs.GetInt("Character") == 0){
            maleCharacter.SetActive(true);
            tutorialMenu.tutorialText.text = "Use arrow keys to move and the left click to attack,\n Space to jump and Esc to pause";
            random = Random.Range(1, 3);
            if(random == 1)
                maleEnemy.SetActive(true);
            else
                femaleEnemy.SetActive(true);
        }else if(PlayerPrefs.GetInt("Character") == 1){
            femaleCharacter.SetActive(true);
            tutorialMenu.tutorialText.text = "Use the arrow keys to move and the left click to attack,\n Space to jump and Esc to pause";
            random = Random.Range(1, 3);
            if(random == 1)
                maleEnemy.SetActive(true);
            else
                femaleEnemy.SetActive(true);
        }
    }
}
