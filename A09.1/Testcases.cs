// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Testcases.cs
// Test cases for the TQueue<T> circular-buffer queue.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class TestCases ---------------------------------------------------------------------------
/// <summary>Runs test cases for the circular-buffer queue</summary>
class TestCases {
   #region Implementation -------------------------------------------
   static void Main () {
      Check ("Dequeue from an empty queue", DequeueEmpty);
      Check ("Single element, then empty again", SingleElement);
      Check ("Reuses freed slots at the front (wrap around)", WrapAround);
      Check ("Grows when completely full", GrowWhenFull);
      Check ("Grows after a wrap around", GrowAfterWrap);
      WriteLine ($"\n{mPassed} passed, {mFailed} failed");
   }

   // Tests whether Dequeue throws an exception when the queue is empty
   static bool DequeueEmpty () {
      try { new TQueue<int> ().Dequeue (); return false; }
      catch (InvalidOperationException) { return true; }
   }

   // Tests whether a single element can be added and removed correctly
   static bool SingleElement () {
      var q = new TQueue<int> ();
      q.Enqueue (42);
      return !q.IsEmpty && q.Dequeue () == 42 && q.IsEmpty;
   }

   // Tests whether the queue reuses freed array slots correctly
   static bool WrapAround () {
      var q = new TQueue<int> ();
      EnqueueAll (q, 1, 2, 3, 4);
      q.Dequeue ();
      q.Dequeue ();
      EnqueueAll (q, 5, 6);
      return DequeueMatches (q, 3, 4, 5, 6);
   }

   // Tests whether the queue grows when its capacity is exceeded
   static bool GrowWhenFull () {
      var q = new TQueue<int> ();
      EnqueueAll (q, 1, 2, 3, 4, 5, 6, 7, 8, 9);
      return DequeueMatches (q, 1, 2, 3, 4, 5, 6, 7, 8, 9);
   }

   // Tests whether the queue grows correctly when the buffer is wrapped
   static bool GrowAfterWrap () {
      var q = new TQueue<char> ();
      EnqueueAll (q, 'A', 'B', 'C', 'D');
      q.Dequeue (); q.Dequeue ();
      EnqueueAll (q, 'E', 'F', 'G');
      return DequeueMatches (q, 'C', 'D', 'E', 'F', 'G');
   }

   // Runs a test case and records whether it passed or failed
   static void Check (string name, Func<bool> test) {
      bool ok;
      try { ok = test (); } catch (Exception) { ok = false; }
      if (ok) mPassed++; else mFailed++;
      ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
      Write (ok ? "[PASS] " : "[FAIL] ");
      ResetColor ();
      WriteLine (name);
   }

   // Adds the given elements to the queue in order
   static void EnqueueAll<T> (TQueue<T> q, params T[] items) {
      foreach (T item in items) q.Enqueue (item);
   }

   // Tests whether the queue returns the expected elements and becomes empty
   static bool DequeueMatches<T> (TQueue<T> q, params T[] expected) {
      foreach (T item in expected)
         if (q.IsEmpty || !EqualityComparer<T>.Default.Equals (q.Dequeue (), item)) return false;
      return q.IsEmpty;
   }

   static int mPassed, mFailed;    // Number of test cases passed and failed
   #endregion
}
#endregion
