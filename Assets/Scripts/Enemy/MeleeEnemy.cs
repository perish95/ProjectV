using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float lastAttackTime;
    
    private void OnTriggerStay(Collider other)
    {
        // 플레이어 태그 감지
        if (!other.CompareTag("Player")) return;

        // 공격 쿨타임 체크
        if (Time.time - lastAttackTime < stats.attackCooldown) return;

        // 플레이어 데미지 처리
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(stats.attackDamage);
            Debug.Log($"{stats.enemyName}가 {stats.attackDamage} 피해를 입혔습니다!");
        }

        lastAttackTime = Time.time;
    }

    public override void Attack()
    {
        /*if (Time.time - lastAttackTime < stats.attackCooldown) return;

        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance <= stats.attackRange)
        {

            PlayerController playerController = Player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(stats.attackDamage);
                Debug.Log($"플레이어가 {stats.attackDamage} 데미지를 받음!");
            }
            
            lastAttackTime = Time.time;
        }*/
    }

    protected override void Update()
    {
        base.Update(); 
    }
}
