// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Spelling Bee - Letter Frequency
// ------------------------------------------------------------------------------------------------
string[] words = File.ReadAllLines ("words.txt");
Dictionary<char, int> freq = new ();
foreach (string word in words) {
   foreach (char c in word.ToUpper ())
      if (c >= 'A' && c <= 'Z') freq[c] = freq.GetValueOrDefault (c) + 1;
}
foreach (var ch in freq.OrderByDescending (a => a.Value).Take (7))
   Console.WriteLine ($"{ch.Key} : {ch.Value}");