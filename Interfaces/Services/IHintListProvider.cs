using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Services
{
    internal interface IHintListProvider<T>
    {
        /// <summary>
        /// It's provides the hint list.
        /// Hint list is used when given model can by used as a available opions for combobox
        /// </summary>
        /// <returns></returns>
        T? GetHintList();
    }
}
