using Chip;
using Data;
using Logic;
using Point;
using UnityEngine;

namespace Factory
{
  public class BoardObjectFactory : IBoardObjectFactory
  {
    private readonly ResourceLoader _loader;
    private GameObject _parent;

    public BoardObjectFactory(ResourceLoader loader)
    {
      _loader = loader;
    }
      
    public PointFacade CreatePoint(Vector2 at) => 
      ((GameObject) Object.Instantiate(
        _loader.LoadPoint(), 
        at,
        Quaternion.identity,
        _parent.transform)).GetComponent<PointFacade>();

    public ChipFacade CreateChip(Vector3 at, int id)
    {
      GameObject chip =
        (GameObject) Object.Instantiate(_loader.LoadChips()[id], at, Quaternion.identity, _parent.transform);

      ChipFacade facade = chip.GetComponent<ChipFacade>();
      facade.SetId(id);

      return facade;
    }

    public void CreateTransition(Vector2 from, Vector2 to)
    {
      GameObject transition = (GameObject) Object.Instantiate(
        _loader.LoadTransition(),
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

    public GameObject CreateParent() =>
      _parent = (GameObject) Object.Instantiate(_loader.LoadEmpty());
  }
}