using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Sales
{
    public class Forecast
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Forecast(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetForecastIndexes();

            this.GetForecastViewDetails();

            this.ForecastSaveRelative();
            this.ForecastPostSaveValidate();

            this.ForecastEditable();

            this.ForecastInitReference();
        }


        private void GetForecastIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Forecasts.ForecastID, CAST(Forecasts.EntryDate AS DATE) AS EntryDate, Forecasts.Reference, Forecasts.VoucherCode, Locations.Code AS LocationCode, Forecasts.TotalQuantity, Forecasts.TotalLineVolume, Forecasts.TotalQuantityM1, Forecasts.TotalLineVolumeM1, Forecasts.TotalQuantityM2, Forecasts.TotalLineVolumeM2, Forecasts.TotalQuantityM3, Forecasts.TotalLineVolumeM3, Forecasts.QuantityVersusVolume, Forecasts.Description " + "\r\n";
            queryString = queryString + "       FROM        Forecasts " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON Forecasts.EntryDate >= @FromDate AND Forecasts.EntryDate <= @ToDate AND Forecasts.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.Forecasts + " AND AccessControls.AccessLevel > 0) AND Forecasts.ForecastLocationID = Locations.LocationID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetForecastIndexes", queryString);
        }


        #region X


        private void GetForecastViewDetails()
        {
            string queryString;

            queryString = " @ForecastID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      ForecastDetails.ForecastID, ForecastDetails.ForecastDetailID, ForecastDetails.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, CommodityCategories.Name AS CommodityCategoryName, " + "\r\n";
            queryString = queryString + "                   ForecastDetails.Quantity, ForecastDetails.LineVolume, ForecastDetails.QuantityM1, ForecastDetails.LineVolumeM1, ForecastDetails.QuantityM2, ForecastDetails.LineVolumeM2, ForecastDetails.QuantityM3, ForecastDetails.LineVolumeM3, ForecastDetails.Remarks " + "\r\n";
            queryString = queryString + "       FROM        ForecastDetails " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON ForecastDetails.ForecastID = @ForecastID AND ForecastDetails.CommodityID = Commodities.CommodityID " + "\r\n";
            queryString = queryString + "                   INNER JOIN CommodityCategories ON Commodities.CommodityCategoryID = CommodityCategories.CommodityCategoryID " + "\r\n";
            queryString = queryString + "       ORDER BY    ForecastDetails.ForecastDetailID " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetForecastViewDetails", queryString);
        }





        private void ForecastSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@SaveRelativeOption = 1) ";
            queryString = queryString + "       BEGIN ";
            queryString = queryString + "           UPDATE          ForecastDetails " + "\r\n";
            queryString = queryString + "           SET             ForecastDetails.Reference = Forecasts.Reference " + "\r\n";
            queryString = queryString + "           FROM            Forecasts INNER JOIN ForecastDetails ON Forecasts.ForecastID = @EntityID AND Forecasts.ForecastID = ForecastDetails.ForecastID " + "\r\n";
            queryString = queryString + "       END ";

            this.totalSmartCodingEntities.CreateStoredProcedure("ForecastSaveRelative", queryString);
        }

        private void ForecastPostSaveValidate()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("ForecastPostSaveValidate", queryArray);
        }



        private void ForecastEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("ForecastEditable", queryArray);
        }


        private void ForecastInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("Forecasts", "ForecastID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.Forecasts));
            this.totalSmartCodingEntities.CreateTrigger("ForecastInitReference", simpleInitReference.CreateQuery());
        }


        #endregion
    }
}
