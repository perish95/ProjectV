using UnityEngine;

// 필드에 놓이는 아이템 드랍 오브젝트. Collider(Is Trigger 체크)가 필요함.
[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [Header("이 오브젝트를 주웠을 때 지급할 아이템")]
    public ItemData data;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        if (InventoryManager.Instance.AddItem(data, player))
        {
            Destroy(gameObject);
        }
    }
}
