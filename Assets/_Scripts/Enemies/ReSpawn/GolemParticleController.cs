using UnityEngine;

public class DeathParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    private HealthScript healthScript;

    private void Start()
    {
        healthScript = GetComponent<HealthScript>();
        if (healthScript != null)
        {
            healthScript.OnDeath += HandleDeath;
        }
    }

    private void HandleDeath()
    {
        if (deathParticles != null)
        {
            ParticleSystem spawnedDeathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            spawnedDeathParticles.Play();
        }
    }

    private void OnDestroy()
    {
        if (healthScript != null)
        {
            healthScript.OnDeath -= HandleDeath;
        }
    }
}
