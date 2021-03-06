using NaturalnieApp2.Attributes;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Database
{
    public interface IGetModelProvider
    {
        public List<string> GetElementsNames();
        public Dictionary<string, object> GetElementsValues();
        public Task<Dictionary<string, object>> GetElementsValuesAsync();
    }
}
