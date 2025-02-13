using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Button _restartButton;

    public static UnityAction Restart;

    private Vector2 _dragStartPos;
    private Rigidbody _rigidbody;

    private Vector3 _startPosition = new Vector3(0f, 1f, -5f);
    private Vector3[] _position = { new Vector3(-3f, 0.5f, -5f), new Vector3(0, 0.5f, -5f), new Vector3(3f, 0.5f, -5f) };
    private int currentIdx = 1;

    // 움직이나?
    private bool _isMove;
    private bool _isMoving;
    // 오른쪽인가?
    private bool _isRight;
    // 점프하나?
    private bool _isJump;
    private bool _isJumping = false;
    /* 점프 시 y좌표 직접 계산용 상수 및 시간값 */
    private float _timeValue = 0f;
    private const float _startSpeed = 6f;
    private readonly float _gravity = Physics.gravity.y;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Restart += PlayerRestart;
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
            _isJump = deltaY >= 100f;
            _isRight = deltaX > 0f;
            _isMove = Math.Abs(deltaX) >= 100f;
            if (_isJump && !_isJumping)
            {
                // Jump();
                _isJumping = true;
            }
            if (_isMove)
            {
                _isMoving = true;
                if (_isRight && currentIdx < 2)
                    currentIdx++;
                else if (!_isRight && currentIdx > 0)
                    currentIdx--;
            }
        }
    }

    private void LateUpdate()
    {
        if(_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _position[currentIdx], 3f * Time.deltaTime);
            if (transform.position == _position[currentIdx])
                _isMoving = false;
        }
        if(_isJumping)
        {
            _timeValue += Time.deltaTime;
            float currentY = 0.5f + _startSpeed * _timeValue + _gravity * _timeValue * _timeValue / 2;
            Vector3 pos = transform.position;
            pos.y = currentY;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            Time.timeScale = 0f;
            _restartButton.gameObject.SetActive(true);
        }
        else if(other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.Scoring();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && _isJumping)
        {
            _isJumping = false;
            _timeValue = 0f;
        }            
    }

    void PlayerRestart()
    {
        transform.position = _startPosition;
        currentIdx = 1;
        _isJumping = false;
        _isJump = false;
        _isMove = false;
        _isMoving = false;
    }
}
