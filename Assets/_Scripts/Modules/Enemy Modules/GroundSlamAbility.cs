using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundSlamAbility", menuName = "ScriptableObjects/Abilities/GroundSlamAbility")]
public class GroundSlamAbility : AbilityAbstract
{
    [SerializeField] private float damageRadius = 10f;
    [SerializeField] private float healthCost = 20f;
    [SerializeField] private float damageAmount = 25f;
    [SerializeField] private float cooldownTime = 5f;
    private bool isOnCooldown = false;

    private EnemyComponentManager enemyComponentManager;
    private FactionScript factionScript;
    private HealthScript healthScript;

    public override void TurnOnAbility()
    {
        base.TurnOnAbility();
        enemyComponentManager = CollisionModuleController.instance.GetComponent<EnemyComponentManager>();
        factionScript = enemyComponentManager.GetComponent<FactionScript>();
        healthScript = enemyComponentManager.GetComponent<HealthScript>();

        if (enemyComponentManager != null && factionScript != null && healthScript != null && !isOnCooldown && healthScript._currentHealth > healthCost)
        {
            healthScript.TakeDamage(healthCost);
            SlamGround(enemyComponentManager);
            enemyComponentManager.StartCoroutine(CooldownCoroutine());
        }
    }

    private void SlamGround(EnemyComponentManager enemyComponentManager)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(enemyComponentManager.transform.position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            FactionScript targetFactionScript = hitCollider.GetComponent<FactionScript>();
            if (targetFactionScript != null)
            {
                if (targetFactionScript.userFaction != factionScript.userFaction)
                {
                    HealthScript targetHealthScript = hitCollider.GetComponent<HealthScript>();
                    if (targetHealthScript != null)
                    {
                        targetHealthScript.TakeDamage(damageAmount);
                    }
                }
                else if (hitCollider.gameObject == enemyComponentManager.gameObject)
                {
                    healthScript.TakeDamage(damageAmount);
                }
            }
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    public override void TurnOffAbility()
    {
        base.TurnOffAbility();
    }
}
