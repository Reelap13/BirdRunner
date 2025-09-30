using UnityEngine;
using Mirror;

public class Feather : NetworkBehaviour
{
    [SerializeField] private Rigidbody projectileRigidbody;
    [SerializeField] private float projectileLifetime = 1f;

    private bool isDisposed;

    [SerializeField] private bool spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem effectOnDestroyPrefab;
    [SerializeField] private float effectOnDestroyLifetime = 2f;

    public Rigidbody Rigidbody => projectileRigidbody;

    private void Start()
    {
        if (!isServer) return;
        Invoke(nameof(DisposeProjectile), projectileLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;
        if (isDisposed) return;


        DisposeProjectile();
    }


    private void DisposeProjectile()
    {

        SpawnEffectOnDestroy();

        Destroy(gameObject);

        NetworkServer.Destroy(gameObject);

        isDisposed = true;
    }

    private void SpawnEffectOnDestroy()
    {
        if (spawnEffectOnDestroy == false) return;

        var effect = Instantiate(effectOnDestroyPrefab, transform.position, Quaternion.identity);
        effect.Play();

        Destroy(effect.gameObject, effectOnDestroyLifetime);
    }
}
