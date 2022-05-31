using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class ProvidersGroup
    {
        private List<object> providers;
        private string groupName;

        public string GroupName
        {
            get { return groupName; }
            init { groupName = value; }
        }

        public ProvidersGroup(string groupName = "")
        {
            GroupName = groupName;
            providers = new List<object>();
        }

        public void AddProviders(List<object> providers)
        {
            foreach (object provider in providers)
            {
                AddProvider(provider);
            }
        }

        public void AddProvider<T>(T provider) where T : notnull
        {
            if (CheckIfExist(typeof(T)))
            {
                throw new Exception($"Element type {typeof(T)} already exist on the list!");
            }
            providers.Add(provider);
        }

        public T GetProvider<T>() where T: class
        {
            T foundProviders = (T)providers.Where(provider => provider.GetType() == typeof(T)).First();
            if (foundProviders == null)
            {
                throw new ArgumentNullException("provider");
            }

            return foundProviders;
        }

        private bool CheckIfExist(Type providerType)
        {
            if(providers.Exists(e => e.GetType() == providerType))
            {
                return true;
            }
            return false;
        }
    }
}
