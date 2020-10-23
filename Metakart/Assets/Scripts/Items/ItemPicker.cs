using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private readonly Item[] allItemsList = {
        new Boost(),
        new GreenShell()};

    public void Start()
    {
        GameObject obj = new GameObject();
        CoroutineController controller = obj.AddComponent<CoroutineController>();

        foreach (Item item in allItemsList)
        {
            item.coroutineController = controller;
        }
    }

    public Item PickRandomItem()
    {
        int index = Random.Range(0, allItemsList.Length);
        return allItemsList[index];
    }
}
