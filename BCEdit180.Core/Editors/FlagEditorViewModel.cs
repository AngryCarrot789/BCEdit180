﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.Dialog;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class FlagEditorViewModel : BaseDialogViewModel {
        private bool ignoreFlagChanges;

        public ObservableCollection<FlagItemViewModel> FlagItems { get; }

        private readonly Dictionary<long, FlagItemViewModel> bitToFlag;

        private long bitMask;

        public long BitMask {
            get => this.bitMask;
            set => RaisePropertyChanged(ref this.bitMask, value);
        }

        private object previewEnumValue;
        public object PreviewEnumValue {
            get => this.previewEnumValue;
            set => RaisePropertyChanged(ref this.previewEnumValue, value);
        }

        private Func<long, Enum> MaskToEnum { get; }

        public FlagEditorViewModel(Func<long, Enum> maskToEnum) {
            this.FlagItems = new ObservableCollection<FlagItemViewModel>();
            this.MaskToEnum = maskToEnum;
            this.bitToFlag = new Dictionary<long, FlagItemViewModel>();
        }

        public void OnFlagChanged(FlagItemViewModel flagItem) {
            if (this.ignoreFlagChanges) {
                return;
            }

            if (flagItem.IsChecked) {
                this.BitMask |= flagItem.Bit;
            }
            else {
                this.BitMask &= ~flagItem.Bit;
            }

            UpdateEnumPreview();
        }

        public void UpdateEnumPreview() {
            this.PreviewEnumValue = GetEnumValue();
        }

        public void LoadFlags<TEnum>(Func<TEnum, long> enumToBit) where TEnum : Enum {
            Type type = typeof(TEnum);
            this.FlagItems.Clear();

            foreach (TEnum value in type.GetEnumValues()) {
                FlagItemViewModel flag = new FlagItemViewModel(Enum.GetName(type, value), enumToBit(value), OnFlagChanged);
                this.FlagItems.Add(flag);
                this.bitToFlag[flag.Bit] = flag;
            }
        }

        public object GetEnumValue() {
            return this.MaskToEnum(this.BitMask);
        }

        public void UpdateFlagItemsWithBitMask(long bitMask) {
            this.ignoreFlagChanges = true;
            foreach (FlagItemViewModel flag in this.FlagItems) {
                flag.IsChecked = false;
            }

            this.ignoreFlagChanges = false;

            long j = 1;
            for (int i = 0; i <= bitMask; j = 1 << ++i) {
                if (this.bitToFlag.TryGetValue(j, out FlagItemViewModel flag)) {
                    flag.IsChecked = (bitMask & j) != 0;
                }
            }

            // go through each bit of bitMask, check if the specific bit part exists
            // and update it
        }
    }
}
