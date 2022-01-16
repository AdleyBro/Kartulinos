using kart_action;
using power_up;

public class Boost : Item
{
    public Boost() : base("UI/ItemHud/mushroom") { }

    public override PowerUpState GetPowerUpState(KartAction k)
    {
        return new BoostState(k);
    }
}
