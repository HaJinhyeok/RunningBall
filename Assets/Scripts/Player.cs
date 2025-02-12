using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour
{
    private Vector2 _dragStartPos;
    private Rigidbody _rigidbody;

    private Vector3 _velocity = Vector3.one;

    // 움직이나?
    private bool _isMove;
    // 오른쪽인가?
    private bool _isRight;
    // 점프하나?
    private bool _isJump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _dragStartPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            float deltaX = endPos.x - _dragStartPos.x;
            float deltaY = endPos.y - _dragStartPos.y;
            Debug.Log($"deltaX : {deltaX}, deltaY : {deltaY}");
            _isJump = deltaY >= 100f;
            _isRight = deltaX > 0f;
            _isMove = Math.Abs(deltaX) >= 100f;
            if (_isJump)
            {
                Jump();
            }
            if (_isMove)
            {
                Move(_isRight);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            Debug.Log("Game Over");
        }
        else if(other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.Scoring();
            Debug.Log("Get Score");
            Debug.Log("Other Collider : " + other.name);
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * 300f);
    }

    void Move(bool isRight)
    {
        int flag = isRight ? 1 : -1;
        Vector3 dst = transform.position + new Vector3(flag * 3f, 0, 0);
        transform.position = Vector3.SmoothDamp(transform.position, dst, ref _velocity, 0.5f);
    }
}
