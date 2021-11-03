using Chip;
using Data;
using Point;
using UnityEngine;

namespace Factory
{
  public class MapFactory : IMapFactory
  {
    private const string ParentName = "Map";

    private Object[] _chips;
    private GameObject _parent;
    private int _chipId;

    public MapFactory()
    {
      CreateParent();
      LoadChips();
    }

    public PointFacade CreatePoint(Vector2 at) => 
      ((GameObject) Object.Instantiate(
        Resources.Load(ResourcePath.PointPrefab), 
        at,
        Quaternion.identity,
        _parent.transform)).GetComponent<PointFacade>();

    public ChipFacade CreateChip(Vector3 at)
    {
      GameObject chip =
        (GameObject) Object.Instantiate(_chips[_chipId], at, Quaternion.identity, _parent.transform);

      ChipFacade facade = chip.GetComponent<ChipFacade>();
      facade.SetId(_chipId);
      _chipId++;

      return facade;
    }

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

    private void CreateParent()
    {
      _parent = (GameObject) Object.Instantiate(Resources.Load(ResourcePath.EmptyPrefab));
      _parent.name = ParentName;
    }

    private void LoadChips() => 
      _chips = Resources.LoadAll(ResourcePath.ChipsPrefab);
  }
}