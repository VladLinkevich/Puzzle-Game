using Data;
using Factory;
using Logic;
using RoundState;
using UnityEngine;
using Zenject;

namespace Installer
{
  public class BoardInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IBoardObjectFactory>().To<BoardObjectFactory>().AsSingle();
      Container.Bind<BoardFactory>().AsSingle().NonLazy();
      Container.Bind<PathFinder>().AsSingle();
      Container.Bind<MovePointAnimation>().AsSingle();
      Container.Bind<SelectPoint>().AsSingle();
      Container.Bind<SelectChip>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
    }
  }
}