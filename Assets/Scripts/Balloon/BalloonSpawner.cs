using System.Collections;

using UnityEngine;

class BalloonSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Balloon _prefab;
    [Header("Parameters")]
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnRange;

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
        var cooldownTime = 1f / _spawnRate;
        var cooldown = new WaitForSeconds(cooldownTime);

        while(true)
        {
            Spawn();

            yield return cooldown;
        }
    }
    private void Spawn()
    {
        var position = GetRandomPosition();

        Instantiate(_prefab, position, Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        var offset = Random.Range(-_spawnRange, _spawnRange);
        var position = new Vector2(transform.position.x + offset, transform.position.y);

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        var leftPoint = new Vector2(transform.position.x - _spawnRange, transform.position.y);
        var rightPoint = new Vector2(transform.position.x + _spawnRange, transform.position.y);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(leftPoint, rightPoint);
    }
}