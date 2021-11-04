using System.Collections;
using Logic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
  public class WinPanel : MonoBehaviour
  {
    private const string MenuSceneName = "Menu";
    public Button RestartButton;
    public Button MenuButton;
    
    public CanvasGroup Group;
    public float ShowDuration;

    private WinObserver _winObserver;
    
    [Inject]
    private void Construct(WinObserver winObserver)
    {
      _winObserver = winObserver;

      _winObserver.Win += Show;
    }
    
    public void Awake()
    {
      RestartButton.onClick.AddListener(RestartGame);
      MenuButton.onClick.AddListener(LoadMenu);
    }

    private void Show()
    {
      _winObserver.Win -= Show;
      gameObject.SetActive(true);
      Group.alpha = 0;
      StartCoroutine(ShowAnimation());
    }

    private IEnumerator ShowAnimation()
    {
      float duration = 0;
      while (ShowDuration > duration)
      {
        duration += Time.deltaTime;
        Group.alpha = Mathf.Clamp(duration / ShowDuration, 0, 1);
        yield return null;
      }
    }

    private void RestartGame() => 
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void LoadMenu() => 
      SceneManager.LoadScene(MenuSceneName);
  }
}