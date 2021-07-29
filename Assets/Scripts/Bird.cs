using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    Vector2 _startPosition;
    [SerializeField]
    float _launchForce = 500f;
    [SerializeField]
    float maxDragDistance = 5f; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPosition = _rigidbody.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.isKinematic = true;
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;    
    }

    void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;
        Vector2 currentPosition = transform.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _launchForce);
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(_startPosition, desiredPosition);

        if(distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody.position = desiredPosition;
    }

    void ResetPlayerPosition()
    {
        _rigidbody.position = _startPosition;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.rotation = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterSeconds());   
    }

    IEnumerator ResetAfterSeconds()
    {
        yield return new WaitForSeconds(3);
        ResetPlayerPosition();
    }
}
