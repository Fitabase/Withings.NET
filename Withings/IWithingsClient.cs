using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Withings.API.Portable.Models;

namespace Withings.API.Portable
{
    public interface IWithingsClient
    {
        event EventHandler<EventArgs> GetRequestToken;
        string Output { get; set; }

        Task<List<Device>> GetDevicesAsync();
      
    }
}
