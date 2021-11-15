using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
class Balloon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {

    }

    public void ChangeSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }
    public void Blow()
    {
        Destroy(gameObject);
    }
}