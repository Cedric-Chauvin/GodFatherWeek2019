﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpening : MonoBehaviour
{
    public static ChestOpening Instance;

    private bool inRange;
    private float startTime = 0f;
    private float timerheld = 0f;
    private int rng;
    private bool held = false;

    [Range(1, 10)]
    public int minTime = 4;
    [Range(1, 10)]
    public int maxTime = 8;

    public GameObject victoryui;
    public GameObject gameui;

    public Animator animator;

    private void Awake()
    {
        Instance = this;

        System.Random random = new System.Random();
        rng = random.Next(minTime, maxTime);
        Debug.Log(rng);
    }
    private void OnTriggerExit()
    {
        held = false;

        timerheld = 0;
        startTime = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        animator = other.gameObject.GetComponentInChildren<Animator>();
        int playerNumber = other.gameObject.GetComponent<PlayerController>().playerNumber;

        if (Input.GetAxis("P" + playerNumber + "_Action_Axis") == -1f && other.gameObject.tag.Equals("Player"))
        {
            startTime = Time.time;
            timerheld = Time.time;
        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetAxis("P" + playerNumber + "_Action_Axis") == -1f && held == false)
        {
            timerheld += Time.deltaTime;
            Debug.Log(timerheld);

            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timerheld > (startTime + rng))
            {
                held = true;
                ButtonHeld();
            }
        }
    }

    // Method called after held for required time
    private void ButtonHeld()
    {
        Debug.Log("Held for " + rng + " seconds");

        Victory();
    }

    public void Victory()
    {
        Debug.Log("Victory!");

        animator.SetBool("Win", true); // VICTORY ANIMATION

        gameui.SetActive(false);
        victoryui.SetActive(true);
    }
}
