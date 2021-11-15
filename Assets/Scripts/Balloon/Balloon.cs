using System;

using UnityEngine;

using Lean.Touch;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
class Balloon : LeanSelectableByFingerBehaviour, IDamageable
{
    public event Action<Balloon> Blowed;

    private Animator _animator;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody;

    private bool _isBlowing;
    private float _gravityScale;

    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _gravityScale = _rigidbody.gravityScale;
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }
    public void ApplyDamage()
    {
        Blow();
    }

    protected override void OnSelected()
    {
        DisableGravity();

        transform.localScale = Vector3.one * 1.1f;
    }
    protected override void OnDeselected()
    {
        EnableGravity();

        transform.localScale = Vector3.one;
    }

    private void Blow()
    {
        if (_isBlowing) return;
        _isBlowing = true;

        StartBlowAnimation();
        DisableGravity();

        Blowed?.Invoke(this);
    }
    private void StartBlowAnimation()
    {
        _animator.SetTrigger(nameof(Blow));
    }

    private void DisableGravity()
    {
        _rigidbody.isKinematic = true;

        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = Vector2.zero;
    }
    private void EnableGravity()
    {
        _rigidbody.gravityScale = _gravityScale;
        _rigidbody.isKinematic = false;
    }
}