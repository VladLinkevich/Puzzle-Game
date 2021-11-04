using Chip;
using Point;
using UnityEngine;

namespace Factory
{
  public interface IBoardObjectFactory
  {
    GameObject CreateParent();
    PointFacade CreatePoint(Vector2 at);
    ChipFacade CreateChip(Vector3 at, int id);
    void CreateTransition(Vector2 from, Vector2 to);
  }
}