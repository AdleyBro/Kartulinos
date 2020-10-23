using System.Collections;
using UnityEngine;

public class GreenShell : Item
{
    public GreenShell() : base("UI/ItemHud/greenshell") { }

    public override void Utilize(KartMovement2 player)
    {
        GameObject shell = Resources.Load<GameObject>("GreenShell");
        player.SpawnItem(shell);
    }
}
