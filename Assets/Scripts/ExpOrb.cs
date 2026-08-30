using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour,IPoolable
{
    public float moveSpeed = 5f;      // 플레이어에게 빨려드는 속도
    public float pickupRange = 2f;    // 흡수 거리

    private Transform player;
    private bool isAttracted = false;
    private float expValue = 0;         // 경험치 값
    public string OriginTag { get; private set; }

    public void SetOriginTag(string tag)
    {
        OriginTag = tag;
    }

    private void Update()
    {
        /*if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 일정 거리 안으로 들어오면 플레이어에게 끌려감
        if (distance < pickupRange)
            isAttracted = true;

        if (isAttracted)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // 아주 가까워지면 경험치 추가 후 오브젝트 반환
            if (distance < 0.5f)
            {
                /*var levelSystem = player.GetComponent<PlayerLevelSystem>();
                if (levelSystem != null)
                {
                    levelSystem.AddExperience(expValue);
                }#1#

                ObjectPoolManager.Instance.ReturnToPool(gameObject, OriginTag);
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerExp = other.GetComponent<PlayerController>();
            playerExp.AddExperience(expValue);
            
            ObjectPoolManager.Instance.ReturnToPool(gameObject, "ExpOrb");  // 오브젝트 풀 매니저에 경험치 오브 등록할 것 
        }
    }

    public void SetExpValue(float exp)
    {
        expValue = exp;
    }

    public void OnSpawn()
    {
        isAttracted = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
