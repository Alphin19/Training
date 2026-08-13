// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Spellbee game
// ------------------------------------------------------------------------------------------------
char[] valid_letters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
string filePath = "words.txt";
string[] words = File.ReadAllLines (filePath);
int score = 0;
Dictionary<string, int> List = new Dictionary<string, int> ();
foreach (string word in words) if (is_valid (word)) total_score (word);
void total_score (string word) {
   int word_score = 0;
   if (word.Length < 4) return;
   if (word.Length == 4) word_score = 1;
   else word_score = word.Length;
   if (is_pangram (word)) word_score += 7;
   score += word_score;
   if (!List.ContainsKey (word)) List.Add (word, word_score);
}
bool is_valid (string word) {
   if (!word.Contains (valid_letters[0])) return false;
   foreach (char letter in word) if (!valid_letters.Contains (letter)) return false;
   return true;
}
bool is_pangram (string word) {
   foreach (char letter in valid_letters) if (!word.Contains (letter)) return false;
   return true;
}
foreach (var item in List.OrderByDescending (x => x.Value).ThenBy (x => x.Key)) {
   if (is_pangram (item.Key)) Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine ($"{item.Key,-20} {item.Value}");
   Console.ResetColor ();
}
Console.WriteLine ($"\nTotal Score : {score}");