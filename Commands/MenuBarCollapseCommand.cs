using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Commands
{
    internal class MenuBarCollapseCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            ItemsControl castedParameter = parameter as ItemsControl;
        }
    }
}
