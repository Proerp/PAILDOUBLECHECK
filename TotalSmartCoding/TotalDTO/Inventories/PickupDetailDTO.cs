﻿using System;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalDTO.Helpers;
using System.Collections.Generic;
using TotalModel.Helpers;
using TotalBase;
using System.ComponentModel;
using TotalDTO.Helpers.Interfaces;

namespace TotalDTO.Inventories
{
    public class PickupDetailDTO : QuantityDetailDTO, IPrimitiveEntity, ILineDetailBinlLocation
    {
        public int GetID() { return this.PickupDetailID; }

        //IMPORTANT: IMPLEMENT PropertyChanged!!!!
        //NOW: AFTER ADD PickupDetailDTO TO COLLECTION, WE DON'T CHANGE THESE PROPERTIES FROM BINDING DataGridView ALSO FROM BACKEND PickupDetailDTO OBJECT. SO: WE DON'T IMPLEMENT PropertyChanged FOR THESE PROPERTIES
        //LATER: IF WE RECEIPT FORM OTHER SOURCE THAN FROM PICKUP ONLY, WE SHOULD CONSIDER THIS => AND IMPLEMENT PropertyChanged FOR THESE PROPERTIES WHEN NECCESSARY


        public int PickupDetailID { get; set; }
        public int PickupID { get; set; }

        public int BatchID { get; set; }
        public System.DateTime BatchEntryDate { get; set; }

        public int WarehouseID { get; set; }

        private Nullable<int> binLocationID;
        [DefaultValue(null)]
        public Nullable<int> BinLocationID
        {
            get { return this.binLocationID; }
            set { ApplyPropertyChange<PickupDetailDTO, Nullable<int>>(ref this.binLocationID, o => o.BinLocationID, value); }
        }

        private string binLocationCode;
        [DefaultValue(null)]
        public virtual string BinLocationCode
        {
            get { return this.binLocationCode; }
            set { ApplyPropertyChange<PickupDetailDTO, string>(ref this.binLocationCode, o => o.BinLocationCode, value, false); }
        }

        public Nullable<int> PackID { get; set; }
        public string PackCode { get; set; }
        public Nullable<System.DateTime> PackEntryDate { get; set; }


        public Nullable<int> CartonID { get; set; }
        public string CartonCode { get; set; }
        public Nullable<System.DateTime> CartonEntryDate { get; set; }


        public Nullable<int> PalletID { get; set; }
        public string PalletCode { get; set; }
        public Nullable<System.DateTime> PalletEntryDate { get; set; }

        public string BarcodeCode { get { return this.PalletID != null ? this.PalletCode : (this.CartonID != null ? this.CartonCode : PackCode); } }
        public Nullable<System.DateTime> BarcodeEntryDate { get { return this.PalletID != null ? this.PalletEntryDate : (this.CartonID != null ? this.CartonEntryDate : PackEntryDate); } }

        public override string Caption
        {
            get { return this.CommodityCodeAndName + ",   Quantity: " + this.Quantity.ToString("N0") + ",   Volume: " + this.Volume.ToString("N2"); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupDetailDTO>(p => p.PalletID), "Vui lòng chọn pallet.", delegate { return (this.PackID != null || this.CartonID != null || this.PalletID != null); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupDetailDTO>(p => p.BinLocationID), "Vui lòng chọn bin location.", delegate { return (this.BinLocationID != null); }));

            return validationRules;

        }

    }





}
