using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
class Balloon : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    private bool _isBlowing;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }
    public void Blow()
    {
        if (_isBlowing) return;
        _isBlowing = true;

        StartBlowAnimation();
        PlayBlowSound();
    }

    private void StartBlowAnimation()
    {
        _animator.SetTrigger(nameof(Blow));
    }
    private void PlayBlowSound()
    {
        _audioSource.Play();
    }
}