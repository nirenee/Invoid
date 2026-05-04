using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet/ChunkDatabase")]

public class ChunksDatabase : ScriptableObject
{
    public List<ChunkEntry> Chunks;
    public List<ChunkEntry> Corridors;
    [System.Serializable]
    public class ChunkEntry
    {
        public GameObject prefab;
        public ChunkType type;
        [Range(1, 10)] public int weight =1;
    }
   
}
public enum ChunkType { Start, Spaceshipart, EnemyArena, Parkour, Corridor }