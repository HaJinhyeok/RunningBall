using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject Box;
    public GameObject Coin;
    public TMP_Text ScoreText;
    public static UnityAction Scoring;

    private int _score;

    Vector3[] _spawnPos = { new Vector3(-3f, 0.5f, 40f), new Vector3(0f, 0.5f, 40f), new Vector3(3f, 0.5f, 40f) };

    void Start()
    {
        StartCoroutine(SpawnObject());
        Scoring += GetScore;
        ScoreText.text = _score.ToString();
    }

    IEnumerator SpawnObject()
    {
        for (int i = 0; i < _spawnPos.Length; i++)
        {
            int randObj = Random.Range(0, 2);

            if (randObj == 0)
            {
                Instantiate(Box, _spawnPos[i], Quaternion.identity);
            }
            else
            {
                Instantiate(Coin, _spawnPos[i], Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(SpawnObject());
    }

    void GetScore()
    {
        _score += 10;
        ScoreText.text = _score.ToString();
    }
}
