﻿using TotalBase.Enums;
using TotalDTO;
using TotalDTO.Inventories;
using TotalSmartCoding.ViewModels.Helpers;

namespace TotalSmartCoding.ViewModels.Inventories
{
    public class GoodsReceiptViewModel : GoodsReceiptDTO, IViewDetailViewModel<GoodsReceiptDetailDTO>
    {
    }

    public class GoodsReceiptDetailAvailableViewModel : BaseDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.AvailableItems; } }

        public override bool AllowDataInput { get { return false; } }

        public override bool Printable { get { return true; } }
        public override bool PrintVisible { get { return true; } }
    }
}
