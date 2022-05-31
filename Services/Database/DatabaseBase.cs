using NaturalnieApp.Database;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database
{
    internal class DatabaseBase<T> where T : DatabaseBase<T>
    {

        internal class ModelChangedEventArgs: EventArgs
        {
            public Type ModelType { get; init; }
            public string ModelName { get; init; }

            public ModelChangedEventArgs(Type modelType, string modelName)
            {
                ModelType = modelType;
                ModelName = modelName;
            }
        }

        public delegate void ModelChangedHandler(object sender, ModelChangedEventArgs e);
        public static event ModelChangedHandler ModelChange;

        public static void OnModelChange(object sender, Type modelType, string modelName)
        {
            ModelChangedHandler handler = ModelChange;
            handler?.Invoke(sender, new ModelChangedEventArgs(modelType, modelName));
        }

        private ShopContext shopContext;

        public ShopContext ShopContext
        {
            get { return shopContext; }
            set { shopContext = value; }
        }

        public DatabaseBase(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public enum HistoryOperationType
        {
            AddedNew,
            Deleted,
            Modified
        }

        public T? CheckDatabaseConnection()
        {
            bool connectionStatus = CheckConnection();
            if (connectionStatus)
            { 
                return (T)this;
            }
            return default;
        }

        public async Task<T>? CheckDatabaseConnectionAsync()
        {
            bool connectionStatus = await CheckConnectionAsync();
            if (connectionStatus)
            {
                return (T)this;
            }
            return default;
        }

        private async Task<bool> CheckConnectionAsync()
        {
            return await Task.Run(() =>
                {
                    return shopContext.Database.Exists();
                }
            );
            
        }

        private bool CheckConnection()
        {

            return shopContext.Database.Exists();

        }
    }
}
