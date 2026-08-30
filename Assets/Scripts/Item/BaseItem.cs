using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [Header("아이템 데이터 참조")]
    public ItemData data;
    
    public int ItemLevel => itemLevel; //itemLevel getter

    protected PlayerController baseItemPlayer; //수정 필요

    private int itemLevel = 0;
    [SerializeField] private int maxItemLevel = 5;
    
    //playerController는 매개로 받는 대신에 InventoryManager에서 받아오는게 현명해보인다. 아니면 GameManager를 만들어서 Player의 정보를 읽어오는 것도 좋아 보인다.
    public virtual void Acquire(PlayerController playerController)
    {
        baseItemPlayer = playerController;

        if (itemLevel < maxItemLevel)
        {
            itemLevel++;
            OnLevelChanged();
        }
        else
        {
            //5레벨일 경우 더이상 나오지 않도록 
            Debug.Log(data.itemName + "은(는) 이미 최대 레벨입니다.");
        }
    }
    
    // 레벨이 변경될 때 실행되는 함수 (무기/패시브별로 재정의)
    public abstract void OnLevelChanged();
    

    // UI 등에서 설명용으로 표시
    public virtual string GetDescription()
    {
        return $"{data.itemName} \n{data.description}";
    }
}
