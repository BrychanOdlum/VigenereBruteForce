using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class brute {

	public static void Main(String[] args) {



		string dict = @"dict.txt";
		string output = @"output2.txt";
		List<string> dictionary = new List<string>();

		Dictionary<string, int> countRefs = new Dictionary<string, int> {};



		if (!File.Exists(output))  {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(output))  {

			}
        }



		string value = "YCUMLEMBAJJOPEEYSPETEOSGFJH";
		string attempt;
		int counter  = 0;
		double progress = 0, total = 0;
		int keylength = 5;
		int limiter = 2;


		using (StreamReader sr = File.OpenText(dict))  {
	    	string wordpre = "";
		    while ((wordpre = sr.ReadLine()) != null) {
				total += 1;
				if (wordpre.Length >= limiter)
					dictionary.Add(wordpre);
			}
		}



		using (StreamReader sr = File.OpenText(dict))  {
            string word = "";
            while ((word = sr.ReadLine()) != null) {
				progress += 1;
				if (isAllLetters(word)) {
					if (word.Length == keylength) {
						attempt = decipherVeginere(value, word);
						if (dictionary.Any(attempt.Contains)) {
							counter += 1;
							Console.WriteLine("\r[S] " + word + "\t\t" + attempt);
							using (StreamWriter sw = File.AppendText(output)) {
								sw.WriteLine("\r[S] " + word + "\t\t" + attempt);
							}

							countRefs.Add(word, dictionary.Count(s => attempt.Contains(s)));
						}
					}
				}
				Console.Write("\r>{0:n}%", (100/total)*progress);
            }



        }

		Console.WriteLine("  [Total: {0}]", total);
		Console.WriteLine("");

		Console.WriteLine("// ########## ########## ########## //");
		Console.WriteLine(" > MOST PROBABLE KEYS / VALUES");
		Console.WriteLine("// ########## ########## ########## //\n");
		foreach (var item in countRefs.OrderByDescending(r => r.Value).Take(10)) {
			Console.WriteLine("KEY {0}, \"{1}\" [{2}]", item.Key, decipherVeginere(value, item.Key), item.Value);
		}

		Console.WriteLine("");
		Console.WriteLine("");







	}

	public static bool isAllLetters(string s) {
    	foreach (char c in s) {
        if (!Char.IsLetter(c))
            return false;
    	}
    	return true;
	}

	static string decipherVeginere(string text, string key)	{
	    StringBuilder result = new StringBuilder();
	    int keyLength = key.Length;
	    int diff;
	    char decoded;

		text = text.Replace(" ", "").ToLower();

	    for (int i = 0; i < text.Length; i++) {
	        diff = text[i] - key[i%keyLength];

	        //diff should never be more than 25 or less than -25
	        if(diff < 0)
	            diff += 26;

	        decoded = (char)(diff + 'a') ;
	        result.Append(decoded);
	    }

	    return result.ToString();
	}

}
