using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : Character
{

    float Xp;
    Weapon equippedWeapon;
    Equipment equippedEquipment;
    [SerializeField] List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();



    void Start()
    {
        
        equippedWeapon = weaponList[0];
        equippedEquipment = equipmentList[0];

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

