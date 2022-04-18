using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutBugs
{
    public class DataTemplateByTextSelector : DataTemplateSelector
    {
        static public string Text { get; set; } = "TopAllignedTextBlockFixedText";

        public DataTemplate TopAllignedTextBlockFixedText { get; set; }
        public DataTemplate LeftAllignedTextBoxWithHorizontalLinesText { get; set; }
        public DataTemplate StretchAllignedGridTextBoxWithHorizontalLinesText { get; set; }
        public DataTemplate TopAllignedTextBoxWithVerticalLinesText { get; set; }
        public DataTemplate StretchAllignedGridTextBoxWithVerticalLinesText { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (Text == "TopAllignedTextBlockFixedText") return TopAllignedTextBlockFixedText;
            if (Text == "LeftAllignedTextBoxWithHorizontalLinesText") return LeftAllignedTextBoxWithHorizontalLinesText;
            if (Text == "StretchAllignedGridTextBoxWithHorizontalLinesText") return StretchAllignedGridTextBoxWithHorizontalLinesText;
            if (Text == "TopAllignedTextBoxWithVerticalLinesText") return TopAllignedTextBoxWithVerticalLinesText;
            if (Text == "StretchAllignedGridTextBoxWithVerticalLinesText") return StretchAllignedGridTextBoxWithVerticalLinesText;
            throw new NotSupportedException();
        }

    }
}
