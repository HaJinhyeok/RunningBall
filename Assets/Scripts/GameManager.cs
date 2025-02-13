using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Box;
    public GameObject Coin;
    public TMP_Text ScoreText;
    public Button RestartButton;

    public static UnityAction Scoring;

    private int _score;

    private Vector3[] _spawnPos = { new Vector3(-3f, 0.5f, 40f), new Vector3(0f, 0.5f, 40f), new Vector3(3f, 0.5f, 40f) };
    private Vector3[] _eulers = {
        new Vector3(15, 30, 45), new Vector3(15, 30, 0),
        new Vector3(15, 0, 45), new Vector3(0, 30, 45) };

    void Start()
    {
        StartCoroutine(SpawnObject());
        Scoring += GetScore;
        ScoreText.text = _score.ToString();
        RestartButton.onClick.AddListener(OnRestartButtonClick);
    }

    IEnumerator SpawnObject()
    {
        Vector3 euler = _eulers[Random.Range(0, _eulers.Length)];
        for (int i = 0; i < _spawnPos.Length; i++)
        {
            int randObj = Random.Range(0, 2);

            if (randObj == 0)
            {
                Instantiate(Box, _spawnPos[i], Quaternion.identity);
            }
            else
            {
                Instantiate(Coin, _spawnPos[i], Quaternion.Euler(euler));
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

    void OnRestartButtonClick()
    {
        _score = 0;
        ScoreText.text = _score.ToString();
        RestartButton.gameObject.SetActive(false);
        StopAllCoroutines();
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        for (int i = 0; i < boxes.Length; i++)
        {
            Destroy(boxes[i]);
        }
        for (int i = 0; i < coins.Length; i++)
        {
            Destroy(coins[i]);
        }
        Time.timeScale = 1f;
        Player.Restart();
        StartCoroutine(SpawnObject());
    }
}
