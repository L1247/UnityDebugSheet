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
    #region Public Variables

        public Button button;

        public Text text;

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

            gameObject.name = model.Name;
        }

    #endregion
    }

    public sealed class CustomButtonCellModel : CellModel
    {
    #region Public Variables

        public string Name;

        public Button SelectOnUp { get; set; }

        public Color  Color { get; } = Color.black;
        public string Text  { get; set; }

    #endregion

        internal void InvokeClicked()
        {
            Clicked?.Invoke();
        }

        public event Action Clicked;
    }
}