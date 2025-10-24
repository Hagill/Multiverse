using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameScene scene = GameScene.MinigameZoneScene;

    [SerializeField] private GameObject uiPanel;

    private void Awake()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.SetCurrentPortal(this);

            if (uiPanel != null)
                uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.UnSetCurrentPortal();

            if (uiPanel != null)
                uiPanel.SetActive(false);
        }
    }
}
