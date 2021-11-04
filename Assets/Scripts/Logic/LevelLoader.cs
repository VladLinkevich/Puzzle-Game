using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic
{
  public class LevelLoader : MonoBehaviour
  {
    private const string GameSceneName = "Game";

    public void LoadLevel(string dataName)
    {
      PlayerPrefs.SetString(ResourcePath.MapDataPref, dataName);
      SceneManager.LoadScene(GameSceneName);
    }
  }
}