using SemManagement.MonitoringContext.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    [Table("SnapshotEntry")]
    public class SnapshotEntryDto
    {
        [Key]
        public Guid Id { get; set; }

        public SnapshotTypeEnum SnapshotType { get; set; }

        public SnapshotEntryStateEnum EntryState { get; set; }

        public DateTime DateTime { get; set; }

        public SnapshotEntryDto()
        {

        }

        public SnapshotEntryDto(SnapshotTypeEnum snapshotType, SnapshotEntryStateEnum entryState, DateTime dateTime)
        {
            Id = Guid.NewGuid();
            SnapshotType = snapshotType;
            EntryState = entryState;
            DateTime = dateTime;
        }
    }
}
