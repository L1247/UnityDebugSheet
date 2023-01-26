#region

using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class LabelModel
    {
    #region Public Variables

        public int            Index          { get; }
        public LabelCellModel LabelCellModel { get; }
        public string         Text           => LabelCellModel.CellTexts.Text;

    #endregion

    #region Constructor

        public LabelModel(int index , LabelCellModel labelCellModel)
        {
            Index          = index;
            LabelCellModel = labelCellModel;
        }

    #endregion
    }
}