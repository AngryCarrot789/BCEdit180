﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BCEdit180.LineNumbers {
    public class LineNumberAdorder : Adorner {
        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
        }

        public LineNumberAdorder(UIElement adornedElement) : base(adornedElement) {

        }
    }
}
