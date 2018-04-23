using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController: MonoBehaviour {
	[SerializeField]
	private Stat time;
    public float timeVal;

    int numberOfRuns = 1;
	private void Awake()
	{   
        time.Initialize ();
	}

    private void Start()
    {
        numberOfRuns = PlayerPrefs.GetInt("NumberOfRuns");
        if (numberOfRuns > 1 && time.MaxVal + 0.5f - (0.5f * numberOfRuns) > 0f)
        {
            time.MaxVal = time.MaxVal + 0.5f - (0.5f * numberOfRuns);
            time.CurrentVal = time.MaxVal;
        }
        else if (time.MaxVal + 0.5f - (0.5f * numberOfRuns) < 2f)
        {
            time.MaxVal = 2f;
            time.CurrentVal = time.MaxVal;
        }
    }

    private void FixedUpdate()
    {
        timeVal = time.CurrentVal;
        time.CurrentVal -= Time.deltaTime;
        
    }
}
