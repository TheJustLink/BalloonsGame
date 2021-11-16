using System;

using System.Collections;

using UnityEngine;

class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private BalloonSpawnerData _data;

    public event Action<Balloon> Spawned;
    public bool IsSpawning => _coroutine != null;

    private Coroutine _coroutine;

    public void StartSpawning()
    {
        if (IsSpawning) return;

        _coroutine = StartCoroutine(Spawning());
    }
    public void StopSpawning()
    {
        if (IsSpawning == false) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator Spawning()
    {
        var cooldownTime = 1f / _data.SpawnRate;
        var acceleration = _data.SpawnRateAcceleration;

        while(true)
        {
            Spawn();

            yield return new WaitForSeconds(cooldownTime);

            if (cooldownTime > 0.25f)
                cooldownTime -= acceleration;
        }
    }
    private void Spawn()
    {
        var position = GetRandomPosition();
        var ballon = Instantiate(_data.BalloonPrefab, position, Quaternion.identity);

        Spawned?.Invoke(ballon);
    }

    private Vector2 GetRandomPosition()
{
        var spawnRange = _data.SpawnRange;
        var offset = UnityEngine.Random.Range(-spawnRange, spawnRange);
        var position = new Vector2(transform.position.x + offset, transform.position.y);

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        var spawnRange = _data.SpawnRange;
        var leftPoint = new Vector2(transform.position.x - spawnRange, transform.position.y);
        var rightPoint = new Vector2(transform.position.x + spawnRange, transform.position.y);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(leftPoint, rightPoint);
    }
}