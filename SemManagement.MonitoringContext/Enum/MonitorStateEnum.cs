﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.Enum
{
    public enum MonitorStateEnum
    {
        Started,
        Finished
    }

    public enum MonitorTypeEnum
    {
        Stations,
        Playlists,
        Rules
    }

    public enum SnapshotDirection
    {
        In,
        Out
    }
}
