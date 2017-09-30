using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using MouseWarpTool.Models;
using Livet.Messaging.Windows;

namespace MouseWarpTool.ViewModels
{
    class SettingViewModel : ViewModel
    {
        private ConfigRepository _configRepository;

        public SettingViewModel()
        {
            _configRepository = ServiceLocator.Instance.GetInstance<ConfigRepository>();

            IgnitionTime = _configRepository.GetConfig().IgnitionTime;
        }

        private double _ignitionTime;
        public double IgnitionTime
        {
            get { return _ignitionTime; }
            set { _ignitionTime = Math.Round(value, 1, MidpointRounding.AwayFromZero); this.RaisePropertyChanged("IgnitionTime"); }
        }

        private void SaveSetting()
        {
            var setting = _configRepository.GetConfig();
            setting.IgnitionTime = IgnitionTime;
            _configRepository.Save(setting);
        }

        public void OnClose()
        {
            SaveSetting();
        }
    }
}
