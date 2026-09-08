using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : BaseItem
{
    [Header("무기 공통 옵션")] 
    public float itemDamage;
    public float itemCooldown;
    public Transform spawnPoint; //액티브 아이템을 획득 시에 플레이어 주변에 생성될 위치

    protected float weaponItemDamage;
    protected float weaponItemCooldown;

    //private float cooldownTimer = 0f;

    private bool statsInitialized;

    protected virtual void Start()
    {
        EnsureStatsInitialized();
    }

    // Start()는 프레임 지연 실행이라, Instantiate 직후 같은 프레임에 Acquire()가 먼저 불릴 수 있음.
    // 어느 쪽이 먼저 오든 weaponItemDamage/weaponItemCooldown이 레벨업 전에 반드시 초기화되도록 보장.
    private void EnsureStatsInitialized()
    {
        if (statsInitialized) return;
        statsInitialized = true;

        weaponItemDamage = itemDamage;
        weaponItemCooldown = itemCooldown;
    }

    public override void Acquire(PlayerController playerController)
    {
        EnsureStatsInitialized();
        base.Acquire(playerController);
    }

    protected virtual void Update()
    {
    }

    //액티브 아이템 레벨업 시에 20%씩 상승
    public override void OnLevelChanged()
    {
        weaponItemDamage *= 1.2f;
    }
}