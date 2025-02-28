using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    Rifle,
    Shotgun,
    Sniper
}
public enum GunStyle
{
    nonautomatic, 
    automatic
}

[CreateAssetMenu(fileName = "Item", menuName = "New Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string gunName;
    public string description;
    public GunType gunType;
    public GunStyle gunStyle;
    public Sprite icon;
    public GameObject DropPrefab; //아이템을 버렸을 때 스폰되는 프리팹

    [Header("Stacking")]
    public int nowBulletInTheGun; // 탄알집에 남아있는 탄알
    public int maxBulletInTheGun; // 탄알집에 최대로 채울 수 있는 수
    public int maxBulletAmount; // 소유중인 탄알 수

    [Header("GunDamage")]
    public int Damage;
}