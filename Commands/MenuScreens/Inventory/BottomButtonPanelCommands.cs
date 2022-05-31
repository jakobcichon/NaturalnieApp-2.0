using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Commands.MenuScreens.Inventory
{
    internal class BottomButtonPanelCommands : CommandBase
    {
        public override void Execute(object? parameter)
        {
            SignleButtonModel localSender = parameter as SignleButtonModel;

            if (localSender == null) return;

            localSender.AdditionalAction();
        }
    }
}
