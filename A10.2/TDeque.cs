// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TDeque.cs
// A generic double-ended queue implemented using a circular array.
// ------------------------------------------------------------------------------------------------

#region class TDeque ------------------------------------------------------------------------------
/// <summary>A double-ended queue backed by a growable circular array</summary>
class TDeque<T> {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes the deque with the specified len</summary>
   public TDeque (int capacity = 4) {
      if (capacity <= 0) throw new ArgumentOutOfRangeException (nameof (capacity));
      mItems = new T[capacity];
      mHead = 0;
      mTail = -1;
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the number of items in the deque</summary>
   public int Count { get; private set; }

   /// <summary>Returns true when the deque contains no items</summary>
   public bool IsEmpty => Count == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds an item at the front of the deque</summary>
   public void AddFirst (T item) {
      EnsureCapacity ();
      int len = mItems.Length;
      if (IsEmpty) mHead = mTail = 0;
      else mHead = (mHead - 1 + len) % len;
      mItems[mHead] = item;
      Count++;
   }

   /// <summary>Adds an item at the rear of the deque</summary>
   public void AddLast (T item) {
      EnsureCapacity ();
      int len = mItems.Length;
      if (IsEmpty) mHead = mTail = 0;
      else mTail = (mTail + 1) % len;
      mItems[mTail] = item;
      Count++;
   }

   /// <summary>Returns the first item without removing it</summary>
   public T PeekFirst () {
      ThrowIfEmpty ();
      return mItems[mHead];
   }

   /// <summary>Returns the last item without removing it</summary>
   public T PeekLast () {
      ThrowIfEmpty ();
      return mItems[mTail];
   }

   /// <summary>Removes and returns the item at the front</summary>
   public T RemoveFirst () => Remove (true);

   /// <summary>Removes and returns the item at the rear</summary>
   public T RemoveLast () => Remove (false);
   #endregion

   #region Implementation -------------------------------------------
   // Increases capacity when the array becomes full
   void EnsureCapacity () {
      int len = mItems.Length;
      if (Count < len) return;
      T[] expanded = new T[len * 2];
      int firstSegmentLength = Math.Min (Count, len - mHead);
      Array.Copy (mItems, mHead, expanded, 0, firstSegmentLength);
      Array.Copy (mItems, 0, expanded, firstSegmentLength, Count - firstSegmentLength);
      mItems = expanded;
      mHead = 0;
      mTail = Count - 1;
   }

   // Removes an item from the specified end
   T Remove (bool first) {
      ThrowIfEmpty ();
      int len = mItems.Length;
      int index = first ? mHead : mTail;
      T item = mItems[index];
      mItems[index] = default!;
      Count--;
      if (IsEmpty) {
         mHead = 0;
         mTail = -1;
      } else if (first) {
         mHead = (mHead + 1) % len;
      } else {
         mTail = (mTail - 1 + len) % len;
      }
      return item;
   }

   // Throws an exception when the deque is empty
   void ThrowIfEmpty () {
      if (IsEmpty) throw new InvalidOperationException ("Deque is empty.");
   }
   #endregion

   #region Private Data ---------------------------------------------
   T[] mItems;
   int mHead, mTail;
   #endregion
}
#endregion