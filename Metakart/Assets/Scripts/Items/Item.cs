using kart_action;
using power_up;

public abstract class Item
{
    private readonly string iconImagePath;

    public Item(string iconImagePath)
    {
        this.iconImagePath = iconImagePath;
    }

    public abstract PowerUpState GetPowerUpState(KartAction k);

    public string GetIconImagePath()
    {
        return iconImagePath;
    }
}
