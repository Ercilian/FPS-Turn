using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] string characterName;
    [SerializeField] float cur_HP;
    [SerializeField] float max_HP;
    [SerializeField] protected float defByEquipment;
    public Animator Animator;

    protected int level = 1;
    private bool isDead = false;

    protected virtual void Start()
    {

        characterName = gameObject.name;
        cur_HP = max_HP;
    }
    public void TakeDamage(float Dmg)
    {

        float finalDmg = Dmg - defByEquipment;
        cur_HP -= finalDmg;
        Debug.Log(characterName + " took " + finalDmg + " damage. Current HP: " + cur_HP + "/" + max_HP);
        
        IsAlive();

    }
    
    public void IsAlive()
    {

        if (cur_HP <= 0 && !isDead)
        {
            isDead = true;
            Animator.SetBool("IsDead", true);
            Debug.Log(characterName + " has been defeated.");
            StartCoroutine(DeathRoutine());
        }

    }

    IEnumerator DeathRoutine()
    {
        Units units = GetComponent<Units>();
        if (units != null)
        {
            TurnManager.Instance.RemoveUnit(units);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }





}
