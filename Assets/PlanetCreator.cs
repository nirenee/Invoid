using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class PlanetCreator : MonoBehaviour
{
    public ChunksDatabase database;
    public int totalChunks = 6;

    private List<GameObject> spawned = new();

    void Start() => GeneratePlanet();

   private void BakeNavMesh()
    {
        var surfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in surfaces)
            surface.BuildNavMesh();
    }

   private void GeneratePlanet()
    {
        foreach (var g in spawned) Destroy(g);
        spawned.Clear();

        var current = Instantiate(GetByType(ChunkType.Start), Vector3.zero, Quaternion.identity);
        spawned.Add(current);

        for (int i = 0; i < totalChunks - 2; i++)
        {
            var exitConnector = GetExitConnector(current);
            if (exitConnector == null)
            {
                Debug.LogError($"Iteración {i}: no hay conector North en {current?.name}");
                break;
            }

            var corridor = SpawnConnected(GetRandomCorridor(exitConnector), exitConnector);
            if (corridor == null)
            {
                Debug.LogError($"Iteración {i}: corridor null");
                break;
            }
            spawned.Add(corridor);

            var corridorExit = GetExitConnector(corridor);
            var nextChunk = SpawnConnected(GetRandomChunk(), corridorExit);
            if (nextChunk == null)
            {
                Debug.LogError($"Iteración {i}: nextChunk null");
                break;
            }
            spawned.Add(nextChunk);
            current = nextChunk;
        }

        var lastExit = GetExitConnector(current);
        if (lastExit != null)
        {
            var lastCorridor = SpawnConnected(GetRandomCorridor(lastExit), lastExit);
            if (lastCorridor != null)
            {
                spawned.Add(lastCorridor);
                var bossExit = GetExitConnector(lastCorridor);
                var final = SpawnConnected(GetByType(ChunkType.Spaceshipart), bossExit);
                if (final != null) spawned.Add(final);
            }
        }

        BakeNavMesh();
    }

    GameObject SpawnConnected(GameObject prefab, Transform exitConnector)
    {
        if (prefab == null || exitConnector == null) return null;
        var temp = Instantiate(prefab);
        var entry = GetEntryConnector(temp);
        if (entry == null) { Destroy(temp); return null; }
        temp.transform.position += exitConnector.position - entry.position;
        return temp;
    }

    GameObject GetByType(ChunkType type) =>
        database.Chunks.FirstOrDefault(e => e.type == type)?.prefab;

    Transform GetExitConnector(GameObject chunk) =>
        chunk?.GetComponentsInChildren<ChunkConnector>()
              .FirstOrDefault(c => c.Direction == Direction.North)?.transform;

    Transform GetEntryConnector(GameObject chunk) =>
        chunk?.GetComponentsInChildren<ChunkConnector>()
              .FirstOrDefault(c => c.Direction == Direction.South)?.transform;

    GameObject GetRandomChunk()
    {
        var pool = database.Chunks
            .Where(e => e.type != ChunkType.Start && e.type != ChunkType.Spaceshipart)
            .ToList();
        if (pool.Count == 0) { Debug.LogError("Pool vacío"); return null; }
        return PickWeighted(pool);
    }

    GameObject GetRandomCorridor(Transform exitConnector)
    {
        var exitHeight = exitConnector.GetComponent<ChunkConnector>()?.Height ?? Height.Ground;
        var pool = database.Corridors
            .Where(e => e.prefab.GetComponentInChildren<ChunkConnector>()?.Height == exitHeight)
            .ToList();
        return pool.Count > 0 ? PickWeighted(pool) : database.Corridors[0].prefab;
    }

    GameObject PickWeighted(List<ChunksDatabase.ChunkEntry> pool)
    {
        int total = pool.Sum(e => e.weight);
        int roll = Random.Range(0, total);
        int acc = 0;
        foreach (var e in pool)
        {
            acc += e.weight;
            if (roll < acc) return e.prefab;
        }
        return pool[^1].prefab;
    }
}