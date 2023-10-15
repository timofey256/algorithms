namespace KMP {
	class Program {
		private static string text = "AABAACAADAABAABA";
		private static int[] lps;

		public static void Main(string[] args) {
			string word = "AABA";

			ConstructLPS(word);	
			FindWord(word);
		}

		private static void ConstructLPS(string word) {
			lps = new int[word.Length];
			lps[0] = 0;
			int i = 1;
			int len = 0;
			while (i < word.Length) {
				if (word[i] == word[len]) {
					len += 1;
					lps[i] = len;
					i += 1;
				}	

				if (len != 0) {
					len = lps[len-1];
				}
				else {
					lps[i] = 0;
					i += 1;
				}
			}
		}
	
		private static void FindWord(string word) {
			int i = 0; // for word 
			int j = 0; // for text

			while (word.Length-i <= text.Length-j) {
				if (word[i] == text[j]) {
					i += 1;
					j += 1;
				}	

				if (i == word.Length) {
					Console.WriteLine("occurence!");
					i = lps[i-1];
				}
				else if (j < text.Length && word[i] != text[j]) {
					if (i != 0) {
						i = lps[i-1];
					}
					else {
						j += 1;
					}
				}
			}
		}
	}
}
