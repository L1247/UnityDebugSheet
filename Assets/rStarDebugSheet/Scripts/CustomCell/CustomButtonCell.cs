#region

using System;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace rStarDebugSheet.Scripts.CustomCell
{
    public sealed class CustomButtonCell : Cell<CustomButtonCellModel>
    {
    #region Private Variables

        [SerializeField]
        private Text text;

        [SerializeField]
        private Button button;

    #endregion

    #region Protected Methods

        protected override void SetModel(CustomButtonCellModel model)
        {
            text.text  = model.Text;
            text.color = model.Color;
            // Button
            button.interactable = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(model.InvokeClicked);
        }

    #endregion
    }

    public sealed class CustomButtonCellModel : CellModel
    {
    #region Public Variables

        public Color  Color { get; set; } = Color.black;
        public string Text  { get; set; }

    #endregion

        internal void InvokeClicked()
        {
            Clicked?.Invoke();
        }

        public event Action Clicked;
    }
}