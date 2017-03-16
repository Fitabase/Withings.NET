using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.OAuth1
{
    public interface ITokenManager
    {
        Task<OAuth1RequestToken> AccessTokenAsync(WithingsClient client);
    }
}
