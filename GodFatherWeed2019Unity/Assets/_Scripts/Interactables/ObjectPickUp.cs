using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickUp : MonoBehaviour
{

    private ObjectBase scriptObj;

    private void Start()
    {
        scriptObj = GetComponent<ObjectBase>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetItemInRange(scriptObj);

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player.GetItemInRange() == this)
                player.SetItemInRange(null);

        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            ObjectBase inRangePlayer = player.GetItemInRange();

                if (inRangePlayer != null)
                {
                    if (Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - inRangePlayer.transform.position)) > Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - transform.position)))
                    {
                        player.SetItemInRange(scriptObj);
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerController>().SetItemInRange(scriptObj);

                }
        }
    }
}
