using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public ViewModelBase ContentViewModel { get; }

        public LayoutViewModel( ViewModelBase contentViewModel)
        {

            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}
