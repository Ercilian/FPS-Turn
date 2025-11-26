using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Units))]
[RequireComponent(typeof(Shooting))]

public class EnemyAI : MonoBehaviour
{

    private Units units;
    private Shooting shooting;
    [SerializeField] private float visionRange = 15f;
    private float weaponRange;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        units = GetComponent<Units>();
        shooting = GetComponent<Shooting>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        if (distanceToTarget <= weaponRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }
        else
        {
            yield return MoveTowardsTarget(target.transform.position);

            if (distanceToTarget <= weaponRange && hasLineOfSight(target))
            {
                yield return AttackTarget(target);
            }
            else
            {
                units.FinishAction();
            }
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
        return shooting.isOnLoS(target.transform.position, weaponRange);
    }

    private IEnumerator AttackTarget(Units target)
    {
        Debug.Log(units.CharacterName + " is attacking " + target.CharacterName);

        Vector3 lookAtPosition = target.transform.position - transform.position;
        lookAtPosition.y = 0;
        if (lookAtPosition != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookAtPosition);
        }

        shooting.Shoot(target.transform.position, weaponRange);

        yield return new WaitForSeconds(0.5f);
        
        if(units.HasMoved)
        {
            units.FinishAction();
            yield break;
        }
        units.FinishAttack();
    }

    private IEnumerator MoveTowardsTarget(Vector3 targetPosition)
    {
        Debug.Log(units.CharacterName + " is moving towards the target.");

        agent.destination = targetPosition;
        yield return new WaitForSeconds(5f);
        units.FinishMove();
    }
    





}
