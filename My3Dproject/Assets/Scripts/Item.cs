using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon }; //열거형으로 아이템 정의
    public Type type;
    public int value;

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime); //아이템 회전
    }
}
