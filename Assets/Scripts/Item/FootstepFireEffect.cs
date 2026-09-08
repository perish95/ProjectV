using UnityEngine;

public class FootstepFireEffect : WeaponItem
{
    [Header("이펙트 프리팹")]
    [Tooltip("걸을 때마다 생성할 불 이펙트 프리팹 (파티클 시스템 등)")]
    public GameObject firePrefab;

    [Header("생성 위치")]
    [Tooltip("생성 위치에 더해줄 랜덤 오프셋 반경")]
    public float spawnRadius = 0.1f;

    [Header("발걸음 간격")]
    [Tooltip("기준 이동 속도일 때의 발걸음 간격(초)")]
    public float baseStepInterval = 0.4f;

    [Header("정리")]
    [Tooltip("생성된 이펙트를 자동으로 삭제할 시간(초). 0 이하면 삭제하지 않음")]
    public float effectLifetime = 2f;

    private float _stepTimer;

    protected override void Start()
    {
        base.Start(); // weaponItemDamage = itemDamage, weaponItemCooldown = itemCooldown
    }

    protected override void Update()
    {
        if (baseItemPlayer == null || !baseItemPlayer.IsMoving)
        {
            _stepTimer = 0f;
            return;
        }

        float speedRatio = Mathf.Clamp(baseItemPlayer.Velocity.magnitude / Mathf.Max(baseItemPlayer.speed, 0.01f), 0.5f, 2f);
        float interval = baseStepInterval / speedRatio;

        _stepTimer += Time.deltaTime;
        if (_stepTimer >= interval)
        {
            _stepTimer = 0f;
            SpawnFireEffect();
        }
    }

    // Walk 애니메이션 클립에 Add Event로 추가해서 발이 땅에 닿는 타이밍에 정확히 호출할 수도 있음
    public void OnFootstep()
    {
        SpawnFireEffect();
    }

    private void SpawnFireEffect()
    {
        if (firePrefab == null || baseItemPlayer == null) return;

        Vector3 position = baseItemPlayer.transform.position;
        Quaternion rotation = baseItemPlayer.transform.rotation;

        if (spawnRadius > 0f)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            position += new Vector3(offset.x, 0f, offset.y);
        }

        GameObject effect = Instantiate(firePrefab, position, rotation);

        FootstepFireDamage fireDamage = effect.GetComponent<FootstepFireDamage>();
        if (fireDamage != null)
        {
            fireDamage.damage = weaponItemDamage; // Chakram과 동일하게 무기 레벨에 따라 스케일된 데미지 사용
        }

        if (effectLifetime > 0f)
        {
            Destroy(effect, effectLifetime);
        }
    }
}
