using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stat")] 
    public float speed = 1f;
    public float rotationSpeed = 10f;
    public float maxHp = 100f;
    public float currentHp;
    public float attackDamage = 20f;
    public float attackRange = 2f;
    public float attackSpeed = 0.5f;
    public float expGrowthRate = 1.2f;
    public delegate void LevelUpEvent(int newLevel); //Level Up Delegate
    public event LevelUpEvent OnLevelUp;
    public int maxLevel = 30;

    public Transform weapon;

    public bool IsMoving => isMoving;

    private Rigidbody rb;
    private Animator _animator;
    private bool isMoving = false;
    private bool isAttacking = false;
    private HashSet<Enemy> hitEnemiesThisAttack = new HashSet<Enemy>();
    private int currentLevel = 1;
    private float currentExp = 0f;
    private float requiredExp = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _animator.SetInteger("animation", 40);
        StartCoroutine(PlayerAttackCoroutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        /***플레이어 애니메이션***/
        if (isMoving)
        {
            _animator.SetInteger("animation", 6);
            float speedRatio = rb.velocity.magnitude / speed;
            speedRatio = Mathf.Clamp(speedRatio, 0.8f, 1.8f);
            _animator.SetFloat("WalkSpeed", speedRatio);
        }
        else
            _animator.SetInteger("animation", 40);

        if (Input.GetKey(KeyCode.F))
            _animator.SetInteger("animation", 40);
    }

    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);


        if (h != 0 || v != 0) isMoving = true;
        else isMoving = false;

        if (move.magnitude > 1f) move.Normalize();

        Vector3 vel = move * speed;

        vel.y = rb.velocity.y;
        rb.velocity = vel;

        if (move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    IEnumerator PlayerAttackCoroutine()
    {
        while (true)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                _animator.SetFloat("attack", attackSpeed);

                yield return new WaitForSeconds(GetCurrentAnimationLength());
                isAttacking = false;
            }
            yield return null;
        }
    }

    public void PlayerAttack() //Player Attack 애니메이션 클립에 Add Event로 추가
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider col in hitEnemies)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                hitEnemiesThisAttack.Add(enemy);
                enemy.TakeDamage(attackDamage);
                Debug.Log($"{enemy.name}에게 {attackDamage} 데미지!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        //Debug.Log($"플레이어 HP: {currentHp}");

        if (currentHp <= 0)
        {
            DeathEvent();
        }
    }

    private void DeathEvent()
    {
        Debug.Log("Player dead");
    }

    float GetCurrentAnimationLength()
    {
        // 레이어 1의 현재 상태 정보 가져오기
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(1);

        // 레이어 1에서 재생 중인 모든 클립 중 상태 이름과 일치하는 클립 찾기
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (state.IsName(clip.name))
            {
                return clip.length;
            }
        }

        // 실패 시 기본값
        return 0.5f;
    }
    
    public void AddExperience(float amount)
    {
        currentExp += amount;
        Debug.Log($"경험치 획득: {amount} (현재 경험치: {currentExp}/{requiredExp})");

        if (currentExp >= requiredExp)
        {
            LevelUp();
        }
    }

    public void IncreaseStat(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Health:
                maxHp += amount;
                break;
            case StatType.Damage:
                attackDamage += amount;
                break;
            case StatType.Speed:
                speed *= amount;
                break;
            case StatType.Range:
                attackRange *= amount;
                break;
            case StatType.AttackSpeed:
                attackSpeed *= amount;

                break;
        }
    }

    private void LevelUp() //레벨업 시에 보상을 띄우는 것 구현필요
    {
        if (currentLevel >= maxLevel) return;
        
        currentLevel++;
        currentExp -= requiredExp; // 남은 경험치는 다음 레벨로 이월
        requiredExp *= expGrowthRate; // 다음 레벨 요구 경험치 증가

        Debug.Log($"현재 레벨: {currentLevel}");

        OnLevelUp?.Invoke(currentLevel);
    }
    
    private void OnDrawGizmosSelected()
    {
        // 공격 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}