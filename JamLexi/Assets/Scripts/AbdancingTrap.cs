using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class AbdancingTrap : MonoBehaviour
{
    [SerializeField] float velocity = 5.0f;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Vector3 finalPosition;
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool isDead = false;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        PlayerMovement2D player = other.GetComponentInParent<PlayerMovement2D>();
        if (player == null) return;

        player.Stun(1.5f);
        Death();
    }

    public void ConfigurePath(Vector3 startPosition, Vector3 endPosition)
    {
        initialPosition = startPosition;
        finalPosition = endPosition;
        transform.position = initialPosition;
        isDead = false;
        spriteRenderer.enabled = true;
    }

    private void Move()
    {
        if (isDead) return;
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, velocity * Time.deltaTime);
        if (transform.position == finalPosition)
        {
            Death();
        }

    }

    private void Death()
    {
        isDead = true;
        StartCoroutine(Revive());
    }

    private IEnumerator Revive()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2.5f);
        isDead = false;
        transform.position = initialPosition;
        spriteRenderer.enabled = true;
    }
}
