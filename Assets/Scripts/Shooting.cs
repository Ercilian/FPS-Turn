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
    public void Shoot(Vector3 enemyPosition, float weaponRange)
    {
        if (isOnLoS(enemyPosition, weaponRange))
        {
            particleSparks.Play();
            Debug.Log("Shooting at the enemy!");
            Units.FinishAttack();
        }
        else
        {
            Debug.Log("Enemy not in line of sight.");
        }
    }

    public bool isOnLoS(Vector3 enemyPosition, float weaponRange)
    {
        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * weaponRange, Color.red, 1f);

        if (Physics.Raycast(transform.position, direction, out hit, weaponRange))
        {
            Character character = hit.collider.GetComponent<Character>();
            if (character != null)
            {
                return true;
            }
        }
        return false;
    }
    
}
