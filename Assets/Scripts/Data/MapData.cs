using UnityEngine;

namespace Data
{
  [CreateAssetMenu(fileName = "MapData", menuName = "Data/Map", order = 0)]
  public class MapData : ScriptableObject
  {
    public Vector2[] SpawnPoints;
    public Neighbor[] Neighbors;
    public int[] ChipSpawnPoints;
  }
}