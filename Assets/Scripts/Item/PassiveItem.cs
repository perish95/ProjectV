using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{                   //초기값
    Health,         //50
    Damage,         //10    
    Speed,          //0.1
    Range,          //0.1
    AttackSpeed     //0.1
}
public class PassiveItem : BaseItem
{
    [Header("패시브 아이템 옵션")]
    public StatType statType;        // 올릴 능력치
    public float statAmount;        // 증가량 (퍼센트 또는 절댓값)
    public float perLevelMultiplier = 0.1f;     // 레벨 기반 증가량

    // 플레이어에 적용할 실제 값
    private float appliedStatAmount;

    // 아이템 장착 시 호출
    private void ApplyPassiveItem()
    {
        appliedStatAmount = statAmount * (1 + (ItemLevel - 1) * perLevelMultiplier);
        baseItemPlayer.IncreaseStat(statType, statAmount);
    }
    public override void OnLevelChanged()
    {
        ApplyPassiveItem();
    }
}
