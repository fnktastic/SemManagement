using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Rule : ViewModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public bool IsDraft { get; set; }

        public bool IsRepeat { get; set; }

        public bool AllStations { get; set; }

        public ICollection<Playlist> SourcePlaylists { get; set; }

        public ICollection<Playlist> TargetPlaylists { get; set; }

        public ICollection<Station> Stations { get; set; }


        private bool _isRuleInProcess = false;
        [NotMapped]
        public bool IsRuleInProcess
        {
            get { return _isRuleInProcess; }
            set
            {
                if (value == _isRuleInProcess) return;
                _isRuleInProcess = value;
                RaisePropertyChanged(nameof(IsRuleInProcess));

            }
        }
    }
}
