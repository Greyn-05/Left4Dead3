using UnityEngine;

public enum ItemType
{
    Null,//Default
    Weapon,//주무기
    SubWeapon,//보조무기
    Grenade,//수류탄
    HealPack,//회복아이템
}

[CreateAssetMenu(fileName = "DefaultItemInfo", menuName = "TopDownController/ItemInfo/Default", order = 0)]
public class ItemInfo : ScriptableObject
{
    public ItemType m_type;//아이템 종류
    public int m_itemId;//아이템 코드
    public string m_itemName;//이름
    public int m_point;//데미지, 회복량
    public int m_stacks;//개수
    public GameObject m_prefab;//프리펩
    public Sprite m_image;//아이템 이미지
}
