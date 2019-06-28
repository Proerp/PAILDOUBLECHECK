using TotalBase.Enums;

namespace TotalBase
{

    public static class ModelSettingManager
    {

        public static int ReferenceLength = 6;
        public static string ReferencePrefix(GlobalEnums.NmvnTaskID nmvnTaskID)
        {
            switch (nmvnTaskID)
            {
                case GlobalEnums.NmvnTaskID.Batch:
                    return "FI";

                case GlobalEnums.NmvnTaskID.PurchaseOrders:
                    return "D";
                case GlobalEnums.NmvnTaskID.PurchaseInvoices:
                    return "H";

                case GlobalEnums.NmvnTaskID.Pickups:
                    return "P";
                case GlobalEnums.NmvnTaskID.GoodsReceipts:
                    return "R";
                case GlobalEnums.NmvnTaskID.SalesOrders:
                    return "O";
                case GlobalEnums.NmvnTaskID.DeliveryAdvices:
                    return "D";
                case GlobalEnums.NmvnTaskID.SalesReturns:
                    return "SR";

                case GlobalEnums.NmvnTaskID.GoodsIssues:
                    return "K";
                case GlobalEnums.NmvnTaskID.TransferOrders:
                    return "TO";
                case GlobalEnums.NmvnTaskID.WarehouseAdjustments:
                    return "WA";


                case GlobalEnums.NmvnTaskID.Forecasts:
                    return "FC";
                case GlobalEnums.NmvnTaskID.CommoditySettings:
                    return "Z";

                default:
                    return "A";
            }


        }
    }
}
