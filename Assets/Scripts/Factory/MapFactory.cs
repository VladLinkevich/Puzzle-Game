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

    public void CreateTransition(Vector2 from, Vector2 to)
    {
      GameObject transition = (GameObject) Object.Instantiate(
        Resources.Load(ResourcePath.TransitionPrefab),
        _parent.transform);

      SetTransitionTransform(@from, to, transition);
    }

    private void SetTransitionTransform(Vector2 @from, Vector2 to, GameObject transition)
    {
      transition.transform.position = (@from + to) / 2;
      Vector3 currentScale = transition.transform.localScale;
      transition.transform.localScale =
        new Vector3(currentScale.x, Vector3.Distance(@from, to), currentScale.z);
      transition.transform.eulerAngles = new Vector3(0, 0, 
        Vector2.Angle(@from.x > to.x ? to - @from : @from - to, Vector2.up));
    }

    private float GetAngle(float x, float distance)
    {
      float radian = Mathf.Sin(x /distance);
      return radian * Mathf.Rad2Deg;
    }

    private void CreateParent()
    {
      _parent = (GameObject) Object.Instantiate(Resources.Load(ResourcePath.EmptyPrefab));
      _parent.name = ParentName;
    }
  }

  public interface IMapFactory
  {
    GameObject CreatePoint(Vector2 at);
    void CreateTransition(Vector2 from, Vector2 to);
  }
}