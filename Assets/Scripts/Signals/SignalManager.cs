using Zenject;

public class SignalManager
{
    public readonly SignalBus _signalBus;

    public SignalManager(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }


}