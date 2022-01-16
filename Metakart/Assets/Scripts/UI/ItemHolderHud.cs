using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using kart_action;

public class ItemHolderHud : MonoBehaviour
{
    private KartAction player;
    private Image image;

    private void Start()
    {
        player = GetComponentInParent<KartAction>();
        image = transform.Find("Item").GetComponent<Image>();
        StartCoroutine(UpdateHoldingItem());
    }

    IEnumerator UpdateHoldingItem()
    {
        while (true) {
            image.sprite = null;
            image.color = Color.clear;
            yield return new WaitUntil(() => player.IsHoldingItem());
            image.sprite = Resources.Load<Sprite>(player.GetHoldingItem().GetIconImagePath());
            image.color = Color.white;
            yield return new WaitUntil(() => !player.IsHoldingItem());
        }
    }

}
