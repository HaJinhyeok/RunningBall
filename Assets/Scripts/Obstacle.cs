using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _speed = 5f;
    private Vector3 _currentEuler;

    private void Start()
    {
        _currentEuler = transform.rotation.eulerAngles;
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (CompareTag("Coin"))
        {
            transform.Rotate(_currentEuler * 5f * Time.deltaTime);
        }
        pos += Vector3.back * _speed * Time.deltaTime;
        transform.position = pos;
    }
}
