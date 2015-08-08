package net.forgottenschism.util;

public class GenericUtil
{
	public static int minimum(int a, int b)
	{
		return a<b ? a : b;
	}

	public static int maximum(int a, int b)
	{
		return a>b ? a : b;
	}

	public static int sum(int[] numbers)
	{
		int sum = 0;

		for(int i = 0; i<numbers.length; i++)
			sum += numbers[i];

		return sum;
	}
}
