﻿using System;

using TotalModel;
using TotalBase.Enums;

namespace TotalDTO.Productions
{
    public class PalletPrimitiveDTO : BarcodeDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public PalletPrimitiveDTO()
            : this(null)
        { }
        public PalletPrimitiveDTO(FillingData fillingData)
            : base(fillingData)
        { }


        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Pallet; } }

        public override int GetID() { return this.PalletID; }
        public virtual void SetID(int id) { this.PalletID = id; }

        public int PalletID { get; set; }

        public int CartonCounts { get; set; }
    }

    public class PalletDTO : PalletPrimitiveDTO, IShallowClone<PalletDTO>
    {
        public PalletDTO()
            : this(null)
        { }
        public PalletDTO(FillingData fillingData)
            : base(fillingData)
        { }


        public decimal QuantityPickup { get; set; }
        public string CartonIDs { get; set; }


        public PalletDTO ShallowClone()
        {
            return (PalletDTO)this.MemberwiseClone();
        }
    }
}
