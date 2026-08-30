using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;

    public enum ItemType { Weapon, Passive }
    public ItemType itemType;

    [Header("획득 시 플레이어에게 붙일 실제 아이템 프리팹 (BaseItem 파생 컴포넌트 포함)")]
    public GameObject itemPrefab;
}