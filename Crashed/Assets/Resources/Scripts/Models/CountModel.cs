using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.

[Serializable]
public class CountModel
{
	public int minimum;             //Minimum value for our Count class.
	public int maximum;             //Maximum value for our Count class.


	//Assignment constructor.
	public CountModel(int min, int max)
	{
		minimum = min;
		maximum = max;
	}
}