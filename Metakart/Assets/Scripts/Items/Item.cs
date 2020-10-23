using UnityEngine;

public abstract class Item
{
    public CoroutineController coroutineController;
    public string name;

    public Item(string _name)
    {
        name = _name;
    }

    public void SetCoroutineController(CoroutineController controller)
    {
        coroutineController = controller;
    }

    public abstract void Utilize(KartMovement2 player);
}
