using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Units))]
[RequireComponent(typeof(Shooting))]

public class EnemyAI : MonoBehaviour
{

    private Units units;
    private Shooting shooting;
    [SerializeField] private float visionRange = 15f;
    private float attackRange;

    void Awake()
    {
        units = GetComponent<Units>();
        shooting = GetComponent<Shooting>();
    }

    void Start()
    {
        
    }
    void Update()
    {  
        if (units.isPlayerUnit)
        {
            return;
        }
        if(TurnManager.Instance.isPlayerTurn)
        {
            return;
        }
        if (!units.HasActed)
        {
            StartCoroutine(ExecuteEnemyTurn());
        }
        
    }

    IEnumerator ExecuteEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        Units target = FindClosestPlayerUnit();
        if (target != null)
            {
                Debug.Log(units.CharacterName + " didn't found any target: " + target.CharacterName);
                units.FinishAction();
                yield break;
            }


        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }


    }

    private Units FindClosestPlayerUnit()
    {
        Units closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Units playerunit in TurnManager.Instance.playerUnits)
        {
            float distance = Vector3.Distance(transform.position, playerunit.transform.position);
            if (distance < closestDistance && distance <= visionRange)
            {
                closestDistance = distance;
                closest = playerunit;
            }
        }
        return closest;
    }

    private bool hasLineOfSight(Units target)
    {
        return true;
    }

    private IEnumerator AttackTarget(Units target)
    {
        return null;
    }

    





}
