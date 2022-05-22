using NaturalnieApp2.Commands;
using NaturalnieApp2.Models;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NaturalnieApp2.ViewModels.MenuScreens.Product
{
    internal class ShowProductViewModel: NotifyPropertyChanged
    {
        public BottomButtonBarModel BottomButtonPanel { get; }

        public ShowProductViewModel()
        {
            BottomButtonPanel = new BottomButtonBarModel();
            BottomButtonPanel.LeftButtons.Add(new SignleButtonModel("Zamknij",
                new BottomButtonPanelCommands(), OnCloseViewAction));

            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Odśwież",
                new BottomButtonPanelCommands(), OnRefreshAction));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Zapisz",
                new BottomButtonPanelCommands(), OnSaveAction));
        }

        #region Public methods
        public void OnCloseViewAction()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zamknąc okno?", "Zamknięcie okna", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) CloseScreen(this);
        }

        public void OnRefreshAction()
        {

        }

        public void OnSaveAction()
        {

        }
        #endregion
    }
}
