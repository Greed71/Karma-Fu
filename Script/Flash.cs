using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    public float duration;
    private SpriteRenderer playerSpriteRenderer, enemySpriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = playerSpriteRenderer.material;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = enemySpriteRenderer.material;
        flashMaterial = new Material(flashMaterial);
    }

    public void Flashh(string character)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine(character));
    }

    private IEnumerator FlashRoutine(string character)
    {
        if (character == "Player")
        {
            playerSpriteRenderer.material = flashMaterial;
        }
        else if (character == "Enemy")
        {
            enemySpriteRenderer.material = flashMaterial;
        }

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        if (character == "Player")
        {
            playerSpriteRenderer.material = originalMaterial;
        }
        else if (character == "Enemy")
        {
            enemySpriteRenderer.material = originalMaterial;
        }

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
