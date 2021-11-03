using Chip;
using Point;
using UnityEngine;

namespace Factory
{
  public interface IMapFactory
  {
    PointFacade CreatePoint(Vector2 at);
    ChipFacade CreateChip(Vector3 at);
    void CreateTransition(Vector2 from, Vector2 to);
  }
}