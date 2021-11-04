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
    private readonly IBoardDataReader[] _boardDataReaders;
    private readonly ResourceLoader _loader;

    public GameBootstrapper(BoardFactory factory, IBoardDataReader[] boardDataReaders, ResourceLoader loader)
    {
      _factory = factory;
      _boardDataReaders = boardDataReaders;
      _loader = loader;
    }

    public void Initialize()
    {
      MapData mapData = _loader.LoadMapData();

      BoardData boardData = CreatePlayBoard(mapData);
      CreateExampleBoard(mapData);

      foreach (IBoardDataReader reader in _boardDataReaders) 
        reader.Construct(boardData);
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