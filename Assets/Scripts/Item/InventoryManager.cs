using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SceneSingleton<InventoryManager> //레벨업시 보상을 랜덤하게 등장시키는 것도 담당 
{
    
    public List<ItemData> ownedItems = new List<ItemData>();
    public int maxItemCount = 6; // 예시: 6개까지만 장착 가능

    public bool AddItem(ItemData newItem)
    {
        if (ownedItems.Count >= maxItemCount)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        ownedItems.Add(newItem);
        ApplyItemEffect(newItem);
        Debug.Log($"아이템 획득: {newItem.itemName}");
        return true;
    }

    private void ApplyItemEffect(ItemData item)
    {
        //내가 PlayerController 보고 직접 맞게 고쳐야 함
        /*Player player = PlayerStats.Instance;

        switch (item.statType)
        {
            case ItemData.StatType.Attack:
                player.attackPower += item.value;
                break;
            case ItemData.StatType.MoveSpeed:
                player.moveSpeed += item.value;
                break;
            case ItemData.StatType.Health:
                player.maxHealth += item.value;
                break;
            case ItemData.StatType.Defense:
                player.defense += item.value;
                break;
            case ItemData.StatType.AttackSpeed:
                player.attackSpeed += item.value;
                break;
        }*/
    }
}
