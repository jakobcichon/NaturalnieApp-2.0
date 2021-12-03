using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels.Controls
{
    internal class MenuScreenWithButtonBarViewModels: ViewModelBase
    {
        #region 

        #endregion

        #region Buttons titles
        private ObservableCollection<string> _buttonsTitles;

        public ObservableCollection<string> ButtonsTitles
        {
            get { return _buttonsTitles; }
            set { _buttonsTitles = value; }
        }
        #endregion

        public MenuScreenWithButtonBarViewModels(ObservableCollection<string> buttonsNames)
        {
            _buttonsTitles = buttonsNames;
        }

    }
}
