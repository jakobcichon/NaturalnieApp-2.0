using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces
{
    public interface IHashableModel
    {
        public string? GetHashCodeFromModel();
        public void RegenerateHashOnPropertyChange();
    }
}
