using UnityEngine;

public class EnemyCharacter : Character
{

    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Equipment equippedEquipment;

    public Weapon EquippedWeapon => equippedWeapon;

    protected override void Start()
    {
        base.Start();
        
        defByEquipment = equippedEquipment.Armour;
    }
}
