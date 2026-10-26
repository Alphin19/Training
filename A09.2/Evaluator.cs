using System.Diagnostics;

namespace Eval;
class EvalException : Exception {
   public EvalException (string message) : base (message) { } }
   class Evaluator {
   public string Evaluate (string input) {
      var tokenizer =  new Tokenizer ();
      for (; ; ) {
         var token = new Tokenizer (input);
         if (token is TEnd) break;
         Process (token);
      }
            } }
public void process (Token token) {

   }