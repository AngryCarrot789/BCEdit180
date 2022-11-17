using JavaAsm;

namespace BCEdit180.Core.Dialog.Methods {
    public class MethodAccessEditorViewModel : BaseDialogViewModel {
        private bool flagPublic;
        private bool flagProtected;
        private bool flagPrivate;
        private bool flagStatic;
        private bool flagAbstract;
        private bool flagSyncrionized;
        private bool flagFinal;
        private bool flagNative;
        private bool flagStrict;
        private bool flagBridge;
        private bool flagSynthetic;
        private bool flagVarargs;

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
            set {
                RaisePropertyChanged(ref this.flagStatic, value);
                if (value) {
                    this.FlagAbstract = false;
                }
            }
        }

        public bool FlagAbstract {
            get => this.flagAbstract;
            set {
                RaisePropertyChanged(ref this.flagAbstract, value);
                if (value) {
                    this.FlagStatic = false;
                }
            }
        }

        public bool FlagSyncrionized {
            get => this.flagSyncrionized;
            set => RaisePropertyChanged(ref this.flagSyncrionized, value);
        }

        public bool FlagFinal {
            get => this.flagFinal;
            set => RaisePropertyChanged(ref this.flagFinal, value);
        }

        public bool FlagNative {
            get => this.flagNative;
            set => RaisePropertyChanged(ref this.flagNative, value);
        }

        public bool FlagStrict {
            get => this.flagStrict;
            set => RaisePropertyChanged(ref this.flagStrict, value);
        }

        public bool FlagBridge {
            get => this.flagBridge;
            set => RaisePropertyChanged(ref this.flagBridge, value);
        }

        public bool FlagSynthetic {
            get => this.flagSynthetic;
            set => RaisePropertyChanged(ref this.flagSynthetic, value);
        }

        public bool FlagVarargs {
            get => this.flagVarargs;
            set => RaisePropertyChanged(ref this.flagVarargs, value);
        }

        public int RawModifiers {
            get {
                int value = 0;
                value |= this.flagPublic       ? 1    : 0;
                value |= this.flagProtected    ? 4    : 0;
                value |= this.flagPrivate      ? 2    : 0;
                value |= this.flagStatic       ? 8    : 0;
                value |= this.flagAbstract     ? 1024 : 0;
                value |= this.flagSyncrionized ? 32   : 0;
                value |= this.flagFinal        ? 16   : 0;
                value |= this.flagNative       ? 256  : 0;
                value |= this.flagStrict       ? 2048 : 0;
                value |= this.flagBridge       ? 64   : 0;
                value |= this.flagSynthetic    ? 4096 : 0;
                value |= this.flagVarargs      ? 128  : 0;
                return value;
            }
            set {
                this.FlagPublic       = (value & 1) != 0;
                this.FlagProtected    = (value & 4) != 0;
                this.FlagPrivate      = (value & 2) != 0;
                this.FlagStatic       = (value & 8) != 0;
                this.FlagAbstract     = (value & 1024) != 0;
                this.FlagSyncrionized = (value & 32) != 0;
                this.FlagFinal        = (value & 16) != 0;
                this.FlagNative       = (value & 256) != 0;
                this.FlagStrict       = (value & 2048) != 0;
                this.FlagBridge       = (value & 64) != 0;
                this.FlagSynthetic    = (value & 4096) != 0;
                this.FlagVarargs      = (value & 128) != 0;
            }
        }

        public MethodAccessModifiers Modifiers {
            get => (MethodAccessModifiers) this.RawModifiers;
            set => this.RawModifiers = (int) value;
        }
    }
}