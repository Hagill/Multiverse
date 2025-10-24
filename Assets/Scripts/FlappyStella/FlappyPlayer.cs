using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float forwardSpeed = 4f;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    FlappyGameManager gameManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = Vector2.zero;
    }

    void Start()
    {
        gameManager = FlappyGameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        

        if (animator == null)
            Debug.LogError("Not Founded Animator");

        if (_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody");
    }

    public void ResetPlayerState()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
        deathCooldown = 0f;

        if (animator != null)
            animator.SetInteger("IsDie", 0);
    }

    public void StartFlapping()
    {
        _rigidbody.gravityScale = 1f;
        _rigidbody.velocity = Vector2.up * flapForce * 0.5f;
        isFlap = false;
    }

    void Update()
    {
        if (gameManager.GetGameState() == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
        else if (gameManager.GetGameState() == GameState.GameOver)
        {
            if (deathCooldown > 0)
            {
                deathCooldown -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.GetGameState() != GameState.Playing)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            return;
        }

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.GetGameState() != GameState.Playing) return;
        if (godMode) return;

        animator.SetInteger("IsDie", 1);
        gameManager.GameOver();
    }
}
