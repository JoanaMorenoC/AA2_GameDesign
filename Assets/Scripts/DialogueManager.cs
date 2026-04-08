using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    enum DialogueType { Reproche, PlayerBlock, PlayerAttack };

    [System.Serializable]
    public class Dialogue
    {
        public AudioClip audio;
        public string text;
    }

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Dialogues")]
    [SerializeField] List<Dialogue> reproches;
    [SerializeField] List<Dialogue> playerBlocks;
    [SerializeField] List<Dialogue> playerAttacks;

    private int currentReproche = -1;

    bool talking = false;

    DialogueType? currentDialogueType = null;
    Coroutine currentCoroutine;

    void OnEnable()
    {
        DragonAttackController.OnDragonReproche += HandleDragonReproach;
        ShieldBlocking.OnPlayerBlockReproche += HandlePlayerBlock;
        PlayerAttack.OnPlayerAttack += HandlePlayerAttack;
    }

    void OnDisable()
    {
        DragonAttackController.OnDragonReproche -= HandleDragonReproach;
        ShieldBlocking.OnPlayerBlockReproche -= HandlePlayerBlock;
        PlayerAttack.OnPlayerAttack -= HandlePlayerAttack;
    }

    // =========================
    // EVENTOS
    // =========================

    void HandleDragonReproach()
    {
        Debug.Log("Dialogos: Dragón lanza reproche");

        // Interrumpe solo ataques del jugador
        if (talking && currentDialogueType == DialogueType.PlayerAttack)
        {
            StopCurrentDialogue();
        }
        else if (talking)
        {
            return;
        }

        currentReproche = Random.Range(0, reproches.Count);
        PlayDialogue(DialogueType.Reproche, currentReproche);
    }

    void HandlePlayerBlock()
    {
        Debug.Log("Dialogos: Jugador bloquea reproche");

        // No hay reproche activo → no hacer nada
        if (currentReproche == -1) return;

        // Interrumpe reproche y ataques del jugador
        if (talking &&
            (currentDialogueType == DialogueType.Reproche ||
             currentDialogueType == DialogueType.PlayerAttack))
        {
            StopCurrentDialogue();
        }
        else if (talking)
        {
            return;
        }

        PlayDialogue(DialogueType.PlayerBlock, currentReproche);

        currentReproche = -1;
    }

    void HandlePlayerAttack()
    {
        Debug.Log("Dialogos: Jugador ataca (sarcasmo)");

        // No interrumpe nada
        if (talking) return;

        int index = Random.Range(0, playerAttacks.Count);
        PlayDialogue(DialogueType.PlayerAttack, index);
    }

    // =========================
    // CONTROL DE AUDIO
    // =========================

    void PlayDialogue(DialogueType type, int index)
    {
        AudioClip audio = null;

        switch (type)
        {
            case DialogueType.Reproche:
                audio = reproches[index].audio;
                break;

            case DialogueType.PlayerBlock:
                audio = playerBlocks[index].audio;
                break;

            case DialogueType.PlayerAttack:
                audio = playerAttacks[index].audio;
                break;
        }

        if (audio == null) return;

        currentDialogueType = type;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(PlayDialogueCoroutine(audio));
    }

    IEnumerator PlayDialogueCoroutine(AudioClip clip)
    {
        talking = true;

        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        talking = false;
        currentDialogueType = null;
    }

    void StopCurrentDialogue()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        audioSource.Stop();

        talking = false;
        currentDialogueType = null;
    }
}