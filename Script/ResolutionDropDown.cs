using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> dropdownResolutions = new List<Resolution>();

    [System.Obsolete]
    private void Start()
    {
        // Popola il dropdown con le risoluzioni supportate
        PopulateDropdown();
    }

    [System.Obsolete]
    void PopulateDropdown()
    {
        // Cancella le opzioni esistenti nel dropdown
        resolutionDropdown.ClearOptions();

        // Ottieni le risoluzioni supportate dal sistema
        Resolution[] resolutions = Screen.resolutions;

        // Crea una lista di stringhe per le opzioni del dropdown
        TMP_Dropdown.OptionDataList options = new TMP_Dropdown.OptionDataList();

        // Aggiungi ogni risoluzione supportata come opzione nel dropdown
        foreach (Resolution resolution in resolutions)
        {
            if (resolution.refreshRate != 60)
            {
                continue;
            }
            string optionText = resolution.width + "x" + resolution.height;

            // Evita di aggiungere risoluzioni duplicate
            if (options.options.Exists(option => option.text == optionText))
            {
                continue;
            }

            options.options.Add(new TMP_Dropdown.OptionData(optionText));
            dropdownResolutions.Add(resolution);
        }

        // Aggiungi le opzioni al dropdown
        resolutionDropdown.options = options.options;
        resolutionDropdown.value = options.options.Count - 1;
    }

    // Metodo chiamato quando viene selezionata una nuova risoluzione
    public void OnResolutionChanged(int index)
    {
        // Ottieni la risoluzione selezionata dal dropdown
        Resolution selectedResolution = dropdownResolutions[index];

        // Imposta la nuova risoluzione del gioco
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
