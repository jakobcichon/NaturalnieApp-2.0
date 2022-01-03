using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Views.Controls.Models
{
    internal interface IProductSelectorHandler
    {
        public void OnFilterRequest(string elementName, object elementValue);
        public void OnElementSelected();
        public void OnClearFilterRequest();
        public void OnProductSelectorLoaded();

        /// <summary>
        /// 
        /// </summary>
        public Action<bool> OnDataFiltered { get; set; }
    }
}
