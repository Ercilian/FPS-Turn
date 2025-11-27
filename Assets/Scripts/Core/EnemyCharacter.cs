using UnityEngine;

public class EnemyCharacter : Character
{

    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Equipment equippedEquipment;

    public Weapon EquippedWeapon => equippedWeapon;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
