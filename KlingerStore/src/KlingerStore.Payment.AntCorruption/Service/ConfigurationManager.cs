using KlingerStore.Payment.AntCorruption.Interfaces;
using System;
using System.Linq;

namespace KlingerStore.Payment.AntCorruption.Service
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string GetValue(string node)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXZ0123456789", 10).Select(x => x[new Random().Next(x.Length)]).ToArray());
        }
    }
}
