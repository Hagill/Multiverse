using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected AnimationHandler animationHandler;
    protected Rigidbody2D _rigidbody;
    
    // 시각적 표현의 점프 효과를 위한 Transform 필드
    [SerializeField] private Transform characterVisuals;

    // 점프 동작동안 걸리는 시간
    public float jumpDuration = 0.3f;
    // 점프 최대 높이
    public float jumpForce = 0.5f;
    // 이동 속도
    public float movementSpeed = 3.0f;

    protected bool isJump = false;
    // 점프 시작된 이후 경과 시간
    private float jumpTimer = 0f;
    // 점프 이후 착지할 위치를 기록
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
        // 현재 이동 방향에 따라 캐릭터를 이동
        Movement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }

        // 점프 로직
        if (isJump)
        {
            // 점프 타이머를 fixedDeltaTime으로 실시간 업데이트
            jumpTimer += Time.fixedDeltaTime;
            float jumping = jumpTimer / jumpDuration;   // 0.0부터 1.0까지 진행

            // 1.0 이상이면 점프가 종료
            if (jumping >= 1f)
            {
                // 점프 종료
                jumping = 1f;
                // 점프 비활성화 및 타이머 초기화
                isJump = false;
                jumpTimer = 0f;
                // 점프 전 위치로 복귀
                characterVisuals.localPosition = initialLocalPosition;
            }
            // 점프가 진행중이면
            else
            {
                float currentHeight;
                if (jumping < 0.5f) // 올라가는 구간
                {
                    currentHeight = Mathf.Lerp(0f, jumpForce, jumping * 2f);
                }
                else // 내려오는 구간
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
