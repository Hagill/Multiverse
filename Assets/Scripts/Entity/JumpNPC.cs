using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpNPC : DummyJump
{
    [Header("NPC Interaction Settings")]
    [SerializeField] private string[] dialogue;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject pressKey;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    private Player player;

    private bool playerInRange = false;
    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    protected override void Start()
    {
        base.Start();

        player = FindObjectOfType<Player>();

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        if (pressKey != null)
        {
            pressKey.SetActive(false);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            if (!isDialogueActive)
            {
                OpenDialogue();
            }
            else
            {
                DisplayNextDialogue();
            }
        }
        else if (isDialogueActive && Input.GetKeyDown(interactionKey))
        {
            DisplayNextDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            pressKey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            pressKey.SetActive(false);
        }
    }

    private void OpenDialogue()
    {

        player.CanMove = false;

        if (dialoguePanel != null && dialogueText != null && dialogue != null && dialogue.Length > 0)
        {
            dialoguePanel.SetActive(true);
            isDialogueActive = true;
            currentDialogueIndex = 0;
            dialogueText.text = dialogue[currentDialogueIndex];
        }
    }

    private void DisplayNextDialogue()
    {
        if (!isDialogueActive)
            return;

        currentDialogueIndex++;
        if (dialogue != null && currentDialogueIndex < dialogue.Length)
        {
            if (dialogueText != null)
            {
                dialogueText.text = dialogue[currentDialogueIndex];
            }
        }
        else
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        player.CanMove = true;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        isDialogueActive = false;
        currentDialogueIndex = 0;
    }
}
