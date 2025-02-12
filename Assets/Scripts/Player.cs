using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour
{
    private Vector2 _dragStartPos;
    private Rigidbody _rigidbody;

    private bool _isDragging;
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
            _isDragging = true;
            _dragStartPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
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
        if(other.CompareTag("Coin"))
        {
            Debug.Log("Get Score");
            GameManager.Scoring();
            Destroy(other.gameObject);
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
        transform.position = Vector3.Lerp(transform.position, dst, 10f);
    }
}
