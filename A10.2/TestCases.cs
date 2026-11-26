// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Test cases for the generic double-ended queue.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region Class TestCases ---------------------------------------------------------------------------
/// <summary>Tests the generic double-ended queue implementation</summary>
class TestCases {
   #region Methods --------------------------------------------------
   /// <summary>Runs all test cases for the double-ended queue</summary>
   public static void Run () {
      TestInitialState ();
      TestAddFirst ();
      TestAddLast ();
      TestRemoveBothEnds ();
      TestMixedOperations ();
      TestSingleElement ();
      TestReuseAfterEmpty ();
      TestEmptyRemoval ();
      TestFrontWraparound ();
      TestRearWraparound ();
      TestGrowthAtFront ();
      TestGrowthAtRear ();
      TestGrowthAfterFrontWrap ();
      TestGrowthAfterRearWrap ();
      TestCount ();
      TestGenericType ();
      WriteLine ($"\n{sPassed} passed, {sFailed} failed");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Checks the initial state of the deque
   static void TestInitialState () {
      var deque = new TDeque<int> ();
      Check ("Initial state", deque.IsEmpty && deque.Count == 0);
   }

   // Checks insertion at the front
   static void TestAddFirst () {
      var deque = new TDeque<int> ();
      deque.AddFirst (10);
      deque.AddFirst (20);
      Check ("AddFirst order", VerifyOrder (deque, 20, 10));
   }

   // Checks insertion at the rear
   static void TestAddLast () {
      var deque = new TDeque<int> ();
      deque.AddLast (10);
      deque.AddLast (20);
      deque.AddLast (30);
      Check ("AddLast order", VerifyOrder (deque, 10, 20, 30));
   }

   // Checks removal from both ends
   static void TestRemoveBothEnds () {
      var deque = new TDeque<int> ();
      deque.AddLast (10);
      deque.AddLast (20);
      deque.AddLast (30);
      bool valid = deque.RemoveFirst () == 10 && deque.RemoveLast () == 30 &&
                   deque.RemoveFirst () == 20 && deque.IsEmpty;
      Check ("Remove from both ends", valid);
   }

   // Checks alternating insertion and removal operations
   static void TestMixedOperations () {
      var deque = new TDeque<int> ();
      deque.AddLast (20);
      deque.AddFirst (10);
      deque.AddLast (30);
      deque.AddFirst (0);
      bool valid = deque.RemoveFirst () == 0 && deque.RemoveLast () == 30 &&
                   deque.RemoveFirst () == 10 && deque.RemoveLast () == 20 && deque.IsEmpty;
      Check ("Mixed operations", valid);
   }

   // Checks insertion and removal of a single element
   static void TestSingleElement () {
      var deque = new TDeque<int> ();
      deque.AddFirst (10);
      bool valid = deque.Count == 1 && deque.RemoveLast () == 10 && deque.IsEmpty;
      deque.AddLast (20);
      valid &= deque.Count == 1 && deque.RemoveFirst () == 20 && deque.IsEmpty;
      Check ("Single element", valid);
   }

   // Checks whether the deque can be reused after becoming empty
   static void TestReuseAfterEmpty () {
      var deque = new TDeque<int> ();
      deque.AddLast (10);
      deque.RemoveFirst ();
      deque.AddFirst (20);
      deque.AddLast (30);
      Check ("Reuse after empty", VerifyOrder (deque, 20, 30));
   }

   // Checks removal from an empty deque
   static void TestEmptyRemoval () {
      var deque = new TDeque<int> ();
      bool valid = ThrowsInvalidOperation (() => deque.RemoveFirst ()) &&
                   ThrowsInvalidOperation (() => deque.RemoveLast ());
      Check ("Empty removal", valid);
   }

   // Checks circular movement of the head index
   static void TestFrontWraparound () {
      var deque = new TDeque<int> (4);
      for (int i = 1; i <= 4; i++) deque.AddLast (i);
      deque.RemoveLast ();
      deque.RemoveLast ();
      deque.AddFirst (0);
      deque.AddFirst (-1);
      Check ("Front wraparound", VerifyOrder (deque, -1, 0, 1, 2));
   }

   // Checks circular movement of the tail index
   static void TestRearWraparound () {
      var deque = new TDeque<int> (4);
      for (int i = 1; i <= 4; i++) deque.AddLast (i);
      deque.RemoveFirst ();
      deque.RemoveFirst ();
      deque.AddLast (5);
      deque.AddLast (6);
      Check ("Rear wraparound", VerifyOrder (deque, 3, 4, 5, 6));
   }

   // Checks capacity expansion when inserting at the front
   static void TestGrowthAtFront () {
      var deque = new TDeque<int> (2);
      for (int i = 1; i <= 6; i++) deque.AddFirst (i);
      Check ("Growth at front", VerifyOrder (deque, 6, 5, 4, 3, 2, 1));
   }

   // Checks capacity expansion when inserting at the rear
   static void TestGrowthAtRear () {
      var deque = new TDeque<int> (2);
      for (int i = 1; i <= 6; i++) deque.AddLast (i);
      Check ("Growth at rear", VerifyOrder (deque, 1, 2, 3, 4, 5, 6));
   }

   // Checks expansion after the head index wraps around
   static void TestGrowthAfterFrontWrap () {
      var deque = new TDeque<int> (4);
      deque.AddLast (1);
      deque.AddLast (2);
      deque.AddFirst (0);
      deque.AddFirst (-1);
      deque.AddLast (3);
      Check ("Growth after front wrap", VerifyOrder (deque, -1, 0, 1, 2, 3));
   }

   // Checks expansion after the tail index wraps around
   static void TestGrowthAfterRearWrap () {
      var deque = new TDeque<int> (4);
      for (int i = 1; i <= 4; i++) deque.AddLast (i);
      deque.RemoveFirst ();
      deque.RemoveFirst ();
      deque.AddLast (5);
      deque.AddLast (6);
      deque.AddLast (7);
      Check ("Growth after rear wrap", VerifyOrder (deque, 3, 4, 5, 6, 7));
   }

   // Checks whether Count is updated correctly
   static void TestCount () {
      var deque = new TDeque<int> ();
      deque.AddFirst (10);
      deque.AddLast (20);
      bool valid = deque.Count == 2;
      deque.RemoveFirst ();
      valid &= deque.Count == 1;
      deque.RemoveLast ();
      valid &= deque.Count == 0 && deque.IsEmpty;
      Check ("Count updates", valid);
   }

   // Checks whether the deque supports different data types
   static void TestGenericType () {
      var deque = new TDeque<string> ();
      deque.AddFirst ("Hello");
      deque.AddLast ("World");
      bool valid = deque.RemoveFirst () == "Hello" && deque.RemoveLast () == "World" &&
                   deque.IsEmpty;
      Check ("Generic type", valid);
   }

   // Checks the deque contents against the expected order
   static bool VerifyOrder (TDeque<int> deque, params int[] expected) {
      if (deque.Count != expected.Length) return false;
      foreach (int item in expected) if (deque.RemoveFirst () != item) return false;
      return deque.IsEmpty;
   }

   // Checks whether the specified operation throws InvalidOperationException
   static bool ThrowsInvalidOperation (Func<int> operation) {
      try { operation (); } catch (InvalidOperationException) { return true; }
      return false;
   }

   // Displays and records the test result
   static void Check (string name, bool passed) {
      if (passed) sPassed++; else sFailed++;
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      WriteLine ($"[{(passed ? "PASS" : "FAIL")}] {name}");
      ResetColor ();
   }
   #endregion

   #region Private Data ---------------------------------------------
   static int sPassed, sFailed;
   #endregion
}
#endregion