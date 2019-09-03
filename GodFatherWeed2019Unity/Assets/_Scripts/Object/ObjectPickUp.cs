﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickUp : MonoBehaviour
{
    public Text pickUpText;
    public string pickupText_Text;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        pickUpText.text = pickupText_Text;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetItemInRange(this);

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

            if (player.GetItemInRange() == this)
                pickUpText.gameObject.SetActive(true);
            else
            {
                ObjectPickUp other = player.GetItemInRange();
                if (Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - other.transform.position)) > Mathf.Abs(Vector3.SqrMagnitude(player.transform.position - transform.position)))
                {
                    player.SetItemInRange(this);
                    pickUpText.gameObject.SetActive(true);

                    other.pickUpText.gameObject.SetActive(false);
                }
                else
                    pickUpText.gameObject.SetActive(false);
            }
        }
    }

    public void RemoveItem()
    {
        pickUpText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
