using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetCreator : MonoBehaviour
{
    public ChunksDatabase database;
    public int totalChunks = 6;

    private List<GameObject> spawned = new();
    private void Start()
    {
        GeneratePlanet();
    }
    private void GeneratePlanet()
    {
        foreach (var g in spawned) Destroy(g);
        spawned.Clear();

        var current = SpawnChunk(GetByType(ChunkType.Start), Vector3.zero, Quaternion.identity);
        spawned.Add(current);

        foreach (var entry in database.Chunks)
        {
            var north = entry.prefab.GetComponentsInChildren<ChunkConnector>()
                            .FirstOrDefault(c => c.Direction == Direction.North);
            var south = entry.prefab.GetComponentsInChildren<ChunkConnector>()
                            .FirstOrDefault(c => c.Direction == Direction.South);

            Debug.Log($"{entry.prefab.name} → North: {north != null} / South: {south != null}");
        }

        for (int i = 0; i < totalChunks - 2; i++)
        {
            var exitConnector = GetExitConnector(current);
            if (exitConnector == null)
            {
                break;
            }

            var corridor = SpawnConnected(GetRandomCorridor(exitConnector), exitConnector);
            spawned.Add(corridor);

            var corridorExit = GetExitConnector(corridor);
            var nextChunk = SpawnConnected(GetRandomChunk(), corridorExit);
            spawned.Add(nextChunk);
            current = nextChunk;
        }

        GameObject SpawnConnected(GameObject prefab, Transform exitConnector)
        {
            if (prefab == null || exitConnector == null) return null;
            var temp = Instantiate(prefab);
            var entry = GetEntryConnector(temp);
            if (entry == null) { Destroy(temp); return null; }

            Vector3 offset = exitConnector.position - entry.position;
            temp.transform.position += offset;
            return temp; 
        }

        GameObject SpawnChunk(GameObject prefab, Vector3 pos, Quaternion rot) => Instantiate(prefab, pos, rot);
        GameObject GetByType(ChunkType type) => database.Chunks.FirstOrDefault(e => e.type == type)?.prefab;
        Transform GetExitConnector(GameObject chunk) => chunk?.GetComponentsInChildren<ChunkConnector>().FirstOrDefault(c => c.Direction == Direction.North)?.transform;
        Transform GetEntryConnector(GameObject chunk) => chunk?.GetComponentsInChildren<ChunkConnector>().FirstOrDefault(c => c.Direction == Direction.South)?.transform;
        GameObject PickWeighted(List<ChunksDatabase.ChunkEntry> pool)
        {
            if (pool.Count == 0) return null;
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
        GameObject GetRandomChunk()
        {
            var pool = database.Chunks.Where(e => e.type != ChunkType.Start && e.type != ChunkType.Spaceshipart).ToList();
            return PickWeighted(pool);
        }
        GameObject GetRandomCorridor(Transform exitConnector)
        {

            var exitHeight = exitConnector.GetComponent<ChunkConnector>()?.Height
                             ?? Height.Ground;
            var pool = database.Corridors.Where(e =>
            {
                var entry = e.prefab.GetComponentInChildren<ChunkConnector>();
                return entry != null && entry.Height == exitHeight;
            }).ToList();

            return pool.Count > 0 ? PickWeighted(pool) : database.Corridors[0].prefab;
        }
        }
}
