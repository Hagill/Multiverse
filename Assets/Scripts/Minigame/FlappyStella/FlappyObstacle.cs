using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyObstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    FlappyGameManager gameManager;

    private bool _hasBeenScored = false;

    private void Start()
    {
        gameManager = FlappyGameManager.Instance;
    }

    private void OnEnable()
    {
        _hasBeenScored = false;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        _hasBeenScored = false;

        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_hasBeenScored) return;

        FlappyPlayer player = collision.GetComponent<FlappyPlayer>();

        if (player != null)
        {
            gameManager.AddScore(1);
            _hasBeenScored = true;
        }
    }
}
