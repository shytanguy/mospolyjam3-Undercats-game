using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfRepairAbility", menuName = "ScriptableObjects/Abilities/SelfRepairAbility")]
public class SelfRepairAbility : AbilityAbstract
{
    [SerializeField] private float repairRate = 10f;
    [SerializeField] private float inactiveDuration = 3f;

    private EnemyComponentManager enemyComponentManager;
    private HealthScript healthScript;

    private Coroutine repairCoroutine;

    public override void TurnOnAbility()
    {
        base.TurnOnAbility();
        enemyComponentManager = CollisionModuleController.instance.GetComponent<EnemyComponentManager>();
        healthScript = enemyComponentManager.GetComponent<HealthScript>();

        if (healthScript != null)
        {
            repairCoroutine = enemyComponentManager.StartCoroutine(StartRepairAfterDelay());
        }
    }

    private IEnumerator StartRepairAfterDelay()
    {
        yield return new WaitForSeconds(inactiveDuration);

        while (AbilityOn)
        {
            healthScript.Heal(repairRate * Time.deltaTime);
            yield return null;
        }
    }

    public override void TurnOffAbility()
    {
        base.TurnOffAbility();
        if (repairCoroutine != null)
        {
            enemyComponentManager.StopCoroutine(repairCoroutine);
        }
    }
}
