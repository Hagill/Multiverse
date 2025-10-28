using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpNPC : DummyJump
{
    [Header("NPC Interaction Settings")]
    [SerializeField] private string[] dialogue; // ���α�
    [SerializeField] private GameObject dialoguePanel;  // ���â �г�
    [SerializeField] private GameObject pressKey;       // ��ȣ�ۿ��� �˸��� UI
    [SerializeField] private TMP_Text dialogueText;     // ��� �ؽ�Ʈ
    [SerializeField] private KeyCode interactionKey = KeyCode.F;    // ��ȣ�ۿ�Ű

    private Player player;

    private bool playerInRange = false; // �÷��̾�� ��ȣ�ۿ� �������� �˸�
    private int currentDialogueIndex = 0;   // ��� �迭 �ε���
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

        // �÷��̾ ��ȣ�ۿ� ���� �Ÿ����� ��ȣ�ۿ� ���� ��
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            // ��簡 Ȱ��ȭ ���� �ʾ��� ��
            if (!isDialogueActive)
            {
                OpenDialogue();
            }
            // ��簡 Ȱ��ȭ ���� ��
            else
            {
                DisplayNextDialogue();
            }
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
        // �÷��̾��� ����Ű �̵��� ��Ȱ��ȭ
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
        // ���� ���� �ѱ�
        if (dialogue != null && currentDialogueIndex < dialogue.Length)
        {
            if (dialogueText != null)
            {
                dialogueText.text = dialogue[currentDialogueIndex];
            }
        }
        // ���� ��簡 ������ ��ȭâ�� ��
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
