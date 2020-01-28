using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace SemManagement.UWP.Helper
{
    public class DeferredAction : IDisposable
    {
        private Timer timer;

        public void Defer(TimeSpan delay)
        {
            this.timer.Change(delay, TimeSpan.FromMilliseconds(-1));
        }

        public static DeferredAction Create(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new DeferredAction(action);
        }

        private DeferredAction(Action action)
        {
            this.timer = new Timer(new TimerCallback(async delegate
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, () => action());
            }));
        }

        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }
        }
    }
}
