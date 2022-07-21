using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.FlagEditor {
    public class FlagEditorViewModel : BaseViewModel {
        public ObservableCollection<FlagItemViewModel> FlagItems { get; }

        private object enumValue;
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

        public Action<object> UpdateEnumCallback { get; set; }

        public FlagEditorViewModel(Func<long, Enum> maskToEnum) {
            this.FlagItems = new ObservableCollection<FlagItemViewModel>();
            this.MaskToEnum = maskToEnum;
        }

        public void OnFlagChanged(FlagItemViewModel flagItem) {
            if (flagItem.IsChecked) {
                this.BitMask |= flagItem.Bit;
            }
            else {
                this.BitMask &= ~flagItem.Bit;
            }

            UpdateEnumPreview();
        }

        public void UpdateEnumPreview() {
            this.PreviewEnumValue = this.MaskToEnum(this.BitMask);
        }

        public void InvokeEnumCallback() {
            this.UpdateEnumCallback?.Invoke(GetEnumValue());
        }

        public void UpdateFlagItemsWithBitMask(long mask, Func<TEnum, long> toLong) where TEnum : Enum {
            for (int index = 0; index < 64; index++) {
                long bit = index & mask;
                if (bit )
            }

            foreach (FlagItemViewModel flagItem in this.FlagItems) {
                long value = (i++ & mask);
                flagItem.IsChecked = value != 0;
            }

            Enum.

            UpdateEnumPreview();
        }

        public void LoadFlags<TEnum>(Func<TEnum, long> enumToBit) where TEnum : Enum {
            Type type = typeof(TEnum);
            this.FlagItems.Clear();

            int GetBitIndex(long value) {
                int i = 0;
                while (value > 0) {
                    value >>= 1;
                    i++;
                }

                return i;
            }

            foreach (TEnum value in type.GetEnumValues()) {
                long val = enumToBit(value);
                this.FlagItems.Add(new FlagItemViewModel(Enum.GetName(type, value), val, GetBitIndex(val), OnFlagChanged));
            }
        }

        public object GetEnumValue() {
            return this.MaskToEnum(this.BitMask);
        }
    }
}
