// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TDeque.cs
// A generic double-ended queue implemented using a circular array.
// ------------------------------------------------------------------------------------------------

#region Class TDeque ------------------------------------------------------------------------------
/// <summary>A double-ended queue backed by a growable circular array</summary>
class TDeque<T> {
   #region Fields ---------------------------------------------------------------------------------
   T[] mItems;
   int mHead;
   int mTail;
   int mCount;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the number of items in the deque</summary>
   public int Count => mCount;

   /// <summary>Returns true when the deque contains no items</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Constructor ----------------------------------------------
   /// <summary>Initializes the deque with the specified capacity</summary>
   public TDeque (int capacity = 4) {
      if (capacity <= 0) throw new ArgumentOutOfRangeException (nameof (capacity));
      mItems = new T[capacity];
      mHead = 0;
      mTail = -1;
      mCount = 0;
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds an item at the front of the deque</summary>
   public void AddFirst (T item) {
      EnsureCapacity ();
      if (IsEmpty) mHead = mTail = 0;
      else mHead = (mHead - 1 + mItems.Length) % mItems.Length;
      mItems[mHead] = item;
      mCount++;
   }

   /// <summary>Adds an item at the rear of the deque</summary>
   public void AddLast (T item) {
      EnsureCapacity ();
      if (IsEmpty) mHead = mTail = 0;
      else mTail = (mTail + 1) % mItems.Length;
      mItems[mTail] = item;
      mCount++;
   }

   /// <summary>Removes and returns the item at the front</summary>
   public T RemoveFirst () {
      if (IsEmpty) throw new InvalidOperationException ("Deque is empty.");
      T item = mItems[mHead];
      mItems[mHead] = default!;
      mCount--;
      if (IsEmpty) { mHead = 0; mTail = -1; } else mHead = (mHead + 1) % mItems.Length;
      return item;
   }

   /// <summary>Removes and returns the item at the rear</summary>
   public T RemoveLast () {
      if (IsEmpty) throw new InvalidOperationException ("Deque is empty.");
      T item = mItems[mTail];
      mItems[mTail] = default!;
      mCount--;
      if (IsEmpty) { mHead = 0; mTail = -1; } else mTail = (mTail - 1 + mItems.Length) % mItems.Length;
      return item;
   }

   /// <summary>Increases capacity when the array becomes full</summary>
   void EnsureCapacity () {
      if (mCount < mItems.Length) return;
      T[] expanded = new T[mItems.Length * 2];
      for (int i = 0; i < mCount; i++) expanded[i] = mItems[(mHead + i) % mItems.Length];
      mItems = expanded;
      mHead = 0;
      mTail = mCount - 1;
   }
   #endregion
}
#endregion