#region

using System.Collections;
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

    #region Public Methods

        public override IEnumerator Initialize()
        {
            // Add a button to this page.
            Init();

            yield break;
        }

    #endregion

    #region Private Methods

        private void Init()
        {
            AddSearchField("type something" , OnSearchFieldChanged , _ => Debug.Log($"{_}"));
            AddLabel("Test1");
            AddLabel("Test2");
            AddButton("Reload" , clicked : ClearItems);
        }

        private void OnSearchFieldChanged(string str)
        {
            Debug.Log($"{str}");
            var reload = string.IsNullOrEmpty(str);
            if (reload)
            {
                ClearItems();
                Init();
                return;
            }

            for (var index = 0 ; index < ItemInfos.Count ; index++)
            {
                var itemInfo   = ItemInfos[index];
                var isNotLabel = itemInfo.PrefabKey != "LabelCell";
                if (isNotLabel) continue;

                var labelCell  = (LabelCellModel)itemInfo.CellModel;
                var containStr = labelCell.CellTexts.Text.Contains(str);
                if (containStr == false) RemoveItem(index);
            }
        }

    #endregion
    }
}