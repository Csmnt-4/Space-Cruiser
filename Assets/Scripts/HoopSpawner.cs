using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoopSpawner : MonoBehaviour
{
    public GameObject hoopPrefab;
    public GameObject particlePrefab;
    public GameObject gameManager;

    public List<GameObject> hoops;
    public List<GameObject> particles;

    public float spawnRate = 0.5f;
    public float offsetRange = 10f;
    public float distanceOffsetMin = 30f;
    public float distanceOffsetMax = 130f;



    public float level = 1f;
    public float levelMax = 5f;
    public float levelMin = 1f;
    public float difficultyIncreaseRate = 1.05f;

    public float hoopSpeed = 40f;
    public float hoopRadius = 5.5f;
    public float hoopMaxCount = 5f;
    public float hoopLinesCount = 7f;

    private float lastSpawnTime;
    private float nextSpawnTime;

    public List<Vector3> positionsOld;
    public bool spawnerEnabled = true;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            positionsOld.Add(new Vector3(0, 0, distanceoffset));
        }
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime && spawnerEnabled)
        {
            SpawnHoop();
            lastSpawnTime = Time.time;
            nextSpawnTime = Time.time + 1f / spawnRate;

            if (hoops.Count > hoopMaxCount)
            {
                for (int i = hoopLinesCount; i >= 0; i--)
                {
                    Destroy(hoops.First().GetComponent<Hoop>().lines.ElementAt(i));
                }

                Destroy(hoops.First());
                hoops.Remove(hoops.First());
            }

            if (particles.Count > hoopMaxCount)
            {
                if (particles.First() == null)
                {
                    Destroy(particles.First());
                    particles.Remove(particles.First());
                }
                else if (particles.First().GetComponent<CollectableParticle>().transform.position.z < -100)
                {
                    Destroy(particles.First());
                    particles.Remove(particles.First());
                }
            }
        }
    }

    private void SpawnHoop()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-offsetRange, offsetRange), Random.Range(-offsetRange, offsetRange), distanceOffsetMin);
        List<Vector3> newPositions = GenerateCirclePoints(hoopRadius,
                                                       spawnPosition.x,
                                                       spawnPosition.y,
                                                       distanceOffsetMax,
                                                       Random.Range(0, 180));
        GameObject hoop = Instantiate(hoopPrefab, spawnPosition, Quaternion.identity);

        hoop.GetComponent<Hoop>().Initialize(hoopSpeed, 10, newPositions, positionsOld);
        hoops.Add(hoop);

        positionsOld.Clear();
        foreach (Vector3 pos in newPositions)
        {
            positionsOld.Add(new Vector3(pos.x, pos.y, 50));
        }

        spawnPosition.z = distanceOffsetMax;
        if (Random.Range(0, 10) > 3)
        {
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            particle.GetComponent<CollectableParticle>().Initialize(hoopSpeed, gameManager);
            particles.Add(particle);
        }
    }

    private static List<Vector3> GenerateCirclePoints(float radius, float offsetX, float offsetY, float z, float offsetDegrees)
    {
        float offsetRadians = MathF.PI * offsetDegrees / 180.0f;

        // Angle between each point
        float angleStep = 2 * MathF.PI / (hoopLinesCount + 1);

        var points = new List<Vector3>();

        for (int i = 0; i <= hoopLinesCount; i++)
        {
            float angle = i * angleStep + offsetRadians;
            float x = radius * MathF.Cos(angle);
            float y = radius * MathF.Sin(angle);

            points.Add(new Vector3(x + offsetX, y + offsetY, z));
        }

        return points;
    }

    public void IncreaseDifficulty()
    {
        if (level <= levelMax)
        {
            spawnRate *= difficultyIncreaseRate;
            hoopSpeed *= difficultyIncreaseRate;
            distanceOffsetMin *= difficultyIncreaseRate;
            distanceOffsetMax *= difficultyIncreaseRate;
            level++;
        }
    }

    public void DecreaseDifficulty()
    {
        if (level >= levelMin)
        {
            spawnRate /= difficultyIncreaseRate;
            hoopSpeed /= difficultyIncreaseRate;
            distanceOffsetMin /= difficultyIncreaseRate;
            distanceOffsetMax /= difficultyIncreaseRate;
            level--;
        }
    }
}