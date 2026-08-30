using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SceneSingleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    protected override void Awake()
    {
        base.Awake();
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                // 풀로 되돌릴 때 자동으로 꺼질 수 있도록
                var poolable = obj.GetComponent<IPoolable>();
                poolable?.SetOriginTag(pool.tag);

                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectQueue);
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn’t exist!");
            return null;
        }

        var queue = poolDictionary[tag];
        GameObject availableObject = null;

        // 비활성 오브젝트만 탐색
        int count = queue.Count;
        for (int i = 0; i < count; i++)
        {
            var obj = queue.Dequeue();

            if (!obj.activeInHierarchy && availableObject == null)
                availableObject = obj;

            queue.Enqueue(obj);
        }

        if (availableObject == null)
        {
            // 모든 오브젝트 활성화된 경우 스폰 중단
            Debug.LogWarning($"No available objects left in pool [{tag}]!");
            return null;
        }

        // 비활성 오브젝트만 활성화 및 재활용
        availableObject.transform.SetPositionAndRotation(position, rotation);
        availableObject.SetActive(true);

        var poolable = availableObject.GetComponent<IPoolable>();
        poolable?.OnSpawn(); // 스폰 시 이벤트 추가

        return availableObject;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        if (obj == null) return;

        // 🟩 despawn 처리 추가
        var poolable = obj.GetComponent<IPoolable>();
        poolable?.OnDespawn();

        obj.SetActive(false);
    }
    
    //해당 태그가 전체 활성화 되었는지 확인
    public bool IsTagFullyActive(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn’t exist!");
            return false;
        }

        foreach (var obj in poolDictionary[tag])
        {
            if (!obj.activeSelf)
            {
                return false; // 아직 비활성화된 객체가 있다면 전부 활성화된 상태 아님
            }
        }

        return true; // 모든 객체가 활성화됨
    }
    
    /*public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn’t exist!");
            return null;
        }

        var queue = poolDictionary[tag];
        GameObject obj = queue.Dequeue();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        queue.Enqueue(obj);
        return obj;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        obj.SetActive(false);
        if (poolDictionary.TryGetValue(tag, out var queue))
        {
            queue.Enqueue(obj);
        }
    }*/
}
