using UnityEngine;
using TMPro;

public class LayoutLeaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    public Score score;

    public void SetLeaderBoard()
    {
        int length = score.scores.Count < scoreTexts.Length ? score.scores.Count : scoreTexts.Length;
        if(length == 0)
        {
            for(int i = 0; i < scoreTexts.Length; i++)
                scoreTexts[i].text = "";
        }
        for(int i = 0; i < length; i++)
        {
            scoreTexts[i].text = score.scores[i].name + ": " + score.scores[i].score;
        }
    }
}
