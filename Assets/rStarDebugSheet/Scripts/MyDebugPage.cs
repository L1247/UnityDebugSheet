#region

using System;
using System.Collections;
using System.Collections.Generic;
using rStarDebugSheet.Scripts.CustomCell;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        private bool initOfGameObject = true;

    #endregion

    #region Public Methods

        public override IEnumerator Initialize()
        {
            // EventSystem.current.SetSelectedGameObject(myDebugPage.gameObject);
            // AddSearchField("type something" , OnSearchFieldChanged , OnSearchFieldChanged);
            AddButton("Test1" , () => Debug.Log("1"));
            AddButton("Test2" , () => Debug.Log("2"));
            AddButton("Test3" , () => Debug.Log("3"));
            AddButton("Test4" , () => Debug.Log("4"));

            yield break;
        }

    #endregion

    #region Protected Methods

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (initOfGameObject)
            {
                var buttonCells = GetComponentsInChildren<CustomButtonCell>();
                EventSystem.current.SetSelectedGameObject(buttonCells[0].gameObject);
                for (var index = 0 ; index < buttonCells.Length ; index++)
                {
                    int upIndex;
                    int downIndex;

                    var button      = buttonCells[index].button;
                    var buttonCount = buttonCells.Length;
                    var isFirstCell = index == 0;
                    var isLastCell  = index == buttonCount - 1;
                    if (isFirstCell)
                    {
                        upIndex   = buttonCount - 1;
                        downIndex = index + 1;
                    }
                    else if (isLastCell)
                    {
                        upIndex   = index - 1;
                        downIndex = 0;
                    }
                    else
                    {
                        upIndex   = index - 1;
                        downIndex = index + 1;
                    }

                    var up   = buttonCells[upIndex].button;
                    var down = buttonCells[downIndex].button;

                    button.navigation = new Navigation { mode = Navigation.Mode.Explicit , selectOnUp = up , selectOnDown = down };
                }

                initOfGameObject = false;
            }
        }

    #endregion

    #region Private Methods

        private void AddButton(string cellText , Action clicked = null)
        {
            var cellModel = new CustomButtonCellModel();
            cellModel.Text = cellText;
            cellModel.Name = $"CustomButtonCellModel:{ItemInfos.Count}";
            if (clicked != null) cellModel.Clicked += clicked;

            var itemModel = new ItemModel(cellModel);
            AddCustomButton(itemModel);
            models.Add(itemModel);
            searchResults.Add(itemModel);
        }

        private void AddCustomButton(ItemModel itemModel)
        {
            var id = AddItem(CustomButtonCellKey , itemModel.CellModel);
            itemModel.SetId(id);
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
                    RemoveItem(labelCell.Id);
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
            foreach (var searchResult in searchResults) RemoveItem(searchResult.Id);
            searchResults.Clear();
            foreach (var labelModel in models) searchResults.Add(labelModel);
            foreach (var searchResult in searchResults) AddCustomButton(searchResult);
        }

    #endregion
    }
}