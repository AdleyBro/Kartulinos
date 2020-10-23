using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolderHud : MonoBehaviour
{
    private KartMovement2 player;
    private Image image;

    private void Start()
    {
        player = GetComponentInParent<KartMovement2>();
        image = transform.Find("Item").GetComponent<Image>();
        StartCoroutine(UpdateHoldingItem());
    }

    IEnumerator UpdateHoldingItem()
    {
        while (true) {
            image.sprite = null;
            image.color = Color.clear;
            yield return new WaitUntil(() => player.IsHoldingItem());
            image.sprite = Resources.Load<Sprite>(player.GetHoldingItem().name);
            image.color = Color.white;
            yield return new WaitUntil(() => !player.IsHoldingItem());
        }
    }

}
