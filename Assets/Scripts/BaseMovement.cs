using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected AnimationHandler animationHandler;
    protected Rigidbody2D _rigidbody;
    
    // �ð��� ǥ���� ���� ȿ���� ���� Transform �ʵ�
    [SerializeField] private Transform characterVisuals;

    // ���� ���۵��� �ɸ��� �ð�
    public float jumpDuration = 0.3f;
    // ���� �ִ� ����
    public float jumpForce = 0.5f;
    // �̵� �ӵ�
    public float movementSpeed = 3.0f;

    protected bool isJump = false;
    // ���� ���۵� ���� ��� �ð�
    private float jumpTimer = 0f;
    // ���� ���� ������ ��ġ�� ���
    private Vector3 initialLocalPosition;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();

        if (characterVisuals == null)
        {
            characterVisuals = this.transform;
        }
        initialLocalPosition = characterVisuals.localPosition;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        HandleAction();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    protected void FixedUpdate()
    {
        // ���� �̵� ���⿡ ���� ĳ���͸� �̵�
        Movement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }

        // ���� ����
        if (isJump)
        {
            // ���� Ÿ�̸Ӹ� fixedDeltaTime���� �ǽð� ������Ʈ
            jumpTimer += Time.fixedDeltaTime;
            float jumping = jumpTimer / jumpDuration;   // 0.0���� 1.0���� ����

            // 1.0 �̻��̸� ������ ����
            if (jumping >= 1f)
            {
                // ���� ����
                jumping = 1f;
                // ���� ��Ȱ��ȭ �� Ÿ�̸� �ʱ�ȭ
                isJump = false;
                jumpTimer = 0f;
                // ���� �� ��ġ�� ����
                characterVisuals.localPosition = initialLocalPosition;
            }
            // ������ �������̸�
            else
            {
                float currentHeight;
                if (jumping < 0.5f) // �ö󰡴� ����
                {
                    currentHeight = Mathf.Lerp(0f, jumpForce, jumping * 2f);
                }
                else // �������� ����
                {
                    currentHeight = Mathf.Lerp(jumpForce, 0f, (jumping - 0.5f) * 2f);
                }

                Vector3 newLocalPos = initialLocalPosition;
                newLocalPos.y += currentHeight;
                characterVisuals.localPosition = newLocalPos;
            }
        }

        if (animationHandler != null)
        {
            animationHandler.SetAnimationParameters(movementDirection, lookDirection);
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * movementSpeed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            jumpTimer = 0f;
        }
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = (other.position - transform.position).normalized * power;
    }
}
