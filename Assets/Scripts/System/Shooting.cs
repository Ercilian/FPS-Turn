using UnityEditor;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] ParticleSystem particleSparks;
    Units Units;

    void Awake()
    {
        Units = GetComponent<Units>();
    }
    public void Shot(Vector3 enemyPosition, float weaponRange, float weaponDmg)
    {
        Character target = isOnLoS(enemyPosition, weaponRange);
        if (target != null)
        {
            Vector3 direction = (enemyPosition - particleSparks.transform.position).normalized;
            particleSparks.transform.rotation = Quaternion.LookRotation(direction);

            particleSparks.Play();
            target.TakeDamage(weaponDmg);
            Units.FinishAttack();
        }
        else
        {
            Debug.Log(Units.CharacterName + " enemy not in line of sight.");
        }
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
