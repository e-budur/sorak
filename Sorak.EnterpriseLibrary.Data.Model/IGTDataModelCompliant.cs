using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sorak.EnterpriseLibrary.Data.Model
{
    public interface IDataModelCompliant
    {
        string CreateUserId { get; set; }
        DateTime InstanceId { get; set; }

    }
}
