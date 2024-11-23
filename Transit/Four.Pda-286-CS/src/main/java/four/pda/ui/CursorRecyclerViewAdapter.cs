using Android.Content;
using Android.Database;
using Androidx.Recyclerview.Widget;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    public abstract class CursorRecyclerViewAdapter<VH> : Adapter<VH>
    {
        private Context mContext;
        private Cursor mCursor;
        private bool mDataValid;
        private int mRowIdColumn;
        private DataSetObserver mDataSetObserver;
        public CursorRecyclerViewAdapter(Context context, Cursor cursor)
        {
            mContext = context;
            mCursor = cursor;
            mDataValid = cursor != null;
            mRowIdColumn = mDataValid ? mCursor.GetColumnIndex("_id") : -1;
            mDataSetObserver = new NotifyingDataSetObserver();
            if (mCursor != null)
            {
                mCursor.RegisterDataSetObserver(mDataSetObserver);
            }
        }

        public virtual Cursor GetCursor()
        {
            return mCursor;
        }

        public override int GetItemCount()
        {
            if (mDataValid && mCursor != null)
            {
                return mCursor.GetCount();
            }

            return 0;
        }

        public override long GetItemId(int position)
        {
            if (mDataValid && mCursor != null && mCursor.MoveToPosition(position))
            {
                return mCursor.GetLong(mRowIdColumn);
            }

            return 0;
        }

        public override void SetHasStableIds(bool hasStableIds)
        {
            base.SetHasStableIds(true);
        }

        public abstract void OnBindViewHolder(VH viewHolder, Cursor cursor);
        public override void OnBindViewHolder(VH viewHolder, int position)
        {
            if (!mDataValid)
            {
                throw new InvalidOperationException("this should only be called when the cursor is valid");
            }

            if (!mCursor.MoveToPosition(position))
            {
                throw new InvalidOperationException("couldn't move cursor to position " + position);
            }

            OnBindViewHolder(viewHolder, mCursor);
        }

        public virtual void ChangeCursor(Cursor cursor)
        {
            Cursor old = SwapCursor(cursor);
            if (old != null)
            {
                old.Dispose();
            }
        }

        public virtual Cursor SwapCursor(Cursor newCursor)
        {
            if (newCursor == mCursor)
            {
                return null;
            }

            Cursor oldCursor = mCursor;
            if (oldCursor != null && mDataSetObserver != null)
            {
                oldCursor.UnregisterDataSetObserver(mDataSetObserver);
            }

            mCursor = newCursor;
            if (mCursor != null)
            {
                if (mDataSetObserver != null)
                {
                    mCursor.RegisterDataSetObserver(mDataSetObserver);
                }

                mRowIdColumn = newCursor.GetColumnIndexOrThrow("_id");
                mDataValid = true;
                NotifyDataSetChanged();
            }
            else
            {
                mRowIdColumn = -1;
                mDataValid = false;
                NotifyDataSetChanged(); //There is no notifyDataSetInvalidated() method in RecyclerView.Adapter
            }

            return oldCursor;
        }

        //There is no notifyDataSetInvalidated() method in RecyclerView.Adapter
        private class NotifyingDataSetObserver : DataSetObserver
        {
            public override void OnChanged()
            {
                base.OnChanged();
                mDataValid = true;
                NotifyDataSetChanged();
            }

            public override void OnInvalidated()
            {
                base.OnInvalidated();
                mDataValid = false;
                NotifyDataSetChanged(); //There is no notifyDataSetInvalidated() method in RecyclerView.Adapter
            }
        }
    }
}