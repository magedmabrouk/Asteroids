using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenWaves = 2, reviveTime = 3, timeToPauseAsteroids = 2, timeToContinueAsteroids = 4;
    [SerializeField] private int waveMax = 5, waveMin = 10, waveMaxIncrement = 2, waveMinIncrement = 1;

    private AsteroidsSpawner _asteroidsSpawner;
    private ShipManager _shipManager;
    private static GameManager _instance;
    public static GameManager Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _shipManager = ShipManager.Instance;
        _asteroidsSpawner = AsteroidsSpawner.Instance;
    }

    private void Start()
    {
        _asteroidsSpawner.SpawnWave(waveMax, waveMin);
        _shipManager.IncreaseScore(0);
        _shipManager.IncreaseLives(0);
    }

    public void NextWave()
    {
        Invoke(nameof(NextWaveAfterTime), timeBetweenWaves);
    }

    private void NextWaveAfterTime()
    {
        waveMax += waveMaxIncrement;
        waveMin += waveMinIncrement;
        _asteroidsSpawner.SpawnWave(waveMax, waveMin);
    }

    public void ShipExploded()
    {
        _shipManager.gameObject.SetActive(false);
        if (ShipManager.Instance.GetLives < 0)
        {
            GameOver();
            return;
        }
        Invoke(nameof(PauseAsteroids), timeToPauseAsteroids);
        Invoke(nameof(ContinueAsteroids), timeToContinueAsteroids);
        Invoke(nameof(ReviveShip), reviveTime);
    }

    private void PauseAsteroids()
    {
        _asteroidsSpawner.DisableAsteroids();
    }

    private void ContinueAsteroids()
    {
        _asteroidsSpawner.EnableAsteroids();
    }

    private void ReviveShip()
    {
        _shipManager.gameObject.SetActive(true);
        _shipManager.transform.position = Vector3.zero;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        UIManager.Instance.GameOver();
    }
}
