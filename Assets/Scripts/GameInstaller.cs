using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [Inject] private GameConfig _gameConfig;

    public override void InstallBindings()
    {
        BaseInstaller.Install(Container);

        //SignalBusInstaller.Install(Container);

        //Container.Bind<SignalManager>().AsSingle();
        //Container.Bind<SaveLoadScript>().AsSingle();
        //Container.Bind<SoundController>().AsSingle();
        //Container.Bind<TimeController>().AsSingle();
        //Container.Bind<ScoreController>().AsSingle();        
        //Container.BindFactory<PlayerController, PlayerController.PlayerFabrik>()            
        //    .FromComponentInNewPrefab(_gameConfig.PlayerPrefab).WithGameObjectName("Player");
        //Container.BindFactory<float, float, float, CollumnsController, CollumnsController.Factory>()
        //    .FromComponentInNewPrefab(_gameConfig.WallPrefab).WithGameObjectName("wall");
        //Container.DeclareSignal<GameEndSignal>();
        //Container.DeclareSignal<AudioChange>();
        //Container.DeclareSignal<ScoreSignal>();
        //Container.DeclareSignal<ResumeSignal>();

    }

}

public class BaseInstaller : Installer<BaseInstaller>
{
    [Inject] private GameConfig _gameConfig;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<SignalManager>().AsSingle();
        Container.Bind<SaveLoadScript>().AsSingle();
        Container.Bind<SoundController>().AsSingle();
        Container.Bind<TimeController>().AsSingle();
        Container.Bind<ScoreController>().AsSingle();
        Container.BindFactory<PlayerController, PlayerController.PlayerFabrik>()
            .FromComponentInNewPrefab(_gameConfig.PlayerPrefab).WithGameObjectName("Player");
        Container.BindFactory<float, float, float, CollumnsController, CollumnsController.Factory>()
            .FromComponentInNewPrefab(_gameConfig.WallPrefab).WithGameObjectName("wall");
        Container.DeclareSignal<GameEndSignal>();
        Container.DeclareSignal<AudioChange>();
        Container.DeclareSignal<ScoreSignal>();
        Container.DeclareSignal<ResumeSignal>();
    }
}


public class TestInstaller : Installer<BaseInstaller>
{
    [Inject] private GameConfig _gameConfig;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<SignalManager>().AsSingle();
        Container.Bind<SaveLoadScript>().AsSingle();
        Container.Bind<SoundController>().AsSingle();
        Container.Bind<TimeController>().AsSingle();
        Container.Bind<ScoreController>().AsSingle();
        Container.BindFactory<PlayerController, PlayerController.PlayerFabrik>();
        Container.BindFactory<float, float, float, CollumnsController, CollumnsController.Factory>();
        Container.DeclareSignal<GameEndSignal>();
        Container.DeclareSignal<AudioChange>();
        Container.DeclareSignal<ScoreSignal>();
        Container.DeclareSignal<ResumeSignal>();
    }
}