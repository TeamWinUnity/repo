using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [Serializable]
    public class GameState
    {
        public const string SaveFilename = "save.sv";
        public int BestScore { get; set; }
    }

    public class GameController : MonoBehaviour
    {
        public GameObject hazard;
        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;

        public Text scoreText;
        public Text startText;
        public Text gameOverText;

        private bool GameOver { get; set; }
        private bool Restart { get; set; }
        public bool Started { get; set; }
        private int score;
        private int currentBest;

        void Start()
        {
            score = 0;
            GameOver = false;
            Started = false;
            Restart = false;

            Load();
            scoreText.text = "your best: " + currentBest;
            StartCoroutine(SpawnWaves());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Restart) SceneManager.LoadScene("Main");
                else if (!Started)
                {
                    Started = true;
                    startText.enabled = false;
                }
            }
        }

        IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (!GameOver)
            {
                if (Started)
                    for (int i = 0; i < hazardCount; i++)
                    {
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRoatation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRoatation);
                        if (GameOver) break;
                        yield return new WaitForSeconds(waveWait);
                    }
                else yield return new WaitForSeconds(startWait);
            }
            startText.enabled = true;
            startText.text = "press 'space' to restart";
            Restart = true;
        }

        public void AddScore(int value)
        {
            score += value;
            UpdateScore();
        }

        void UpdateScore()
        {
            scoreText.text = "score: " + score;
        }

        public void EndGame()
        {
            gameOverText.text = "game over\nyou scored:\n" + score + (currentBest < score ? "\nnew best" : "");
            gameOverText.enabled = true;
            GameOver = true;
            Save();
        }

        public void Load()
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, GameState.SaveFilename)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Path.Combine(Application.persistentDataPath, GameState.SaveFilename),
                    FileMode.Open);
                currentBest = ((GameState) bf.Deserialize(file)).BestScore;
                file.Close();
            }
            else currentBest = 0;
        }

        public void Save()
        {
            if (currentBest < score)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Path.Combine(Application.persistentDataPath, GameState.SaveFilename));
                bf.Serialize(file, new GameState() { BestScore = score });
                file.Close();
            }
        }
    }
}
