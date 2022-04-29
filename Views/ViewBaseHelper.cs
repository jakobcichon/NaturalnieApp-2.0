using NaturalnieApp2.Services.VisualTreeHelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Views
{
    public class ViewBaseHelper
    {
        public int SearchIdentLevel {get; set;}

        private UserControl ParentClass { get;}

        public ViewBaseHelper(UserControl parentClass)
        {
            SearchIdentLevel = 20;
            ParentClass = parentClass;
            ParentClass.Loaded += ParentClass_Loaded;
            ParentClass.Focusable = true;
        }

        private void ParentClass_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            List<Button>? childList = VisualTreeHelperServices.GetChildOfType<Button>(ParentClass, SearchIdentLevel);
            if(childList != null)
            {
                foreach(Button button in childList)
                {
                    button.Click += Button_Click;
                }    
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParentClass.Focus();
        }

    }
}
