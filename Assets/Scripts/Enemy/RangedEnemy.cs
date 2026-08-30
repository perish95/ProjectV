using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    
    private float lastAttackTime;

    public override void Attack()
    {
        if (Time.time - lastAttackTime < stats.attackCooldown) return;

        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance <= stats.attackRange)
        {
            ShootProjectile();
            lastAttackTime = Time.time;
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab && firePoint)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * 10f;
            Debug.Log(stats.enemyName + "이(가) 원거리 공격!");
        }
    }

    protected override void Update()
    {
        base.Update();
        Attack();
    }
}
