using System.Collections;
using UnityEngine;

public class Boost : Item
{
    public Boost() : base("UI/ItemHud/mushroom") { }

    public override void Utilize(KartMovement2 player)
    {
        coroutineController.StartCoroutine(AddBoost(player));
    }

    IEnumerator AddBoost(KartMovement2 player)
    {
        player.SetIsBoosting(true);
        yield return new WaitForSeconds(2f);
        player.SetIsBoosting(false);
    }
}
