using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SceneSingleton<InventoryManager> //레벨업시 보상을 랜덤하게 등장시키는 것도 담당
{

    public List<ItemData> ownedItems = new List<ItemData>();
    public int maxItemCount = 6; // 예시: 6개까지만 장착 가능

    // 이미 획득한 아이템의 실제 컴포넌트 인스턴스 (같은 아이템 재획득 시 레벨업 처리용)
    private readonly Dictionary<ItemData, BaseItem> _activeItems = new Dictionary<ItemData, BaseItem>();

    public bool AddItem(ItemData newItem, PlayerController player)
    {
        if (newItem == null || player == null) return false;

        // 이미 보유 중인 아이템이면 새로 생성하지 않고 레벨업만 처리
        if (_activeItems.TryGetValue(newItem, out BaseItem existingItem))
        {
            existingItem.Acquire(player);
            return true;
        }

        if (ownedItems.Count >= maxItemCount)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        if (newItem.itemPrefab == null)
        {
            Debug.LogWarning($"{newItem.itemName}에 itemPrefab이 설정되어 있지 않습니다.");
            return false;
        }

        Transform spawnPoint = player.activeItemSpawnPoint != null ? player.activeItemSpawnPoint
            : player.weapon != null ? player.weapon : player.transform;

        // spawnPoint에 매달지 않고 위치값만 가져오고, 회전은 프리팹에 저장된 값 그대로 사용
        // 하이어라키상으로는 플레이어와 같은 레벨(형제)에 생성
        GameObject instance = Instantiate(newItem.itemPrefab, spawnPoint.position, newItem.itemPrefab.transform.rotation, player.transform.parent);

        BaseItem baseItem = instance.GetComponent<BaseItem>();
        if (baseItem == null)
        {
            Debug.LogWarning($"{newItem.itemPrefab.name}에 BaseItem 컴포넌트가 없습니다.");
            Destroy(instance);
            return false;
        }

        // 액티브(무기) 아이템은 발사/회수 기준점(spawnPoint)이 비어있으면 ActiveItemSpawnPoint를 기본값으로 사용
        if (baseItem is WeaponItem weaponItem && weaponItem.spawnPoint == null)
        {
            weaponItem.spawnPoint = spawnPoint;
        }

        baseItem.data = newItem;
        baseItem.Acquire(player);

        ownedItems.Add(newItem);
        _activeItems.Add(newItem, baseItem);

        Debug.Log($"아이템 획득: {newItem.itemName}");
        return true;
    }
}
