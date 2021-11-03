using Data;
using UnityEngine;

namespace Factory
{
  public class MapFactory : IMapFactory
  {
    private const string ParentName = "Map";

    private GameObject _parent;

    public MapFactory()
    {
      CreateParent();
    }

    public GameObject CreatePoint(Vector2 at) => 
      (GameObject) Object.Instantiate(
        Resources.Load(ResourcePath.PointPrefab), 
        at,
        Quaternion.identity,
        _parent.transform);

    private void CreateParent()
    {
      _parent = (GameObject) Object.Instantiate(Resources.Load(ResourcePath.EmptyPrefab));
      _parent.name = ParentName;
    }
  }

  public interface IMapFactory
  {
    GameObject CreatePoint(Vector2 at);
  }
}