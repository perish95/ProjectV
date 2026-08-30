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

    protected virtual void Start()
    {
        weaponItemDamage = itemDamage;
        weaponItemCooldown = itemCooldown;
    }

    protected virtual void Update()
    {
        /*
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= currentCooldown)
        {
            cooldownTimer = 0f;
            Attack();
        }*/
    }
    

    public abstract void Attack();

    //액티브 아이템 레벨업 시에 20%씩 상승
    public override void OnLevelChanged()
    {
        weaponItemDamage *= 1.2f;
    }
}