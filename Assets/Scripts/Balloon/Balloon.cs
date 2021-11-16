using System;

using UnityEngine;

using Lean.Touch;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
class Balloon : LeanSelectableByFingerBehaviour, IDamageable
{
    public event Action<Balloon> Blowed;
    public event Action<Balloon> OutOfScreen;
    public event Action<Balloon> Destroying;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Camera _camera;

    private bool _isBlowing;
    private float _gravityScale;

    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;

        _gravityScale = _rigidbody.gravityScale;
    }
    private void OnDestroy()
    {
        Destroying?.Invoke(this);
    }

    private void FixedUpdate()
    {
        if (IsOutOfScreen())
            OnOutOfScreen();
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }
    public void ApplyDamage()
    {
        Blow();
    }

    public void Blow()
    {
        if (_isBlowing) return;
        _isBlowing = true;

        StartBlowAnimation();
        DisablePhysics();

        Blowed?.Invoke(this);
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

    private void StartBlowAnimation()
    {
        _animator.SetTrigger(nameof(Blow));
    }

    private void DisablePhysics()
    {
        _rigidbody.simulated = false;
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

    private void OnOutOfScreen()
    {
        OutOfScreen?.Invoke(this);

        Destroy();
    }
    private bool IsOutOfScreen()
    {
        var position = (Vector2)transform.position;
        var cameraUpPoint = _camera.ViewportToWorldPoint(Vector2.one);
        var isOut = position.y > cameraUpPoint.y;

        return isOut;
    }
}