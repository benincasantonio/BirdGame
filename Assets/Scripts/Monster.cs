using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{

    [SerializeField]
    Sprite _deadSprite;
    [SerializeField]
    ParticleSystem _particleSystem;
    SpriteRenderer _spriteRenderer;
    bool _hasDied = false;

    public bool HasDied { get { return _hasDied; } }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(ShouldDieFromCollision(collision) && !_hasDied)
        {
            Die();
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        Debug.Log(collision.contacts[0].normal.y);

        return false;
    }

    void Die()
    {
        _hasDied = true;
        _spriteRenderer.sprite = _deadSprite;
        _particleSystem.Play();
        StartCoroutine(DestroyAfterSeconds());
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
