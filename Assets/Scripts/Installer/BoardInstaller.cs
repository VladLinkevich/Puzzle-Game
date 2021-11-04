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
      Container.Bind<ResourceLoader>().AsSingle();
      Container.Bind<MovePointAnimation>().AsSingle();
      Container.BindInterfacesAndSelfTo<SelectPoint>().AsSingle();
      Container.BindInterfacesAndSelfTo<SelectChip>().AsSingle();
      Container.BindInterfacesAndSelfTo<WinObserver>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
    }
  }
}