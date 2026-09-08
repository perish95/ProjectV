using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//차크람이 캐릭터랑 충돌하지 않도록 하기
//쿨타임 대한 해결 필요(갓다 왓을 때 쿨타임 동안 false로 한다든지


public class Chakram : WeaponItem
{
    [Header("Chakram")]
    public float speed = 10f;
    public float maxDistance = 10f;
    public float rotateSpeed = 720f;

    private Vector3 targetPos; //도착지점
    private float coolTime = 0f; //넣어야하는지 고민
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChakramLoop()); //temp
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ChakramLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ChakramRoutine()); // 왕복 실행
            yield return new WaitForSeconds(coolTime);     // 텀
        }
    }

    IEnumerator ChakramRoutine() //yield return 대신에 쿨타임 필요
    {
        Vector3 startPos = spawnPoint.position;
        transform.position = startPos;

        Vector3 fireDir = spawnPoint.forward.normalized;
        targetPos = startPos + fireDir * maxDistance;
        
        //발사
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            Rotate();
            yield return null;
        }

        //회수
        while (Vector3.Distance(transform.position, spawnPoint.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                spawnPoint.position,
                speed * Time.deltaTime
            );

            Rotate();
            yield return null;
        }
    }

    //회전은 애니메이션 or 아니면 이거 쓰는 걸로
    void Rotate() 
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    //적에게 데미지
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy temp = other.GetComponent<Enemy>();
            
            temp.TakeDamage(weaponItemDamage);
        }
    }
}
