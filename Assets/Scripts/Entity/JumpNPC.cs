using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpNPC : DummyJump
{
    [Header("NPC Interaction Settings")]
    [SerializeField] private string[] dialogue; // 대사로그
    [SerializeField] private GameObject dialoguePanel;  // 대사창 패널
    [SerializeField] private GameObject pressKey;       // 상호작용을 알리는 UI
    [SerializeField] private TMP_Text dialogueText;     // 대사 텍스트
    [SerializeField] private KeyCode interactionKey = KeyCode.F;    // 상호작용키

    private Player player;

    private bool playerInRange = false; // 플레이어와 상호작용 가능함을 알림
    private int currentDialogueIndex = 0;   // 대사 배열 인덱스
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

        // 플레이어가 상호작용 가능 거리에서 상호작용 했을 때
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            // 대사가 활성화 되지 않았을 때
            if (!isDialogueActive)
            {
                OpenDialogue();
            }
            // 대사가 활성화 됐을 때
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
        // 플레이어의 방향키 이동을 비활성화
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
        // 다음 대사로 넘김
        if (dialogue != null && currentDialogueIndex < dialogue.Length)
        {
            if (dialogueText != null)
            {
                dialogueText.text = dialogue[currentDialogueIndex];
            }
        }
        // 다음 대사가 없으면 대화창을 끔
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
