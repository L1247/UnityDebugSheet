#region

using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class ItemModel
    {
    #region Public Variables

        public ButtonCellModel CellModel { get; }

        public int    Index { get; private set; }
        public string Text  => CellModel.CellTexts.Text;

    #endregion

    #region Constructor

        public ItemModel(int index , ButtonCellModel labelCellModel)
        {
            Index     = index;
            CellModel = labelCellModel;
        }

    #endregion

    #region Public Methods

        public void SetIndex(int index)
        {
            Index = index;
        }

    #endregion
    }
}