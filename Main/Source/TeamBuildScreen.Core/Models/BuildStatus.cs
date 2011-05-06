using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBuildScreen.Core.Models
{
    public enum BuildStatus
    {
        Stopped,
        Succeeded,
        Failed,
        InProgress,
        NotStarted,
        PartiallySucceeded,
        Loading,
        NoneFound
    }
}