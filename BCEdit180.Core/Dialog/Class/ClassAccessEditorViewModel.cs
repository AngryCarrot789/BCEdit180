using JavaAsm;

namespace BCEdit180.Core.Dialog.Class {
    public class ClassAccessEditorViewModel : BaseDialogViewModel {
        private bool flagPublic;
        private bool flagProtected;
        private bool flagPrivate;
        private bool flagStatic;
        private bool flagAbstract;
        private bool flagFinal;
        private bool flagStrict;
        private bool flagAnnotation;
        private bool flagEnum;
        private bool flagInterface;
        private bool flagSuper;
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

        public bool FlagFinal {
            get => this.flagFinal;
            set => RaisePropertyChanged(ref this.flagFinal, value);
        }

        public bool FlagStrict {
            get => this.flagStrict;
            set => RaisePropertyChanged(ref this.flagStrict, value);
        }

        public bool FlagAnnotation {
            get => this.flagAnnotation;
            set => RaisePropertyChanged(ref this.flagAnnotation, value);
        }

        public bool FlagEnum {
            get => this.flagEnum;
            set => RaisePropertyChanged(ref this.flagEnum, value);
        }

        public bool FlagInterface {
            get => this.flagInterface;
            set => RaisePropertyChanged(ref this.flagInterface, value);
        }

        public bool FlagSuper {
            get => this.flagSuper;
            set => RaisePropertyChanged(ref this.flagSuper, value);
        }

        public bool FlagSynthetic {
            get => this.flagSynthetic;
            set => RaisePropertyChanged(ref this.flagSynthetic, value);
        }

        public int RawModifiers {
            get {
                int value = 0;
                value |= this.flagPublic     ? 1 : 0;
                value |= this.flagProtected  ? 4 : 0;
                value |= this.flagPrivate    ? 2 : 0;
                value |= this.flagStatic     ? 8 : 0;
                value |= this.flagAbstract   ? 1024 : 0;
                value |= this.flagFinal      ? 16 : 0;
                value |= this.flagStrict     ? 2048 : 0;
                value |= this.flagAnnotation ? 8192 : 0;
                value |= this.flagEnum       ? 16384 : 0;
                value |= this.flagInterface  ? 512 : 0;
                value |= this.flagSuper      ? 32 : 0;
                value |= this.flagSynthetic  ? 4096 : 0;
                return value;
            }
            set {
                this.FlagPublic     = (value & 1) != 0;
                this.FlagProtected  = (value & 4) != 0;
                this.FlagPrivate    = (value & 2) != 0;
                this.FlagStatic     = (value & 8) != 0;
                this.FlagAbstract   = (value & 1024) != 0;
                this.FlagFinal      = (value & 16) != 0;
                this.FlagStrict     = (value & 2048) != 0;
                this.FlagAnnotation = (value & 8192) != 0;
                this.FlagEnum       = (value & 16384) != 0;
                this.FlagInterface  = (value & 512) != 0;
                this.FlagSuper      = (value & 32) != 0;
                this.FlagSynthetic  = (value & 4096) != 0;
            }
        }

        public ClassAccessModifiers Modifiers {
            get => (ClassAccessModifiers) this.RawModifiers;
            set => this.RawModifiers = (int) value;
        }
    }
}