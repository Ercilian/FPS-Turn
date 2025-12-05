using UnityEditor;
using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSparks;
    Units Units;

    [Header("Probabilidad de impacto (Aliados y Enemigos)")]
    [Range(0f, 1f)] public float shortRangeHitChance = 1f;
    [Range(0f, 1f)] public float midRangeHitChance = 0.7f;
    [Range(0f, 1f)] public float longRangeHitChance = 0.4f;

    void Awake()
    {
        Units = GetComponent<Units>();
    }

    public void Shot(Vector3 enemyPosition, float weaponRange, float weaponDmg)
    {
        Character target = isOnLoS(enemyPosition, weaponRange);
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, enemyPosition);
            float rangeStep = weaponRange / 3f;
            float hitChance = 1f;

            if (Units.isPlayerUnit)
            {
                if (distance <= rangeStep)
                    hitChance = shortRangeHitChance;
                else if (distance <= rangeStep * 2f)
                    hitChance = midRangeHitChance;
                else
                    hitChance = longRangeHitChance;
            }
            else
            {
                if (distance <= rangeStep)
                    hitChance = shortRangeHitChance;
                else if (distance <= rangeStep * 2f)
                    hitChance = midRangeHitChance;
                else
                    hitChance = longRangeHitChance;
            }

            if (Random.value <= hitChance)
            {
                Vector3 direction = (enemyPosition - particleSparks.transform.position).normalized;
                particleSparks.transform.rotation = Quaternion.LookRotation(direction);

                particleSparks.Play();
                target.TakeDamage(weaponDmg);
                Units.FinishAttack();
                Debug.Log($"{Units.CharacterName} hit! Chance: {hitChance}");
            }
            else
            {
                Debug.Log($"{Units.CharacterName} missed! Chance: {hitChance}");
                Units.FinishAttack();
            }
        }
        else
        {
            Debug.Log(Units.CharacterName + " enemy not in line of sight.");
        }
    }

    public void ShotWithDelay(Vector3 enemyPosition, float weaponRange, float weaponDmg)
    {
        StartCoroutine(ShotCoroutine(enemyPosition, weaponRange, weaponDmg));
    }

    private IEnumerator ShotCoroutine(Vector3 enemyPosition, float weaponRange, float weaponDmg)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Attacking");

        yield return new WaitForSeconds(1f);

        Shot(enemyPosition, weaponRange, weaponDmg);
    }

    public Character isOnLoS(Vector3 enemyPosition, float weaponRange)
    {
        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * weaponRange, Color.red, 1f);

        if (Physics.Raycast(transform.position, direction, out hit, weaponRange))
        {
            Character character = hit.collider.GetComponent<Character>();
            if (character != null)
            {
                return character;
            }
        }
        return null;
    }
}
