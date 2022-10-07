using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.LowLevel;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [CanBeNull]
    [SerializeField] private GameObject originalPrefab;
    private Queue<GameObject> poolingQueue = new Queue<GameObject>();
    protected Queue<GameObject> PoolingQueue => poolingQueue;

    public T GetObjectFromPool<T>()
    {
        if (PoolingQueue.Count > 0)
        {
            return PoolingQueue.Dequeue().GetComponent<T>();
        }
        else
        {
            return GetNewInstance<T>();
        }
    }
    
    public GameObject GetObjectFromPool()
    {
        if (PoolingQueue.Count > 0)
        {
            return PoolingQueue.Dequeue().gameObject;
        }
        else
        {
            return GetNewInstance();
        }
    }

    public void PutObjectIntoPool(GameObject instance)
    {
        PoolingQueue.Enqueue(instance);
        instance.transform.SetParent(transform);
        instance.transform.localPosition = Vector3.zero;
        instance.SetActive(false);
    }

    private T GetNewInstance<T>()
    {
        return originalPrefab != null ? Instantiate(originalPrefab, transform).GetComponent<T>() : default;
    }
    
    private GameObject GetNewInstance()
    {
        return originalPrefab != null ? Instantiate(originalPrefab, transform).gameObject : default;
    }
}