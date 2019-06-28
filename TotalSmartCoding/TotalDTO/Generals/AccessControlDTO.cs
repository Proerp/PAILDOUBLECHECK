﻿using TotalModel.Helpers;

namespace TotalDTO.Generals
{
    public class AccessControlDTO : NotifyPropertyChangeObject
    {
        public bool NoAccess
        {
            get { return this.AccessLevel == 0; }
            set
            {
                if (this.NoAccess != value)
                {
                    this.AccessLevel = 0;
                    this.NotifyPropertyChanged("NoAccess");
                    this.NotifyPropertyChanged("ReadOnly");
                    this.NotifyPropertyChanged("Editable");
                }
            }
        }
        public bool ReadOnly
        {
            get { return this.AccessLevel == 1; }
            set
            {
                if (this.ReadOnly != value)
                {
                    this.AccessLevel = 1;
                    this.NotifyPropertyChanged("NoAccess");
                    this.NotifyPropertyChanged("ReadOnly");
                    this.NotifyPropertyChanged("Editable");
                }
            }
        }
        public bool Editable
        {
            get { return this.AccessLevel == 2; }
            set
            {
                if (this.Editable != value)
                {
                    this.AccessLevel = 2;
                    this.NotifyPropertyChanged("NoAccess");
                    this.NotifyPropertyChanged("ReadOnly");
                    this.NotifyPropertyChanged("Editable");
                }
            }
        }

        public int AccessLevel { get; set; }

        private bool approvalPermitted;
        public bool ApprovalPermitted
        {
            get { return this.approvalPermitted; }
            set { ApplyPropertyChange<AccessControlDTO, bool>(ref this.approvalPermitted, o => o.ApprovalPermitted, value); }
        }

        private bool unApprovalPermitted;
        public bool UnApprovalPermitted
        {
            get { return this.unApprovalPermitted; }
            set { ApplyPropertyChange<AccessControlDTO, bool>(ref this.unApprovalPermitted, o => o.UnApprovalPermitted, value); }
        }

        private bool voidablePermitted;
        public bool VoidablePermitted
        {
            get { return this.voidablePermitted; }
            set { ApplyPropertyChange<AccessControlDTO, bool>(ref this.voidablePermitted, o => o.VoidablePermitted, value); }
        }

        private bool unVoidablePermitted;
        public bool UnVoidablePermitted
        {
            get { return this.unVoidablePermitted; }
            set { ApplyPropertyChange<AccessControlDTO, bool>(ref this.unVoidablePermitted, o => o.UnVoidablePermitted, value); }
        }

        private bool showDiscount;
        public bool ShowDiscount
        {
            get { return this.showDiscount; }
            set { ApplyPropertyChange<AccessControlDTO, bool>(ref this.showDiscount, o => o.ShowDiscount, value); }
        }
    }
}
