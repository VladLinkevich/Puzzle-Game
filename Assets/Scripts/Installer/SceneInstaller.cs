using Factory;
using Logic;
using RoundState;
using Zenject;

namespace Installer
{
  public class SceneInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IMapFactory>().To<MapFactory>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<CreateMap>().AsSingle().NonLazy();
      Container.Bind<SelectChip>().AsSingle().NonLazy();
      Container.Bind<SelectPoint>().AsSingle().NonLazy();
      Container.Bind<MovePointAnimation>().AsSingle().NonLazy();
      Container.Bind<PathFinder>().AsSingle().NonLazy();
    }
  }
}