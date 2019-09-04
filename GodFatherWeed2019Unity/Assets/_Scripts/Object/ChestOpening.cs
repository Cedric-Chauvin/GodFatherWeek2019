using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpening : MonoBehaviour
{
    public Text Text;
    private bool inRange;
    private float startTime = 0f;
    private float timerheld = 0f;
    private int rng;
    [SerializeField] public string key;
    private bool held = false;

    // Start is called before the first frame update
    void Start()
    {
        GenRng();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(key) && other.gameObject.tag.Equals("Player"))
        {
            startTime = Time.time;
            timerheld = startTime;
        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetKey(key) && held == false)
        {
            timerheld += Time.deltaTime;
            Text.gameObject.SetActive(true);
            Text.text = (timerheld - startTime).ToString();

            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timerheld > (startTime + rng))
            {
                held = true;
                ButtonHeld();
            }
        }

        // For single effects. Remove if not needed
        if (Input.GetKeyUp(key))
        {
            held = false;
            timerheld = 0;
            startTime = 0;
        }
    }

	// Method called after held for required time
	private void ButtonHeld()
    {
        Debug.Log("held for " + rng + " seconds");
        timerheld = startTime;
        GenRng();
    }

    public void GenRng()
    {
        rng = Random.Range(3, 10);
        print(rng);
    }

}
