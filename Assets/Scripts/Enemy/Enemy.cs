using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IPoolable
{
    public EnemyStats stats;
    public string OriginTag { get; private set; }
    
    [SerializeField] protected float CurrentHealth;
    protected Transform Player;

    public abstract void Attack(); //Enemy 전체적으로 필요한 것이 있을 경우 함수명을 변경해서 사용할 것

    protected virtual void Start()
    {
        CurrentHealth = stats.maxHealth;
    }

    protected virtual void Update()
    {
        if (Player == null) return;
        
        if(Vector3.Distance(Player.position, transform.position) > stats.attackRange) //공격거리 도달하면 이동 정지
            ChasePlayer();
    }

    protected virtual void ChasePlayer()
    {
        Vector3 dir = (Player.position - transform.position).normalized;
        transform.position += dir * stats.moveSpeed * Time.deltaTime;
        transform.LookAt(Player);
    }

    public virtual void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(stats.enemyName + " 사망");
        if (ObjectPoolManager.Instance.IsTagFullyActive("ExpOrb")) return;
        
        GameObject expOrbObj = ObjectPoolManager.Instance.SpawnFromPool("ExpOrb", transform.position, Quaternion.identity);
        
        ExpOrb expOrb = expOrbObj.GetComponent<ExpOrb>();
        if (expOrb != null)
        {
            expOrb.SetExpValue(stats.enemyExp); // 적의 경험치 스탯 값 전달
        }
        
        ObjectPoolManager.Instance.ReturnToPool(gameObject, OriginTag);
    }
    
    private void OnDrawGizmosSelected()
    {
        // 공격 범위 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    public void GetPlayerPosition(Transform pos)
    {
        Player = pos;
    }
    
    public void SetOriginTag(string tag)
    {
        OriginTag = tag;
    }

    public void OnSpawn()
    {
        CurrentHealth = stats.maxHealth;
    }

    public void OnDespawn()
    {
        // 비활성화시 이벤트 추가
    }
}
