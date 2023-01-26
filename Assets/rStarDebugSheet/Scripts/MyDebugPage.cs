#region

using System.Collections;
using System.Collections.Generic;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;
using UnityEngine;

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

        private void Init()
        {
            AddSearchField("type something" , OnSearchFieldChanged , _ => Debug.Log($"{_}"));
            AddLabel("Test1");
            AddLabel("Test2");
        }

        private void OnSearchFieldChanged(string str)
        {
            Debug.Log($"OnSearchFieldChanged: {str}");
            var reload = string.IsNullOrEmpty(str);
            if (reload)
            {
                Reload();
                return;
            }

            foreach (var labelCell in searchResults)
            {
                var containStr       = labelCell.Text.Contains(str);
                var notInFilterRange = containStr == false;
                if (notInFilterRange)
                {
                    searchResults.Remove(labelCell);
                    RemoveItem(labelCell.Index);
                }
            }
        }

        private new void Reload()
        {
            foreach (var labelModel in labelCells) searchResults.Add(labelModel);

            foreach (var searchResult in searchResults) AddLabel(searchResult.LabelCellModel);
        }

    #endregion
    }
}