using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;

    [Header("Input System")]
    [SerializeField] private InputActionReference moveActionReference;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float stunTimer = 0f;

    public bool IsStunned => stunTimer > 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
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
            return;
        }

        if (moveActionReference != null)
        {
            moveInput = moveActionReference.action.ReadValue<Vector2>();
        }

        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();
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
    }

        public void OnMove(InputValue value)
    {
        if (!IsStunned)
            moveInput = value.Get<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsStunned)
            moveInput = context.ReadValue<Vector2>();
    }
}