using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonBlink : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Color originalColor;
    public Color blinkColor = Color.red;
    public float blinkDuration = 0.5f;

    private bool isBlinking = false;
    private Coroutine blinkCoroutine; 

    private void Start()
    {
        originalColor = buttonText.color;
    }

    public void OnStateEnter()
    {
        /*if (!isBlinking)
        {
            isBlinking = true;
            blinkCoroutine = StartCoroutine(BlinkText()); // Modifica questa linea
        }
        else
        {
            StopBlinking(); // Aggiungi questa linea
        }*/
        // Se il pulsante sta già lampeggiando, fermiamo la coroutine
        if (isBlinking)
        {
            StopCoroutine(blinkCoroutine);
        }

        // Iniziamo a far lampeggiare il pulsante
        isBlinking = true;
        blinkCoroutine = StartCoroutine(BlinkText());
    }

    public void StopBlinking() // Aggiungi questo metodo
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            buttonText.color = originalColor;
            isBlinking = false;
        }
    }

    public IEnumerator BlinkText()
    {
        /*float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonText.color = Color.Lerp(originalColor, blinkColor, Mathf.PingPong(elapsedTime * 5f, 1f));
            yield return null;
        }
        buttonText.color = originalColor;
        isBlinking = false;*/
        while (isBlinking)
        {
            buttonText.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            if (!isBlinking) // Aggiungi questa linea
                break;
            buttonText.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
        buttonText.color = originalColor;
        isBlinking = false;
    }
}