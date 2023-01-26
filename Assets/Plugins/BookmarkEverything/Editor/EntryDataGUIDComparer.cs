#region

using System;
using System.Collections.Generic;

#endregion

namespace BookmarkEverything
{
    public class EntryDataGUIDComparer : EqualityComparer<BookmarkEverythingEditor.EntryData>
    {
    #region Public Methods

        public override bool Equals(BookmarkEverythingEditor.EntryData x , BookmarkEverythingEditor.EntryData y)
        {
            return x.GUID == y.GUID;
        }

        public override int GetHashCode(BookmarkEverythingEditor.EntryData obj)
        {
            throw new NotImplementedException();
        }

    #endregion
    }
}