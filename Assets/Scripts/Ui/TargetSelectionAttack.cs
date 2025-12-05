using UnityEngine;
using UnityEngine.UI;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;
    [SerializeField] Button attackButton1;
    [SerializeField] Button attackButton2;

    Shooting shootingComponent;
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

    public void UpdateAttackButtons()
    {
        Shooting shootingComponent = characterShooting.GetComponent<Shooting>();
        PlayerCharacter playerCharacter = characterShooting.GetComponent<PlayerCharacter>();

        float range = playerCharacter.EquippedWeapon.WeaponRange;
        attackButton1.interactable = shootingComponent.isOnLoS(target_1.transform.position, range) != null;
        attackButton2.interactable = shootingComponent.isOnLoS(target_2.transform.position, range) != null;
    }
}