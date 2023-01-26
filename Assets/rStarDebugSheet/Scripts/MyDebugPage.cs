#region

using System.Collections;
using System.Collections.Generic;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class MyDebugPage : DefaultDebugPageBase
    {
    #region Protected Variables

        protected override string Title => "MyDebugPage";

    #endregion

    #region Private Variables

        private readonly List<LabelModel> labelCells    = new List<LabelModel>();
        private readonly List<LabelModel> searchResults = new List<LabelModel>();

    #endregion

    #region Public Methods

        public override IEnumerator Initialize()
        {
            // Add a button to this page.
            Init();

            yield break;
        }

    #endregion

    #region Private Methods

        private void AddLabel(string cellText)
        {
            var labelCellModel = new LabelCellModel(false);
            labelCellModel.CellTexts.Text = cellText;
            var index      = AddLabel(labelCellModel);
            var labelModel = new LabelModel(index , labelCellModel);
            labelCells.Add(labelModel);
            searchResults.Add(labelModel);
        }

        private void AddLabel(LabelModel labelModel)
        {
            var index = AddLabel(labelModel.LabelCellModel);
            labelModel.SetIndx(index);
        }

        private void Init()
        {
            AddSearchField("type something" , OnSearchFieldChanged , OnSearchFieldChanged);
            AddLabel("Test1");
            AddLabel("Test2");
        }

        private void OnSearchFieldChanged(string str)
        {
            var reload = string.IsNullOrEmpty(str);
            if (reload)
            {
                Reload();
                return;
            }

            for (var index = searchResults.Count - 1 ; index >= 0 ; index--)
            {
                var labelCell        = searchResults[index];
                var containStr       = labelCell.Text.Contains(str);
                var notInFilterRange = containStr == false;
                if (notInFilterRange)
                {
                    searchResults.Remove(labelCell);
                    RemoveItem(labelCell.Index);
                }
            }

            foreach (var labelModel in labelCells)
            {
                var containStr = labelModel.Text.Contains(str);
                if (containStr)
                {
                    if (searchResults.Contains(labelModel)) continue;
                    searchResults.Add(labelModel);
                    AddLabel(labelModel);
                }
            }
        }

        private new void Reload()
        {
            foreach (var searchResult in searchResults) RemoveItem(searchResult.Index);
            searchResults.Clear();
            foreach (var labelModel in labelCells) searchResults.Add(labelModel);
            foreach (var searchResult in searchResults) AddLabel(searchResult);
        }

    #endregion
    }
}