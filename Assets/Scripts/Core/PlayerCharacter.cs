using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : Character
{

    float Xp;
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Equipment equippedEquipment;
    [SerializeField] List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();
    public GameObject targetSelectionPanel;
            
    public Weapon EquippedWeapon => equippedWeapon;




    protected override void Start()
    {
        base.Start();
        targetSelectionPanel.SetActive(false);
        equippedWeapon = weaponList[0];
        equippedEquipment = equipmentList[0];

        defByEquipment = equippedEquipment.Armour;

    }

    void Update()
    {
        
    }

    void EarnExperience(float expGained)
    {
        Xp += expGained;
        Debug.Log(gameObject.name + " earned " + expGained + " XP.");
    }

    void LevelUp()
    {
        
        level++;

    }
}

