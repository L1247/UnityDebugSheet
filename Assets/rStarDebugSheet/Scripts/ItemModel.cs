#region

using rStarDebugSheet.Scripts.CustomCell;
using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class ItemModel
    {
    #region Public Variables

        public CustomButtonCellModel CellModel { get; }

        public int    Index { get; private set; }
        public string Text  => CellModel.Text;

    #endregion

    #region Constructor

        public ItemModel(CustomButtonCellModel cellModel)
        {
            CellModel = cellModel;
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