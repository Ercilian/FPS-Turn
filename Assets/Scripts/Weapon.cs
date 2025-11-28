using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{

    [SerializeField] string weaponName;
    [SerializeField] float weaponDmg;
    [SerializeField] float ArmourPen;
    [SerializeField] int maxAmmo;
    [SerializeField] int currentAmmo;
    [SerializeField] float weaponRange;
    public float WeaponRange => weaponRange;
    public float WeaponDmg => weaponDmg;
}

