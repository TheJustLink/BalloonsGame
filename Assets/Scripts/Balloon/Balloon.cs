using UnityEngine;

[RequireComponent(typeof(Animator))]
class Balloon : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Blow()
    {
        _animator.SetTrigger(nameof(Blow));

        Destroy(gameObject, 1f);
    }
}