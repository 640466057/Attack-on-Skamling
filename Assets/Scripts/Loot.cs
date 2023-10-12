using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private bool shouldExist = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Tag tag) && tag.tags.Contains(Tag.Tags.Player) && shouldExist)
        {
            collision.gameObject.GetComponent<PlayerController>().ammunition++;
            shouldExist = false;
            Destroy(gameObject);
        }
    }
}
