using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float moveSpeed;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    public float enemyExp;
}
