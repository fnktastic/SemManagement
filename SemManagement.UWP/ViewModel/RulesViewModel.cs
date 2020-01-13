using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.View.ContentDialogs;
using SemManagement.UWP.ViewModel.ContentDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel
{
    public class RulesViewModel : ViewModelBase
    {
        #region fields
        #endregion

        #region properties
        #endregion

        public RulesViewModel()
        {

        }

        #region private methods

        #endregion

        #region commands
        private RelayCommand _addRuleCommand;
        public RelayCommand AddRuleCommand => _addRuleCommand ?? (_addRuleCommand = new RelayCommand(AddRule));
        private async void AddRule()
        {
            var addRuleViewModel = new AddRuleViewModel();

            var addRuleContentDialog = new AddRuleContentDialog(addRuleViewModel);

            var descision = await addRuleContentDialog.ShowAsync();

            switch (descision)
            {
                case ContentDialogResult.Primary:
                    break;
                case ContentDialogResult.Secondary:
                    break;
                case ContentDialogResult.None:
                    break;
            }
        }
        #endregion
    }
}
