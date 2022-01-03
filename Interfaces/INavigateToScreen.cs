﻿using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces
{
    public interface INavigateToScreen
    {
        public IHostScreen HostScreen { get; set; }
        public void Navigate(ViewModelBase screenToNavigate);
        public void CloseScreen(ViewModelBase screenToClose);
        public void AddHostScreen(IHostScreen hostScreen);
    }
}
