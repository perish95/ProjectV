using System.Collections.Generic;
using UnityEngine;

public class FootstepFireDamage : MonoBehaviour
{
    [Header("데미지 설정")]
    public float damage = 5f;
    [Tooltip("같은 적에게 데미지를 다시 줄 수 있는 최소 간격(초)")]
    public float tickInterval = 0.5f;

    private readonly Dictionary<Enemy, float> _nextDamageTime = new Dictionary<Enemy, float>();

    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamage(other);
    }

    //Chakram과 동일한 데미지 판정 로직 (tag 확인 후 TakeDamage 호출)
    private void TryDamage(Collider other)
    {
        if (other.tag != "Enemy") return;

        Enemy temp = other.GetComponent<Enemy>();
        if (temp == null) return;

        if (_nextDamageTime.TryGetValue(temp, out float nextTime) && Time.time < nextTime) return;

        temp.TakeDamage(damage);
        _nextDamageTime[temp] = Time.time + tickInterval;
    }
}
