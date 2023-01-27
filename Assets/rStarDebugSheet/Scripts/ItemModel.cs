#region

using rStarDebugSheet.Scripts.CustomCell;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class ItemModel
    {
    #region Public Variables

        public CustomButtonCellModel CellModel { get; }

        public int    Id   { get; private set; }
        public string Text => CellModel.Text;

    #endregion

    #region Constructor

        public ItemModel(CustomButtonCellModel cellModel)
        {
            CellModel = cellModel;
        }

    #endregion

    #region Public Methods

        public void SetId(int index)
        {
            Id = index;
        }

    #endregion
    }
}