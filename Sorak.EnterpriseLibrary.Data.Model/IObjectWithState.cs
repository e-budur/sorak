using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sorak.EnterpriseLibrary.Data.Model
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }
    }
}
