using System.Collections;
using UnityEngine;

public class ItemBoxAction : MonoBehaviour
{
    private Vector3 upV;
    private Vector3 rightV;
    private Vector3 forwardV;
    private ItemPicker itemPicker;
    private bool ready = true;
    private float position = 0f;
    private float speed = 1f;
    private float sign = 1f;

    private void Start()
    {
        upV = transform.up;
        rightV = transform.right;
        forwardV = transform.forward;
        itemPicker = GetComponentInParent<ItemPicker>();
    }

    void Update()
    {
        if (position > 0.45f)
            sign = -1f;
        if (position < -0.45f)
            sign = 1f;

        position += sign * speed * Time.deltaTime;
        transform.position += upV * speed * sign * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(60f * Time.deltaTime, upV) * Quaternion.AngleAxis(20f * Time.deltaTime, rightV) * Quaternion.AngleAxis(30f * Time.deltaTime, forwardV);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (ready && collider.tag == "Player")
        {
            StartCoroutine(DisappearAndReload());
            KartMovement2 player = collider.GetComponent<KartMovement2>();
            if (!player.IsHoldingItem()) {
                Item item = itemPicker.PickRandomItem();
                collider.gameObject.GetComponent<KartMovement2>().SetHoldingItem(item);
            }
        }
    }

    IEnumerator DisappearAndReload()
    {
        GetComponent<MeshRenderer>().enabled = false;
        ready = false;
        yield return new WaitForSeconds(2f);
        GetComponent<MeshRenderer>().enabled = true;
        ready = true;
    }
}
