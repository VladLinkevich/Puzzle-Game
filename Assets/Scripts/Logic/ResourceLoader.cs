using Data;
using UnityEngine;

namespace Logic
{
  public class ResourceLoader
  {
    private Object[] _chips;
    private GameObject _point;
    private GameObject _transition;
    private GameObject _empty;
    private MapData _mapData;

    public Object[] LoadChips() => 
      _chips ??= Resources.LoadAll(ResourcePath.ChipsPrefab);

    public GameObject LoadPoint() => 
      _point ??= (GameObject) Resources.Load(ResourcePath.PointPrefab);

    public GameObject LoadTransition() => 
      _transition ??= (GameObject) Resources.Load(ResourcePath.TransitionPrefab);

    public GameObject LoadEmpty() => 
      _empty ??= (GameObject)Resources.Load(ResourcePath.EmptyPrefab);

    public MapData LoadMapData() => 
      _mapData ??= Resources.Load<MapData>(PlayerPrefs.GetString(ResourcePath.MapDataPref));
  }
}