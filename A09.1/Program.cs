//-------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueue.cs
// A generic queue of T, using a circular buffer (array) as the underlying storage.
// ------------------------------------------------------------------------------------------------

#region class TQueue ------------------------------------------------------------------------------
/// <summary>A generic FIFO queue backed by a growable circular buffer</summary>
class TQueue<T> {
   #region Properties -----------------------------------------------
   /// <summary>Returns true if the queue holds no items</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds an item to the back of the queue</summary>
   public void Enqueue (T a) {
      if (mCount == mData.Length) Grow ();
      mData[(mHead + mCount) % mData.Length] = a;
      mCount++;
   }

   /// <summary>Removes and returns the item at the front of the queue</summary>
   public T Dequeue () {
      if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
      T item = mData[mHead];
      mData[mHead] = default!;                // Release the reference for garbage collection
      mHead = (mHead + 1) % mData.Length;
      mCount--;
      return item;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Doubles the capacity, copying the items in queue order so the head moves back to index 0
   void Grow () {
      var newData = new T[mData.Length * 2];
      int first = Math.Min (mCount, mData.Length - mHead);
      Array.Copy (mData, mHead, newData, 0, first);
      Array.Copy (mData, 0, newData, first, mCount - first);
      mData = newData;
      mHead = 0;
   }
   #endregion

   #region Private data----------------------------------------------
   T[] mData = new T[4];   // The circular buffer
   int mHead;              // Index of the next item to dequeue
   int mCount;             // Number of items currently in the queue
   #endregion
}
#endregion