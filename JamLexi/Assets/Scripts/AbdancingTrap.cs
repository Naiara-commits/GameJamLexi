using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AbdancingTrap : MonoBehaviour
{
    [SerializeField] float velocity = 5.0f;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Vector3 finalPosition;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] PlayerMovement2D playerMovement2D;
    private bool isDead = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        playerMovement2D.Stun(1.5f);
        Death();
    }

    void Move()
    {
        if (isDead) return;
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, velocity * Time.deltaTime);
        if(transform.position == finalPosition)
        {
            Death();
        }

    }

    void Death()
    {
        isDead = true;
        StartCoroutine(Revive());
    }

    IEnumerator Revive()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2.5f);
        isDead = false;
        transform.position = initialPosition;
        spriteRenderer.enabled = true;
    }
}
