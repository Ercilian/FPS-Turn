using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Units))]
[RequireComponent(typeof(Shooting))]

public class EnemyAI : MonoBehaviour
{

    private Units units;
    private Shooting shooting;
    [SerializeField] private float visionRange = 15f;
    [SerializeField] private float moveRange = 10f;
    private bool isExecutingTurn = false;

    [SerializeField] Weapon weapon;
    EnemyCharacter enemyCharacter;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        units = GetComponent<Units>();
        shooting = GetComponent<Shooting>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyCharacter = GetComponent<EnemyCharacter>();
    }

    void Start()
    {
        Debug.Log("The weaponRange is: " + enemyCharacter.EquippedWeapon.WeaponRange);
    }
    void Update()
    {  
        if (units.isPlayerUnit)
            return;
        if (TurnManager.Instance.isPlayerTurn)
            return;
        if (!units.HasActed && !isExecutingTurn)
        {
            isExecutingTurn = true;
            StartCoroutine(ExecuteEnemyTurn());
        }
        
    }

    IEnumerator ExecuteEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        Units target = FindClosestPlayerUnit();
        if (target == null)
        {
            Debug.Log(units.CharacterName + " didn't found any target.");
            units.FinishAction();
            isExecutingTurn = false;
            yield break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= enemyCharacter.EquippedWeapon.WeaponRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }
        else
        {
            yield return MoveTowardsTarget(target.transform.position);

            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= enemyCharacter.EquippedWeapon.WeaponRange && hasLineOfSight(target))
            {
                yield return AttackTarget(target);
            }
            else
            {
                Debug.Log(units.CharacterName + " couldn't attack after moving.");
                yield return new WaitForSeconds(1f);
                units.FinishAction();
            }
        }

        isExecutingTurn = false;
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
        return shooting.isOnLoS(target.transform.position, enemyCharacter.EquippedWeapon.WeaponRange);
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

        shooting.Shot(target.transform.position, enemyCharacter.EquippedWeapon.WeaponRange);

        yield return new WaitForSeconds(0.5f);
        

        units.FinishAction();
        Debug.Log(units.CharacterName + " finished attack.");
    }

    private IEnumerator MoveTowardsTarget(Vector3 targetPosition)
    {
        Debug.Log(units.CharacterName + " is moving towards the target");

        float weaponRange = enemyCharacter.EquippedWeapon.WeaponRange;
        Vector3 direction = (targetPosition - transform.position).normalized; 
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        float desiredDistance = Mathf.Max(distanceToTarget - weaponRange + 2f, 0f);
        float moveDistance = Mathf.Min(desiredDistance, moveRange);

        Vector3 destination = transform.position + direction * moveDistance;
        agent.destination = destination;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        Debug.Log("Enemy reached destination.");
        yield return new WaitForSeconds(1f);
        units.FinishMove();
    }
    





}
