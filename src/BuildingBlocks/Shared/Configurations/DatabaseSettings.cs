using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Configurations
{
    public class DatabaseSettings
    {
        public string DBProvider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
