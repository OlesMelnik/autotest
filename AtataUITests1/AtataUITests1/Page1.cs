using Atata;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtataUITests1
{
    using _ = Page1;

    public class Page1 : Page<_>
    {
        [FindByClass("search-form__input")]
        public TextInput<_> Search { get; private set; }

        [FindByClass("search-form__submit")]
        public Button<_> SearchButton { get; private set; }

        [FindByClass("goods-tile__price")]
        public Text<_> Price { get; private set; }

        [FindByClass("goods-tile__inner")]
        public Link<_> Good { get; private set; }
    }
}
