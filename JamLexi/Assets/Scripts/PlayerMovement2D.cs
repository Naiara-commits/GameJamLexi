using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    private enum MovementAxis
    {
        Horizontal,
        Vertical
    }

    [Header("Movimiento")]
    public float speed = 5f;

    [Header("Input System")]
    [SerializeField] private InputActionReference moveActionReference;

    [Header("Sprites por dirección")]
    [SerializeField] private GameObject spriteObject;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private float stunTimer = 0f;
    private MovementAxis lastMovementAxis = MovementAxis.Vertical;
    private ScaleBreathing2D[] breathingAnimations;

    public bool IsStunned => stunTimer > 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        if (spriteObject != null)
        {
            spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                Debug.LogError("El GameObject asignado en Sprite Object no tiene un SpriteRenderer.", spriteObject);
        }
        else
        {
            Debug.LogError("Asigna el GameObject hijo que contiene el SpriteRenderer.", this);
        }

        breathingAnimations = GetComponentsInChildren<ScaleBreathing2D>(true);
        SetFacingSprite(Vector2.down);
    }

    private void OnEnable()
    {
        if (moveActionReference != null)
            moveActionReference.action.Enable();
    }

    private void OnDisable()
    {
        if (moveActionReference != null)
            moveActionReference.action.Disable();
    }

    private void Update()
    {
        if (stunTimer > 0f)
        {
            stunTimer -= Time.deltaTime;
            moveInput = Vector2.zero;
            UpdateBreathingAnimation();
            return;
        }

        if (moveActionReference != null)
        {
            SetMoveInput(moveActionReference.action.ReadValue<Vector2>());
        }
        UpdateBreathingAnimation();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = IsStunned ? Vector2.zero : moveInput * speed;
    }

    /// Deja al jugador sin poder moverse durante 'x' segundos.
    public void Stun(float duration)
    {
        stunTimer = Mathf.Max(stunTimer, duration);
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        UpdateBreathingAnimation();
    }

    public void OnMove(InputValue value)
    {
        if (!IsStunned)
            SetMoveInput(value.Get<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsStunned)
            SetMoveInput(context.ReadValue<Vector2>());
    }

    private void SetMoveInput(Vector2 input)
    {
        bool hasHorizontalInput = input.x != 0f;
        bool hasVerticalInput = input.y != 0f;

        if (hasHorizontalInput && hasVerticalInput)
        {
            if (lastMovementAxis == MovementAxis.Horizontal)
                input.y = 0f;
            else
                input.x = 0f;
        }
        else if (hasHorizontalInput)
        {
            lastMovementAxis = MovementAxis.Horizontal;
        }
        else if (hasVerticalInput)
        {
            lastMovementAxis = MovementAxis.Vertical;
        }

        moveInput = input;

        if (input.x != 0f)
            SetFacingSprite(input.x > 0f ? Vector2.right : Vector2.left);
        else if (input.y != 0f)
            SetFacingSprite(input.y > 0f ? Vector2.up : Vector2.down);
    }

    private void SetFacingSprite(Vector2 direction)
    {
        Sprite selectedSprite = direction == Vector2.up ? upSprite
            : direction == Vector2.down ? downSprite
            : direction == Vector2.left ? leftSprite
            : rightSprite;

        if (spriteRenderer != null && selectedSprite != null)
            spriteRenderer.sprite = selectedSprite;
    }

    private void UpdateBreathingAnimation()
    {
        bool isMoving = !IsStunned && moveInput.sqrMagnitude > 0f;
        foreach (ScaleBreathing2D breathingAnimation in breathingAnimations)
            breathingAnimation.SetMoving(isMoving);
    }
}