using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment", order = 2)]
public class Equipment : ScriptableObject
{

    [SerializeField] float maxDurability;
    [SerializeField] float cur_Durability;
    [SerializeField] float movementSpeed;
    [SerializeField] float Armour;

}
