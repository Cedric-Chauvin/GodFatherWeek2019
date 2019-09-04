using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickUp : MonoBehaviour
{
    public Text pickUpText;
    public string pickupText_Text;

    private ObjectBase scriptObj;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        pickUpText.text = pickupText_Text;
        scriptObj = GetComponent<ObjectBase>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetItemInRange(scriptObj);

            pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player.GetItemInRange() == this)
                player.SetItemInRange(null);

            pickUpText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            ObjectBase inRangePlayer = player.GetItemInRange();

            if (inRangePlayer == scriptObj)
                pickUpText.gameObject.SetActive(true);
            else
            {
                if (inRangePlayer != null)
                {
                    if (Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - inRangePlayer.transform.position)) > Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - transform.position)))
                    {
                        player.SetItemInRange(scriptObj);
                        pickUpText.gameObject.SetActive(true);

                        inRangePlayer.GetComponent<ObjectPickUp>().pickUpText.gameObject.SetActive(false);
                    }
                    else
                        pickUpText.gameObject.SetActive(false);
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerController>().SetItemInRange(scriptObj);

                    pickUpText.gameObject.SetActive(true);
                }
            }
        }
    }

    public void RemoveItem()
    {
        pickUpText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
