﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	private float imgFillAmount;
	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Image content;
	 
	[SerializeField]
	private Text valueText;

	[SerializeField]
	private Color fullColor;

	[SerializeField]
	private Color lowColor;

	[SerializeField]
	private bool lerpColors;

	public float MaxValue { get; set;}

	public float Value {
		set
		{
			string[] tmp = valueText.text.Split(':');
			valueText.text = tmp [0] + ": " + value;
			imgFillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

	// Use this for initialization
	void Start () {
		if (lerpColors) 
		{
			content.color = fullColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	private void HandleBar () {
		if (imgFillAmount != content.fillAmount)
		{
			content.fillAmount = Mathf.Lerp(content.fillAmount,imgFillAmount,Time.deltaTime * lerpSpeed);
		}
		if (lerpColors) 
		{
			content.color = Color.Lerp (lowColor, fullColor, imgFillAmount);
		}


	}

	private float Map(float value,float inMin,float inMax,float outMin,float outMax) {
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		//calculates the float for fill amount based on the value of heath etc thats being iptuted
	}


}
