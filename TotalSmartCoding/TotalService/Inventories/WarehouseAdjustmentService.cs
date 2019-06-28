﻿using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Inventories;
using TotalCore.Repositories.Inventories;
using TotalCore.Services.Inventories;
using TotalBase.Enums;
using TotalDAL.Repositories.Inventories;
using System;

namespace TotalService.Inventories
{
    public class WarehouseAdjustmentService : GenericWithViewDetailService<WarehouseAdjustment, WarehouseAdjustmentDetail, WarehouseAdjustmentViewDetail, WarehouseAdjustmentDTO, WarehouseAdjustmentPrimitiveDTO, WarehouseAdjustmentDetailDTO>, IWarehouseAdjustmentService
    {
        public WarehouseAdjustmentService(IWarehouseAdjustmentRepository warehouseAdjustmentRepository)
            : base(warehouseAdjustmentRepository, "WarehouseAdjustmentPostSaveValidate", "WarehouseAdjustmentSaveRelative", "WarehouseAdjustmentToggleApproved", null, null, "GetWarehouseAdjustmentViewDetails")
        {
        }

        public override ICollection<WarehouseAdjustmentViewDetail> GetViewDetails(int warehouseAdjustmentID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("WarehouseAdjustmentID", warehouseAdjustmentID) };
            return this.GetViewDetails(parameters);
        }

        protected override void SaveRelative(WarehouseAdjustment warehouseAdjustment, SaveRelativeOption saveRelativeOption)
        {
            base.SaveRelative(warehouseAdjustment, saveRelativeOption);

            if (warehouseAdjustment.HasPositiveLine)
            {
                IGoodsReceiptAPIRepository goodsReceiptAPIRepository = new GoodsReceiptAPIRepository(this.GenericWithDetailRepository.TotalSmartCodingEntities);
                IGoodsReceiptBaseService goodsReceiptBaseService = new GoodsReceiptBaseService(new GoodsReceiptRepository(this.GenericWithDetailRepository.TotalSmartCodingEntities));

                //VERY IMPORTANT: THE BaseService.UserID IS AUTOMATICALLY SET BY CustomControllerAttribute OF CONTROLLER, ONLY WHEN BaseService IS INITIALIZED BY CONTROLLER. BUT HERE, THE this.goodsReceiptBaseService IS INITIALIZED BY VehiclesInvoiceService => SO SHOULD SET goodsReceiptBaseService.UserID = this.UserID
                goodsReceiptBaseService.UserID = this.UserID;

                if (saveRelativeOption == SaveRelativeOption.Update)
                {
                    GoodsReceiptDTO goodsReceiptDTO = new GoodsReceiptDTO();

                    goodsReceiptDTO.EntryDate = warehouseAdjustment.EntryDate;
                    goodsReceiptDTO.WarehouseID = warehouseAdjustment.WarehouseReceiptID;

                    goodsReceiptDTO.WarehouseAdjustmentID = warehouseAdjustment.WarehouseAdjustmentID;

                    goodsReceiptDTO.GoodsReceiptTypeID = (int)GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments;

                    goodsReceiptDTO.StorekeeperID = warehouseAdjustment.StorekeeperID;

                    goodsReceiptDTO.Description = warehouseAdjustment.Description;
                    goodsReceiptDTO.Remarks = warehouseAdjustment.Remarks;

                    List<PendingWarehouseAdjustmentDetail> pendingWarehouseAdjustmentDetails = goodsReceiptAPIRepository.GetPendingWarehouseAdjustmentDetails(warehouseAdjustment.LocationID, null, warehouseAdjustment.WarehouseAdjustmentID, warehouseAdjustment.WarehouseReceiptID, null, false);
                    foreach (PendingWarehouseAdjustmentDetail pendingWarehouseAdjustmentDetail in pendingWarehouseAdjustmentDetails)
                    {
                        GoodsReceiptDetailDTO goodsReceiptDetailDTO = new GoodsReceiptDetailDTO()
                        {
                            GoodsReceiptID = goodsReceiptDTO.GoodsReceiptID,

                            WarehouseAdjustmentID = pendingWarehouseAdjustmentDetail.WarehouseAdjustmentID,
                            WarehouseAdjustmentDetailID = pendingWarehouseAdjustmentDetail.WarehouseAdjustmentDetailID,
                            WarehouseAdjustmentReference = pendingWarehouseAdjustmentDetail.PrimaryReference,
                            WarehouseAdjustmentEntryDate = pendingWarehouseAdjustmentDetail.PrimaryEntryDate,

                            WarehouseAdjustmentTypeID = pendingWarehouseAdjustmentDetail.WarehouseAdjustmentTypeID,

                            BatchID = pendingWarehouseAdjustmentDetail.BatchID,
                            BatchEntryDate = pendingWarehouseAdjustmentDetail.BatchEntryDate,

                            BinLocationID = pendingWarehouseAdjustmentDetail.BinLocationID,
                            BinLocationCode = pendingWarehouseAdjustmentDetail.BinLocationCode,

                            CommodityID = pendingWarehouseAdjustmentDetail.CommodityID,
                            CommodityCode = pendingWarehouseAdjustmentDetail.CommodityCode,
                            CommodityName = pendingWarehouseAdjustmentDetail.CommodityName,

                            Quantity = (decimal)pendingWarehouseAdjustmentDetail.QuantityRemains,
                            LineVolume = (decimal)pendingWarehouseAdjustmentDetail.LineVolumeRemains,

                            PackID = pendingWarehouseAdjustmentDetail.PackID,
                            PackCode = pendingWarehouseAdjustmentDetail.PackCode,
                            CartonID = pendingWarehouseAdjustmentDetail.CartonID,
                            CartonCode = pendingWarehouseAdjustmentDetail.CartonCode,
                            PalletID = pendingWarehouseAdjustmentDetail.PalletID,
                            PalletCode = pendingWarehouseAdjustmentDetail.PalletCode,

                            PackCounts = pendingWarehouseAdjustmentDetail.PackCounts,
                            CartonCounts = pendingWarehouseAdjustmentDetail.CartonCounts,
                            PalletCounts = pendingWarehouseAdjustmentDetail.PalletCounts
                        };
                        goodsReceiptDTO.ViewDetails.Add(goodsReceiptDetailDTO);
                    }

                    goodsReceiptBaseService.Save(goodsReceiptDTO, true);
                }

                if (saveRelativeOption == SaveRelativeOption.Undo)
                {//NOTES: THIS UNDO REQUIRE: JUST SAVE ONLY ONE GoodsReceipt FOR AN WarehouseAdjustment
                    int? goodsReceiptID = goodsReceiptAPIRepository.GetGoodsReceiptIDofWarehouseAdjustment(warehouseAdjustment.WarehouseAdjustmentID);
                    if (goodsReceiptID != null)
                        goodsReceiptBaseService.Delete((int)goodsReceiptID, true);
                    else
                        throw new Exception("Lỗi không tìm thấy phiếu nhập kho cũ của phiếu điều chỉnh kho này!" + "\r\n" + "\r\n" + "Vui lòng kiểm tra lại dữ liệu trước khi tiếp tục.");
                }
            }
        }
    }
}
