using Atata;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtataUITests1
{
    using _ = Page2;

    public class Page2 : Page<_>
    {
        [FindByClass("product-prices__big")]
        public Text<_> Price { get; private set; }
        public Link<Page1, _> Back { get; private set; }

    }
}
