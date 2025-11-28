using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{

    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;

    Shooting shootingComponent;
    [SerializeField] Weapon Weapon;
    PlayerCharacter playerCharacter;

    void Start()
    {
        shootingComponent = characterShooting.GetComponent<Shooting>();
        playerCharacter = characterShooting.GetComponent<PlayerCharacter>();
    }

    public void ShootTarget1()
    {
        Debug.Log("Aiming at " + target_1.name + " with the weapon range: " + playerCharacter.EquippedWeapon.WeaponRange + " and damage: " + playerCharacter.EquippedWeapon.WeaponDmg);
        shootingComponent.Shot(target_1.transform.position, playerCharacter.EquippedWeapon.WeaponRange, playerCharacter.EquippedWeapon.WeaponDmg);
    }

    public void ShootTarget2()
    {
        Debug.Log("Aiming at " + target_2.name + " with the weapon range: " + playerCharacter.EquippedWeapon.WeaponRange + " and damage: " + playerCharacter.EquippedWeapon.WeaponDmg);
        shootingComponent.Shot(target_2.transform.position, playerCharacter.EquippedWeapon.WeaponRange, playerCharacter.EquippedWeapon.WeaponDmg);
    }
}
