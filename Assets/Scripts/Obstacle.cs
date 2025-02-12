using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float _speed = 5f;

    private void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        //if (CompareTag("Coin"))
        //{
        //    transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        //}
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
