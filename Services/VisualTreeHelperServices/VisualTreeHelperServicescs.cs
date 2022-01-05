using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NaturalnieApp2.Services.VisualTreeHelperServices
{
    public static class VisualTreeHelperServicescs
    {
        public static List<T>? GetChildOfType<T>(DependencyObject depObj, int identLevel = 1)
        where T : DependencyObject
        {
            identLevel--;
            if (identLevel < 0) return null;

            List<T> list = new List<T>();

            if (depObj == null) return null;

            int test = VisualTreeHelper.GetChildrenCount(depObj);

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var test2 = VisualTreeHelper.GetParent(child);

                if (child is T) list.Add((T)child);
                List<T>? childList = GetChildOfType<T>(child, identLevel);
                if (childList != null)
                {
                    list.AddRange(childList);
                }

            }
            return list;
        }
    }
}
