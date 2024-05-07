using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] grenades;
    public bool[] hasWeapons;

    public int ammo, coin, health, hasGrenades;
    public int maxAmmo, maxCoin, maxHealth, maxHasGrenades;

    GameObject nearObject;
    public Weapon equipWeapon;
    PlayerMovement pm;

    int equipWeaponIndex = -1;

    void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }

    public void Interaction()
    {
        if (pm.iDown && nearObject != null) //상호작용키 입력 && nearObject가 존재할경우
        {
            //Debug.Log("YES");
            if (nearObject.tag == "Weapon") //nearObject의 태그가 "Weapon"일경우
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject); //nearObject 삭제
            }
        }
    }

    public void Swap()
    {
        if (pm.sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) return;
        if (pm.sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) return;
        if (pm.sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) return;

        int weaponIndex = -1;
        if (pm.sDown1) weaponIndex = 0;
        if (pm.sDown2) weaponIndex = 1;
        if (pm.sDown3) weaponIndex = 2;

        if ((pm.sDown1 || pm.sDown2 || pm.sDown3) && !pm.isJump && !pm.isSwap)
        {
            Debug.Log("Swap!");
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            pm.anim.SetTrigger("doSwap");

            pm.isSwap = true;
            Invoke("SwapOut", 0.4f);
        }
    }

    public void SwapOut()
    {
        pm.isSwap = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();

            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                        ammo = maxCoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        ammo = maxHealth;
                    break;
                case Item.Type.Grenade:
                    grenades[hasGrenades].SetActive(true);
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}
