using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalBase.Enums;
using TotalModel;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class FillingLineDetailDTO : BaseModel, IBaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.FillingLineDetailID; }

        public int FillingLineDetailID { get; set; }
        public int FillingLineID { get; set; }

        private int deviceID;
        public int DeviceID
        {
            get { return this.deviceID; }
            set { ApplyPropertyChange<FillingLineDetailDTO, int>(ref this.deviceID, o => o.DeviceID, value); }
        }

        private string deviceName;
        public string DeviceName
        {
            get { return this.deviceName; }
            set { ApplyPropertyChange<FillingLineDetailDTO, string>(ref this.deviceName, o => o.DeviceName, value); }
        }

        private int ipv4Byte1;
        [DefaultValue(0)]
        [Range(0, 254, ErrorMessage = "Invalid value")]
        public virtual int IPv4Byte1
        {
            get { return this.ipv4Byte1; }
            set { ApplyPropertyChange<FillingLineDetailDTO, int>(ref this.ipv4Byte1, o => o.IPv4Byte1, value); this.NotifyPropertyChanged("TotalIPv4Byte1"); }
        }

        private int ipv4Byte2;
        [DefaultValue(0)]
        [Range(0, 254, ErrorMessage = "Invalid value")]
        public virtual int IPv4Byte2
        {
            get { return this.ipv4Byte2; }
            set { ApplyPropertyChange<FillingLineDetailDTO, int>(ref this.ipv4Byte2, o => o.IPv4Byte2, value); this.NotifyPropertyChanged("TotalIPv4Byte1"); }
        }

        private int ipv4Byte3;
        [DefaultValue(0)]
        [Range(0, 254, ErrorMessage = "Invalid value")]
        public virtual int IPv4Byte3
        {
            get { return this.ipv4Byte3; }
            set { ApplyPropertyChange<FillingLineDetailDTO, int>(ref this.ipv4Byte3, o => o.IPv4Byte3, value); this.NotifyPropertyChanged("TotalIPv4Byte1"); }
        }

        private int ipv4Byte4;
        [DefaultValue(0)]
        [Range(0, 254, ErrorMessage = "Invalid value")]
        public virtual int IPv4Byte4
        {
            get { return this.ipv4Byte4; }
            set { ApplyPropertyChange<FillingLineDetailDTO, int>(ref this.ipv4Byte4, o => o.IPv4Byte4, value); this.NotifyPropertyChanged("TotalIPv4Byte1"); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<FillingLineDetailDTO>(p => p.DeviceID), "Vui lòng chọn thiết bị.", delegate { return (this.DeviceID > 0); }));

            return validationRules;
        }
    }
}