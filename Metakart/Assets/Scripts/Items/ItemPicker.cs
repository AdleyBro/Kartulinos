using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private readonly Item[] allItemsList = {
        new Boost(),
        new GreenShell(),
        new BananaPeel()};

    public Item PickRandomItem()
    {
        int index = Random.Range(0, allItemsList.Length);
        return allItemsList[index];
    }
}
