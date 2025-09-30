using UnityEngine;
using Mirror;
using Game.PlayerSide.Character;

public class WindTrigger : NetworkBehaviour
{
    [SerializeField] private float windForce;
    [SerializeField] private Vector2 windVector;
    private void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;
        if (!other.TryGetComponent(out CharacterMovement controller)) return;
        Debug.Log("trigger");

        controller.SetWindForce(windVector.normalized, windForce);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!isServer) return;
        if (!other.TryGetComponent(out CharacterMovement controller)) return;

        controller.ResetWindForce();
    }
}
