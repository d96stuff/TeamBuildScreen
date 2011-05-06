using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBuildScreen.Core.Models
{
    public interface IDomainProjectPicker
    {
        bool Show();

        string TfsUri { get; }
    }
}
