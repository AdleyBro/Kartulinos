using kart_action;
using power_up;

public class BananaPeel : Item
{
    public BananaPeel() : base("UI/ItemHud/bananapeel") { }

    public override PowerUpState GetPowerUpState(KartAction k)
    {
        return new BananaPeelState(k);
    }
}