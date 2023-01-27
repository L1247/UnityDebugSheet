using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace rStarDebugSheet.Scripts.CustomCell
{
    public sealed class CustomButtonCell : Cell<CustomButtonCellModel>
    {
        [SerializeField]
        private Text text;

        protected override void SetModel(CustomButtonCellModel model)
        {
            text.text                     = model.Text;
            text.color                    = model.Color;

        }

    }

    public sealed class CustomButtonCellModel : CellModel
    {
        public string Text { get; set; }

        public Color Color { get; set; } = Color.black;
    }
}