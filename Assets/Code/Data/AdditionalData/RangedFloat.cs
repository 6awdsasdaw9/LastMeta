using System;

[Serializable]
public struct RangedFloat
{
	public float MinValue;
	public float MaxValue;

	public float GetRandom()
	{
		return UnityEngine.Random.Range(MinValue, MaxValue);
	}
	public float GetUpperRandom()
	{
		var difference = (MaxValue - MinValue) / 2;
		return UnityEngine.Random.Range(MinValue + difference, MaxValue);
	}
	public float GetLowerRandom()
	{
		var difference = (MaxValue - MinValue) / 2;
		return UnityEngine.Random.Range(MinValue, MaxValue - difference);
	}
}