using System;
using System.Windows.Controls;
using BCEdit180.Core.Editors;

namespace BCEdit180.CodeEditing.Viewers {
    public class HandleControl : Control {
        public HandleViewModel Handle {
            get => (HandleViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        private static double[] roundPower10Double = new double[16] {
            1.0,
            10.0,
            100.0,
            1000.0,
            10000.0,
            100000.0,
            1000000.0,
            10000000.0,
            100000000.0,
            1000000000.0,
            10000000000.0,
            100000000000.0,
            1000000000000.0,
            10000000000000.0,
            100000000000000.0,
            1E+15
        };

        public HandleControl() {
            // i want 97500.123
            double value = 97500.12345d;
            int places = 3;

            // 1000.0
            double thing = roundPower10Double[places];

            // 97500
            int intvalue = (int) value;

            // 0.12345
            double decimalPart = value - intvalue;

            // 123.45
            double aaa = decimalPart * thing;

            double finalValue = (double) intvalue + ((double) aaa / thing);
        }
    }
}