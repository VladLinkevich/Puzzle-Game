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

    public GameObject LoadPoint()
    {
      _point ??= (GameObject) Resources.Load(ResourcePath.PointPrefab);
      return _point;
    }

    public GameObject LoadTransition()
    {
      _transition ??= (GameObject) Resources.Load(ResourcePath.TransitionPrefab);
      return _transition;
    }

    public GameObject LoadEmpty()
    {
      _empty ??= (GameObject)Resources.Load(ResourcePath.EmptyPrefab);
      return _empty;
    }

    public MapData LoadMapData()
    {
      _mapData ??= Resources.Load<MapData>(ResourcePath.MapDataPath);
      return _mapData;
    }
  }
}