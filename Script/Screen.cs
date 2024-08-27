using UnityEngine;
using TMPro;

public class ScreenModeDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown_resolution, dropdown_quality, dropdown_screenMode;

    private void Start()
    {
        dropdown_quality.onValueChanged.AddListener(QualityChange);
        dropdown_screenMode.onValueChanged.AddListener(ScreenModeChange);
    }

    void QualityChange(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    void ScreenModeChange(int screenModeIndex)
    {
        if (screenModeIndex == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (screenModeIndex == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (screenModeIndex == 2)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
}
