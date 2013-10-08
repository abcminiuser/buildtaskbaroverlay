using System.ComponentModel;
using System.Drawing;

namespace FourWalledCubicle.BuildTaskbarOverlay
{
    public static class IconTheme
    {
        public enum IconThemes
        {
            [Description("Minimal")]
            ICON_THEME_1,

            [Description("Vista")]
            ICON_THEME_2,

            [Description("Soft")]
            ICON_THEME_3,
        };

        public static Icon IconBuilding(IconThemes theme)
        {
            switch (theme)
            {
                case IconThemes.ICON_THEME_1:
                    return Resources.Building;
                case IconThemes.ICON_THEME_2:
                    return Resources.Building2;
                case IconThemes.ICON_THEME_3:
                    return Resources.Building3;
            }

            return null;
        }

        public static Icon IconBuildOK(IconThemes theme)
        {
            switch (theme)
            {
                case IconThemes.ICON_THEME_1:
                    return Resources.BuildOK;
                case IconThemes.ICON_THEME_2:
                    return Resources.BuildOK2;
                case IconThemes.ICON_THEME_3:
                    return Resources.BuildOK3;
            }

            return null;
        }

        public static Icon IconBuildWarning(IconThemes theme)
        {
            switch (theme)
            {
                case IconThemes.ICON_THEME_1:
                    return Resources.BuildWarning;
                case IconThemes.ICON_THEME_2:
                    return Resources.BuildWarning;
                case IconThemes.ICON_THEME_3:
                    return Resources.BuildWarning;
            }

            return null;
        }

        public static Icon IconBuildFail(IconThemes theme)
        {
            switch (theme)
            {
                case IconThemes.ICON_THEME_1:
                    return Resources.BuildFail;
                case IconThemes.ICON_THEME_2:
                    return Resources.BuildFail2;
                case IconThemes.ICON_THEME_3:
                    return Resources.BuildFail3;
            }

            return null;
        }
    }
}
