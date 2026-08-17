// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Spellbee game
// ------------------------------------------------------------------------------------------------
char[] validLetters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
string filePath = "words.txt";
string[] words = File.ReadAllLines (filePath);
int score = 0;
Dictionary<string, (int Score, bool IsPangram)> dict = new Dictionary<string, (int, bool)> ();
foreach (string word in words) {
   string validWord = word.ToUpper ();
   if (!IsValid (validWord) || validWord.Length < 4 || dict.ContainsKey (validWord)) continue;
   int wordScore = validWord.Length == 4 ? 1 : validWord.Length;
   bool isPangram = IsPangram (validWord);
   if (isPangram) wordScore += 7;
   score += wordScore;
   dict.Add (validWord, (wordScore, isPangram));
}
foreach (var item in dict.OrderByDescending (x => x.Value.Score).ThenBy (x => x.Key)) {
   if (item.Value.IsPangram) Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine ($"{item.Value.Score}. {item.Key}");
   Console.ResetColor ();
}
Console.WriteLine ("----");
Console.WriteLine ($"{score} total");

bool IsValid (string word) => word.Contains (validLetters[0]) && word.All (validLetters.Contains);

bool IsPangram (string word) => validLetters.All (word.Contains);