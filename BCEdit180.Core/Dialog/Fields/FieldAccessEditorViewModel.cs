using JavaAsm;

namespace BCEdit180.Core.Dialog.Fields {
    public class FieldAccessEditorViewModel : BaseDialogViewModel {
        private bool flagPublic;
        private bool flagProtected;
        private bool flagPrivate;
        private bool flagStatic;
        private bool flagTransient;
        private bool flagVolatile;
        private bool flagFinal;
        private bool flagSynthetic;

        public bool FlagPublic {
            get => this.flagPublic;
            set {
                RaisePropertyChanged(ref this.flagPublic, value);
                if (value) {
                    this.FlagProtected = this.FlagPrivate = false;
                }
            }
        }

        public bool FlagProtected {
            get => this.flagProtected;
            set {
                RaisePropertyChanged(ref this.flagProtected, value);
                if (value) {
                    this.FlagPublic = this.FlagPrivate = false;
                }
            }
        }

        public bool FlagPrivate {
            get => this.flagPrivate;
            set {
                RaisePropertyChanged(ref this.flagPrivate, value);
                if (value) {
                    this.FlagPublic = this.FlagProtected = false;
                }
            }
        }

        public bool FlagStatic {
            get => this.flagStatic;
            set => RaisePropertyChanged(ref this.flagStatic, value);
        }

        public bool FlagTransient {
            get => this.flagTransient;
            set => RaisePropertyChanged(ref this.flagTransient, value);
        }

        public bool FlagVolatile {
            get => this.flagVolatile;
            set => RaisePropertyChanged(ref this.flagVolatile, value);
        }

        public bool FlagFinal {
            get => this.flagFinal;
            set => RaisePropertyChanged(ref this.flagFinal, value);
        }

        public bool FlagSynthetic {
            get => this.flagSynthetic;
            set => RaisePropertyChanged(ref this.flagSynthetic, value);
        }

        public int RawModifiers {
            get {
                int value = 0;
                value |= this.flagPublic    ? 1    : 0;
                value |= this.flagProtected ? 4    : 0;
                value |= this.flagPrivate   ? 2    : 0;
                value |= this.flagStatic    ? 8    : 0;
                value |= this.flagTransient ? 128  : 0;
                value |= this.flagVolatile  ? 64   : 0;
                value |= this.flagFinal     ? 16   : 0;
                value |= this.flagSynthetic ? 4096 : 0;
                return value;
            }
            set {
                this.FlagPublic    = (value & 1) != 0;
                this.FlagProtected = (value & 4) != 0;
                this.FlagPrivate   = (value & 2) != 0;
                this.FlagStatic    = (value & 8) != 0;
                this.FlagTransient = (value & 128) != 0;
                this.FlagVolatile  = (value & 64) != 0;
                this.FlagFinal     = (value & 16) != 0;
                this.FlagSynthetic = (value & 4096) != 0;
            }
        }

        public FieldAccessModifiers Modifiers {
            get => (FieldAccessModifiers) this.RawModifiers;
            set => this.RawModifiers = (int) value;
        }
    }
}