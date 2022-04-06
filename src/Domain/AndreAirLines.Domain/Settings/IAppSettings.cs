using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Settings
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
