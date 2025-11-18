using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] string name;
    [SerializeField] float cur_HP;
    [SerializeField] float max_HP;
    [SerializeField] float baseDmg;
    [SerializeField] protected float baseDef;

    protected int level;

    void Start()
    {

        name = gameObject.name;
        cur_HP = max_HP;

    }
    void TakeDamage(float Dmg)
    {

        float finalDmg = Dmg - baseDef;
        cur_HP -= finalDmg;
        IsAlive();

    }

    void IsAlive()
    {

        if (cur_HP <= 0)
        {
            Debug.Log(name + " has been defeated.");
            Destroy(gameObject);
        }

    }





}
