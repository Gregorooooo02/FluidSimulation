using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleSpawner2D : MonoBehaviour
{
    [SerializeField] public int particleCount;

    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private Vector2 spawnCenter;
    [SerializeField] private Vector2 spawnSize;
    [SerializeField] private float jitterStrength;
    [SerializeField] private bool showSpawnBoundsGizmos;

    public ParticleSpawnData SpawnData() {
        ParticleSpawnData data = new ParticleSpawnData(particleCount);
        var rng = new Unity.Mathematics.Random(42);

        float2 s = spawnSize;
        int numX = Mathf.CeilToInt(Mathf.Sqrt(s.x / s.y * particleCount + (s.x - s.y) * (s.x - s.y) / (4 * s.y * s.y)) - (s.x - s.y) / (2 * s.y));
        int numY = Mathf.CeilToInt(particleCount / (float)numX);
        int i = 0;

        for (int y = 0; y < numY; y++) {
            for (int x = 0; x < numX; x++) {
                if (i >= particleCount) break;

                float tx = numX <= 1 ? 0.5f : x / (numX - 1.0f);
                float ty = numY <= 1 ? 0.5f : y / (numY - 1.0f);

                float angle = (float)rng.NextDouble() * math.PI * 2;
                
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Vector2 jitter = direction * jitterStrength * ((float)rng.NextDouble() - 0.5f);

                data.positions[i] = new Vector2((tx - 0.5f) * spawnSize.x, (ty - 0.5f) * spawnSize.y) + jitter + spawnCenter;
                data.velocities[i] = initialVelocity;
                i++;
            }
        }

        return data;
    }

    public struct ParticleSpawnData {
        public float2[] positions;
        public float2[] velocities;

        public ParticleSpawnData(int count) {
            positions = new float2[count];
            velocities = new float2[count];
        }
    }

    private void OnDrawGizmos() {
        if (showSpawnBoundsGizmos && !Application.isPlaying) {
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawWireCube(spawnCenter, Vector2.one * spawnSize);
        }    
    }
}
