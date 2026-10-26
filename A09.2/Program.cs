using Eval;


   Evaluator eval = new Evaluator ();
for (; ; ) {
   string input = Console.ReadLine ().Trim().ToLower();
   try {
      string result = eval.Evaluate (input);
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine (result);
   } catch (Exception e) {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine (e.Message);
   }
   Console.ResetColor ();
}
