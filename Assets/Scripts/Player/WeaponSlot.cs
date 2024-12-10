using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private GameObject weaponObj = null;

    private float weaponDamage = 0;

    /// <summary>
    /// weaponDamage 접근 프로퍼티
    /// </summary>
    public float WeaponDamage { get => weaponDamage; }

    public void AddWeapon(ItemDataSO_Equipable data)
    {
        weaponObj = Instantiate(data.prefab, this.transform);
        weaponObj.name = data.name;
        weaponDamage = data.damage;
    }

    public void RemoveWeapon()
    {
        Destroy(weaponObj);
        weaponDamage = 0f;
    }
}