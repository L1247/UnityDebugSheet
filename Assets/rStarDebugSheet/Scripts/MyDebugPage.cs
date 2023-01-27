#region

using System.Collections;
using System.Collections.Generic;
using rStarDebugSheet.Scripts.CustomCell;
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

        private const string CustomButtonCellKey = "CustomButtonCell";

        private readonly List<ItemModel> models        = new List<ItemModel>();
        private readonly List<ItemModel> searchResults = new List<ItemModel>();

    #endregion

    #region Public Methods

        public override IEnumerator Initialize()
        {
            AddSearchField("type something" , OnSearchFieldChanged , OnSearchFieldChanged);
            AddButton("Test1");
            AddButton("Test2");

            yield break;
        }

    #endregion

    #region Private Methods

        private void AddButton(string cellText)
        {
            var cellModel = new CustomButtonCellModel();
            cellModel.Text = cellText;
            var itemModel = new ItemModel(cellModel);
            AddCustomButton(itemModel);
            models.Add(itemModel);
            searchResults.Add(itemModel);
        }

        private void AddCustomButton(ItemModel itemModel)
        {
            var index = AddItem(CustomButtonCellKey , itemModel.CellModel);
            itemModel.SetIndex(index);
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

            foreach (var model in models)
            {
                var containStr = model.Text.Contains(str);
                if (containStr)
                {
                    if (searchResults.Contains(model)) continue;
                    searchResults.Add(model);
                    AddCustomButton(model);
                }
            }
        }

        private new void Reload()
        {
            foreach (var searchResult in searchResults) RemoveItem(searchResult.Index);
            searchResults.Clear();
            foreach (var labelModel in models) searchResults.Add(labelModel);
            foreach (var searchResult in searchResults) AddCustomButton(searchResult);
        }

    #endregion
    }
}