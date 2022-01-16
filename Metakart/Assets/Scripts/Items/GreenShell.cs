using kart_action;
using power_up;

public class GreenShell : Item
{
    public GreenShell() : base("UI/ItemHud/greenshell") { }

    public override PowerUpState GetPowerUpState(KartAction k)
    {
        return new GreenShellState(k);
    }
}
