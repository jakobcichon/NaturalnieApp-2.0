using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployRepository.API.ConfigurationFile
{
    internal static class ConfigurationFileMapper
    {
        public static Tmodel Map<Tmodel>(Func<FileStream, Tmodel> mappingFunction)
        {
            return mappingFunction;
        }

        public static FileStream GetFileStream(string filePath) 
        {
            return File.OpenRead(filePath);
        }
    }
}
