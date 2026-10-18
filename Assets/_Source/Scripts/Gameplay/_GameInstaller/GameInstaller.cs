using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private CellField _cellField;
    private PillarBar _pillarBar;
    private PillarSpawner _pillarSpawner;

    public override void InstallBindings()
    {
        
    }

    private void BindSceneComponents()
    {
        Container.Bind<CellField>().FromComponentInHierarchy(_cellField).AsSingle();
        Container.Bind<PillarBar>().FromComponentInHierarchy(_pillarBar).AsSingle();
        Container.Bind<PillarSpawner>().FromComponentInHierarchy(_pillarSpawner).AsSingle();
    }
}