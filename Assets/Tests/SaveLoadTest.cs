using Zenject;
using NUnit.Framework;

[TestFixture]
public class SaveLoadTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstaller()
    {
        Container.Bind<SaveLoadScript>().AsSingle();
        Container.Bind<ScoreController>().AsSingle();
        Container.Bind<SoundController>().AsSingle();
    }

    [Test]
    public void SavePathCreating()
    {
        var SaveLoad = Container.Resolve<SaveLoadScript>();
        Assert.IsNotNull(SaveLoad.GetPath());        
    }

    [Test]
    public void TestSoundSaveLoad()
    {
        var SoundController = Container.Resolve<SoundController>();
        var SaveLoad = Container.Resolve<SaveLoadScript>();

        SoundController.SoundSetActive(false);
        SaveLoad.SaveGame();
        SoundController.SoundSetActive(true);
        SaveLoad.LoadGame();
        Assert.AreEqual(false, SoundController.SoundCheck());

    }
}