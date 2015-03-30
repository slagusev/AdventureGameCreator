using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Player.ObjectTypesWrappers
{
    class PlayerStatisticWrapper : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="PlayerStat" /> property's name.
        /// </summary>
        public const string statPropertyName = "PlayerStat";

        private PlayerStatistic _playerStat = null;

        /// <summary>
        /// Sets and gets the stat property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public PlayerStatistic PlayerStat
        {
            get
            {
                return _playerStat;
            }

            set
            {
                if (_playerStat == value)
                {
                    return;
                }

                _playerStat = value;
                RaisePropertyChanged(statPropertyName);
            }
        }

        public PlayerStatisticWrapper(PlayerStatistic ps)
        {
            PlayerStat = ps;
        }
        /// <summary>
        /// The <see cref="IsVisible" /> property's name.
        /// </summary>
        public const string IsVisiblePropertyName = "IsVisible";

        private bool _isVisible = false;

        /// <summary>
        /// Sets and gets the IsVisible property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                if (_isVisible == value)
                {
                    return;
                }

                _isVisible = value;
                RaisePropertyChanged(IsVisiblePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PlaintextValue" /> property's name.
        /// </summary>
        public const string PlaintextValuePropertyName = "PlaintextValue";

        private string _plaintextValue = "";

        /// <summary>
        /// Sets and gets the PlaintextValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string PlaintextValue
        {
            get
            {
                return _plaintextValue;
            }

            set
            {
                if (_plaintextValue == value)
                {
                    return;
                }

                _plaintextValue = value;
                RaisePropertyChanged(PlaintextValuePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ProgressBarValue" /> property's name.
        /// </summary>
        public const string ProgressBarValuePropertyName = "ProgressBarValue";

        private int _progressBarValue = 0;

        /// <summary>
        /// Sets and gets the ProgressBarValue property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int ProgressBarValue
        {
            get
            {
                return _progressBarValue;
            }

            set
            {
                if (_progressBarValue == value)
                {
                    return;
                }

                _progressBarValue = value;
                RaisePropertyChanged(ProgressBarValuePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ProgressBarMin" /> property's name.
        /// </summary>
        public const string ProgressBarMinPropertyName = "ProgressBarMin";

        private int _progressBarMin = 0;

        /// <summary>
        /// Sets and gets the ProgressBarMin property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int ProgressBarMin
        {
            get
            {
                return _progressBarMin;
            }

            set
            {
                if (_progressBarMin == value)
                {
                    return;
                }

                _progressBarMin = value;
                RaisePropertyChanged(ProgressBarMinPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Normalized" /> property's name.
        /// </summary>
        public const string NormalizedPropertyName = "Normalized";

        private double _normalized = 0;

        /// <summary>
        /// Sets and gets the Normalized property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public double Normalized
        {
            get
            {
                return _normalized;
            }

            set
            {
                if (_normalized == value)
                {
                    return;
                }

                _normalized = value;
                RaisePropertyChanged(NormalizedPropertyName);
                ReverseNormalized = -Normalized;
            }
        }

        /// <summary>
        /// The <see cref="ReverseNormalized" /> property's name.
        /// </summary>
        public const string ReverseNormalizedPropertyName = "ReverseNormalized";

        private double _reverseNormalized = 0.0;

        /// <summary>
        /// Sets and gets the ReverseNormalized property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public double ReverseNormalized
        {
            get
            {
                return _reverseNormalized;
            }

            set
            {
                if (_reverseNormalized == value)
                {
                    return;
                }

                _reverseNormalized = value;
                RaisePropertyChanged(ReverseNormalizedPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ProgressBarMax" /> property's name.
        /// </summary>
        public const string ProgressBarMaxPropertyName = "ProgressBarMax";

        private int _progressBarMax = 100;

        /// <summary>
        /// Sets and gets the ProgressBarMax property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int ProgressBarMax
        {
            get
            {
                return _progressBarMax;
            }

            set
            {
                if (_progressBarMax == value)
                {
                    return;
                }

                _progressBarMax = value;
                RaisePropertyChanged(ProgressBarMaxPropertyName);
            }
        }

        public void SetBrush()
        {
            double normalized = (ProgressBarValue - ProgressBarMin) * 1.0 / ProgressBarMax;
            normalized = Normalized;
            if (this.PlayerStat.IsBalanceBar) normalized += .5;
            if (normalized < 0.5 && PlayerStat.LowWarning)
            {
                normalized *= 2;
                //Low = red, High = green
                var value = normalized * 512;
                int redValue = (int)Math.Min(512 - value, 255); // Under 256, All red with some green
                int greenValue = (int)(Math.Min(value,255)); // Over 256 All green with some red
                Color lightColor = Color.FromRgb((byte)redValue, (byte)greenValue, 0);
                Color darkColor = Color.FromRgb((byte)(redValue / 2), (byte)(greenValue / 2), 0);
                ProgressBarColor = new LinearGradientBrush(lightColor, darkColor, 90.0);
            }
            else if (normalized > 0.5 && PlayerStat.HighWarning)
            {
                normalized = (normalized - 0.5) * 2;
                //High = red, Low= green
                var value = normalized * 512;
                int greenValue = (int)Math.Min(512 - value, 255); // Under 256, All green with some red
                int redValue = (int)Math.Min(value, 255); // Over 256 All red with some green
                Color lightColor = Color.FromRgb((byte)redValue, (byte)greenValue, 0);
                Color darkColor = Color.FromRgb((byte)(redValue / 2), (byte)(greenValue / 2), 0);
                ProgressBarColor = new LinearGradientBrush(lightColor, darkColor, 90.0);
            }
            else
            {
                Color lightColor = Color.FromRgb(0, 255, 0);
                Color darkColor = Color.FromRgb(0, 128, 0);
                ProgressBarColor = new LinearGradientBrush(lightColor, darkColor, 90.0);
            }

        }

        /// <summary>
        /// The <see cref="ProgressBarColor" /> property's name.
        /// </summary>
        public const string ProgressBarColorPropertyName = "ProgressBarColor";

        private GradientBrush _progressBarColor = null;

        /// <summary>
        /// Sets and gets the ProgressBarColor property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GradientBrush ProgressBarColor
        {
            get
            {
                return _progressBarColor;
            }

            set
            {
                if (_progressBarColor == value)
                {
                    return;
                }

                _progressBarColor = value;
                RaisePropertyChanged(ProgressBarColorPropertyName);
            }
        }

        public bool GetVisibility()
        {
            var res = new ScriptWrapper(PlayerStat.DisplayCondition).Execute();
            if (res == null) res = true;
            IsVisible = res.Value;
            return IsVisible;
        }

        public void ResetValues()
        {
            try
            {
                GetVisibility();
                if (PlayerStat.IsPlaintext)
                {
                    PlaintextValue = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById[PlayerStat.AssociatedVariable.LinkedVarId].ToString();
                }
                if (PlayerStat.IsProgressBar || PlayerStat.IsBalanceBar)
                {
                    this.ProgressBarValue = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById[PlayerStat.AssociatedVariable.LinkedVarId].CurrentNumberValue;
                    Normalized = ((double)ProgressBarValue) / (ProgressBarMax - ProgressBarMin);
                    if (Normalized < 0) Normalized = 0;
                    if (Normalized > 1) Normalized = 1;
                    if (PlayerStat.IsBalanceBar)
                    {
                        Normalized -= 0.5;

                    }
                    if (this.PlayerStat.MaximumValueConstant)
                    {
                        this.ProgressBarMax = this.PlayerStat.MaximumValueConstantValue;
                    }
                    else
                    {
                        this.ProgressBarMax = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById[PlayerStat.MaximumValueVariableValue.LinkedVarId].CurrentNumberValue;
                    }
                    if (this.PlayerStat.MinimumValueConstant)
                    {
                        this.ProgressBarMin = this.PlayerStat.MinimumValueConstantValue;
                    }
                    else
                    {
                        this.ProgressBarMin = MainViewModel.GetMainViewModelStatic().CurrentGame.VarById[PlayerStat.MinimumValueVariableValue.LinkedVarId].CurrentNumberValue;
                    }
                    this.SetBrush();
                }
            }
            catch (Exception e)
            {

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
