using UnityEditor;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public void Start()
    {
        Vector3 enemyPosition = new Vector3(0, 0, 0);
        float weaponRange = 10f;
        isOnLoS(enemyPosition, weaponRange);
    }

    public bool isOnLoS(Vector3 enemyPosition, float weaponRange)
    {
        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;
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
