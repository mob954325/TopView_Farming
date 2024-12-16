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
        weaponObj = Instantiate(data.weaponPrefab, this.transform);
        weaponObj.name = data.name;
        weaponDamage = data.damage;
    }

    public void RemoveWeapon()
    {
        if (weaponObj != null)
        {
            Destroy(weaponObj);
            weaponDamage = 0f;
        }
        else
        {
            // 장착된 무기가 없습니다.
        }
    }
}