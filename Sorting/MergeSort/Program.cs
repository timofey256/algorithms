namespace MergeSort;

class MergeSortClass
{
	private static int[] MergeSort(int[] a)
	{
		if (a.Length == 1)
		{
			return a;
		}
		
		int[] firstHalf = a.Take(a.Length/2).ToArray();
		int[] secondHalf = a.Skip(a.Length/2).ToArray();
	
		firstHalf = MergeSort(firstHalf);
		secondHalf = MergeSort(secondHalf);

		return Merge(firstHalf, secondHalf);
	}

	private static int[] Merge(int[] a, int[] b)
	{
		int i=0;
		int j=0;
		int index=0;
		
		int[] answer = new int[a.Length+b.Length];

		while (i < a.Length && j < b.Length)
		{
			if (a[i] <= b[j])
			{
				answer[index] = a[i];
				i += 1;
			}
			else
			{
				answer[index] = b[j];
				j += 1;
			}
			index += 1;
		}

		while (i < a.Length)
		{
			answer[index] = a[i];
			i += 1;
			index += 1;
		}


		while (j < b.Length)
		{
			answer[index] = b[j];
			j += 1;
			index += 1;
		}

		return answer;
	}

	public static void Main(string[] args)
	{
		int[] array = new int[] { 5, 1, 7, 10, 2, 6, 12, 11 };
		array = MergeSort(array);
		for (int i=0; i<array.Length; i++)
		{
			Console.Write($"{array[i]} ");
		}
	}
}
