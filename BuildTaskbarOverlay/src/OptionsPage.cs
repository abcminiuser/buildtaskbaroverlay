﻿using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace FourWalledCubicle.BuildTaskbarOverlay
{
    public class OptionsPage : DialogPage
    {
        private IconTheme.IconThemes mIconTheme = IconTheme.IconThemes.ICON_THEME_1;

        private bool mUseProgressBar = true;

        [DisplayName("Icon Overlay Theme")]
        [Description("Select the icon overlay theme to use on the taskbar while building.")]
        [TypeConverter(typeof(EnumDescriptionConverter))]
        public IconTheme.IconThemes Theme
        {
            get { return mIconTheme; }
            set { mIconTheme = value; }
        }

        [DisplayName("Use Progress Bar")]
        [Description("If enabled, a progress-bar style will be applied to the taskbar while building.")]
        public bool UseProgressBar
        {
            get { return mUseProgressBar; }
            set { mUseProgressBar = value; }
        }
    
    }
}
