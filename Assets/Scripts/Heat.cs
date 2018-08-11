using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Heat : MonoBehaviour
{
	public ParticleSystem fire;
	public ParticleSystem smoke;

	public MinMaxInt fireWhenHot;
	public MinMaxInt fireWhenWarm;

	public MinMaxInt smokeWhenHot;
	public MinMaxInt smokeWhenWarm;

	private float currentTemp = 0;

	void Start()
	{
		SetTemp(currentTemp);
	}

	void OnValidate()
	{
		SetTemp(currentTemp);
	}

	private void SetTemp(float temp) // 0-100; 0 = out, 50 is smoking with embers, 100 is raging fire
	{
		currentTemp = temp;

		// WARM! MOSTLY SMOKE!
		if (temp < 50)
		{
			SetSmoke(temp / 50 * smokeWhenWarm.length + smokeWhenWarm.min);
			SetFire(temp / 50 * fireWhenWarm.length + fireWhenWarm.min);
		}
		// HOT! FIRE!
		else
		{
			temp -= 50;
			SetSmoke(temp / 50 * smokeWhenHot.length + smokeWhenHot.min);
			SetFire(temp / 50 * fireWhenHot.length + fireWhenHot.min);
		}
	}

	private void SetFire(float count)
	{
		ParticleSystem.EmissionModule emission = fire.emission;
		emission.rateOverTime = Mathf.FloorToInt(count);
	}

	private void SetSmoke(float count)
	{
		ParticleSystem.EmissionModule emission = smoke.emission;
		emission.rateOverTime = Mathf.FloorToInt(count);
	}

	void OnGUI()
	{
		float newTemp = GUI.HorizontalSlider(new Rect(20, 40, 200, 50), currentTemp, 0, 100);
		GUI.Label(new Rect(20, 20, 400, 20), "Temperature (" + currentTemp + ")");

		if(newTemp != currentTemp)
		{
			currentTemp = newTemp;
			SetTemp(currentTemp);
		}
	}
}
