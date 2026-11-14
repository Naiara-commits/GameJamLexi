using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TeleportOnTrigger2D : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private readonly HashSet<int> blockedColliderIds = new HashSet<int>();

    private void Awake()
    {
        if (!GetComponent<Collider2D>().isTrigger)
            Debug.LogWarning("El collider del teletransporte debe tener Is Trigger activado.", this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        int colliderId = other.GetInstanceID();
        if (blockedColliderIds.Contains(colliderId))
            return;

        if (destination == null)
        {
            Debug.LogError("No se ha asignado el destino del teletransporte.", this);
            return;
        }

        TeleportOnTrigger2D arrivalPortal = destination.GetComponent<TeleportOnTrigger2D>();
        if (arrivalPortal == null)
            arrivalPortal = destination.GetComponentInParent<TeleportOnTrigger2D>();

        if (arrivalPortal != null)
        {
            arrivalPortal.blockedColliderIds.Add(colliderId);
            Debug.Log($"Se bloqueará '{arrivalPortal.name}' para este jugador hasta que salga de su trigger.", arrivalPortal);
        }
        else
        {
            Debug.LogWarning("No se encontró un teletransportador en el destino ni en sus padres; no se podrá evitar el rebote al llegar.", this);
        }

        Rigidbody2D playerBody = other.attachedRigidbody;
        if (playerBody != null)
        {
            playerBody.position = destination.position;
            playerBody.linearVelocity = Vector2.zero;
        }
        else
        {
            other.transform.position = destination.position;
        }

        Debug.Log($"Jugador teletransportado a '{destination.name}'.", this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        blockedColliderIds.Remove(other.GetInstanceID());
    }
}
