using Data;
using RoundState;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class GameBootstrapper : IInitializable
  {
    private const string GameParentName = "Game";
    private const string ExampleParentName = "Example";
    private Vector3 ExampleTransformPosition = new Vector3(-7f, -3f, 0f);
    private Vector3 ExampleTransformLocalScale = new Vector3(0.5f, 0.5f, 0.5f);
    
    private readonly BoardFactory _factory;
    private readonly SelectChip _selectChip;
    private readonly SelectPoint _selectPoint;

    public GameBootstrapper(BoardFactory factory, SelectChip selectChip, SelectPoint selectPoint)
    {
      _factory = factory;
      _selectChip = selectChip;
      _selectPoint = selectPoint;
    }

    public void Initialize()
    {
      MapData mapData = Resources.Load<MapData>(ResourcePath.MapDataPath);

      BoardData boardData = CreatePlayBoard(mapData);
      CreateExampleBoard(mapData);
      
      _selectChip.Construct(boardData);
      _selectPoint.Construct(boardData);
    }

    private BoardData CreatePlayBoard(MapData mapData)
    {
      BoardData boardData = _factory.CreateBoard(new CreateBoardData
      {
        ChipSpawnPoints = mapData.ChipSpawnPoints,
        Neighbors = mapData.Neighbors,
        SpawnPoints = mapData.SpawnPoints
      });

      boardData.Parent.name = GameParentName;
      
      return boardData;
    }

    private void CreateExampleBoard(MapData mapData)
    {
      BoardData boardData = _factory.CreateBoard(new CreateBoardData
      {
        ChipSpawnPoints = mapData.ChipEndPoints,
        Neighbors = mapData.Neighbors,
        SpawnPoints = mapData.SpawnPoints
      });

      boardData.Parent.name = ExampleParentName;
      boardData.Parent.transform.position = ExampleTransformPosition;
      boardData.Parent.transform.localScale = ExampleTransformLocalScale;
    }
  }
}