using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerObject<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private float _minDelayOnSpawn;
    [SerializeField] private float _maxDelayOnSpawn;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private bool _globalSpawn;

    private ObjectPool<T> _objectPool;
    private List<T> _activeObjects;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _activeObjects = new List<T>();

        _objectPool = new ObjectPool<T>
        (
            createFunc: () => Create(GetRandomPosition()),
            actionOnGet: (spawnObject) => OnGet(spawnObject),
            actionOnRelease: (spawnObject) => OnRelease(spawnObject),
            actionOnDestroy: (spawnObject) => Delete(spawnObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnWithDelay());
    }

    public void GetObjectFromPool(Transform transform, Quaternion quaternion)
    {
        T objectFromPool = _objectPool.Get();
        objectFromPool.gameObject.transform.position = transform.position;
        objectFromPool.gameObject.transform.rotation = quaternion;
    }

    public virtual void OnGet(T spawnObject)
    {
        spawnObject.gameObject.SetActive(true);
        _activeObjects.Add(spawnObject);
    }

    public virtual void OnRelease(T spawnObject)
    {
        spawnObject.gameObject.SetActive(false);
        _activeObjects.Remove(spawnObject);
    }

    public virtual T Create(Vector2 vector2)
    {
        T spawnObject = Instantiate(_prefab, vector2, transform.rotation);
        return spawnObject;
    }

    public virtual void Delete(T spawnObject)
    {
        Destroy(spawnObject.gameObject);
    }

    public virtual void Release(T spawnObject)
    {
        _objectPool.Release(spawnObject);
    }

    public virtual void Reset()
    {
        _objectPool.Clear();

        foreach (T obj in _activeObjects)
        {
            Destroy(obj.gameObject);
        }

        _activeObjects.Clear();
    }

    protected Vector2 GetRandomPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    private IEnumerator SpawnWithDelay()
    {
        while (true)
        {
            float delayOnSpawn = Random.Range(_minDelayOnSpawn, _maxDelayOnSpawn);
            WaitForSeconds waitOnSpawn = new WaitForSeconds(delayOnSpawn);

            if (_activeObjects.Count < _poolMaxSize)
                _objectPool.Get();

            yield return waitOnSpawn;
        }
    }
}
