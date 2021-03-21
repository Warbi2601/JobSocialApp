using System;
using Xamarin.Forms;

namespace JobSocialApp.Models
{
    public static class ResourcesHelper
    {
        public const string DynamicPrimaryTextColor = nameof(DynamicPrimaryTextColor);
        public const string DynamicPrimaryBackColour = nameof(DynamicPrimaryBackColour);
        public const string DynamicPrimaryMenuBackColour = nameof(DynamicPrimaryMenuBackColour);
        public const string DynamicPrimaryTopMenuBackColour = nameof(DynamicPrimaryTopMenuBackColour);

        public const string DynamicPrimaryBtnTextColor = nameof(DynamicPrimaryBtnTextColor);
        public const string DynamicPrimaryBtnPositiveBackColour = nameof(DynamicPrimaryBtnPositiveBackColour);
        public const string DynamicPrimaryBtnNegativeBackColour = nameof(DynamicPrimaryBtnNegativeBackColour);
        public const string DynamicPrimaryBtnNeutralBackColour = nameof(DynamicPrimaryBtnNeutralBackColour);

        public const string DynamicInputTextColour = nameof(DynamicInputTextColour);
        public const string DynamicBigHeaderTextColour = nameof(DynamicBigHeaderTextColour);
        public const string DynamicMediumHeaderTextColour = nameof(DynamicMediumHeaderTextColour);

        public const string DynamicThemeTextColour = nameof(DynamicThemeTextColour);
        public const string DynamicThemeBorderColour = nameof(DynamicThemeBorderColour);
        public const string DynamicThemeDarkPinkColour = nameof(DynamicThemeDarkPinkColour);

        public static void SetDynamicResource(string targetResourceName, string sourceResourceName)
        {
            if (!Application.Current.Resources.TryGetValue(sourceResourceName, out var value))
            {
                throw new InvalidOperationException($"key {sourceResourceName} not found in the resource dictionary");
            }

            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDynamicResource<T>(string targetResourceName, T value)
        {
            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetLightMode()
        {
            SetDynamicResource(DynamicPrimaryTextColor, "LightTextColour");
            SetDynamicResource(DynamicPrimaryBackColour, "LightBackColour");
            SetDynamicResource(DynamicPrimaryMenuBackColour, "LightMenuBackColour");
            SetDynamicResource(DynamicPrimaryTopMenuBackColour, "LightTopMenuBackColour");

            SetDynamicResource(DynamicPrimaryBtnTextColor, "LightBtnTextColour");
            SetDynamicResource(DynamicPrimaryBtnPositiveBackColour, "LightBtnPositiveBackColour");
            SetDynamicResource(DynamicPrimaryBtnNegativeBackColour, "LightBtnNegativeBackColour");
            SetDynamicResource(DynamicPrimaryBtnNeutralBackColour, "LightBtnNeutralBackColour");

            SetDynamicResource(DynamicInputTextColour, "LightInputTextColour");
            SetDynamicResource(DynamicBigHeaderTextColour, "LightBigHeaderTextColour");
            SetDynamicResource(DynamicMediumHeaderTextColour, "LightMediumHeaderTextColour");

            SetDynamicResource(DynamicThemeTextColour, "LightThemeTextColour");
            SetDynamicResource(DynamicThemeBorderColour, "LightThemeTextColour");
            SetDynamicResource(DynamicThemeDarkPinkColour, "LightThemeDarkPinkColour");
        }

        public static void SetDarkMode()
        {
            SetDynamicResource(DynamicPrimaryTextColor, "DarkTextColour");
            SetDynamicResource(DynamicPrimaryBackColour, "DarkBackColour");
            SetDynamicResource(DynamicPrimaryMenuBackColour, "DarkMenuBackColour");
            SetDynamicResource(DynamicPrimaryTopMenuBackColour, "DarkTopMenuBackColour");

            SetDynamicResource(DynamicPrimaryBtnTextColor, "DarkBtnTextColour");
            SetDynamicResource(DynamicPrimaryBtnPositiveBackColour, "DarkBtnPositiveBackColour");
            SetDynamicResource(DynamicPrimaryBtnNegativeBackColour, "DarkBtnNegativeBackColour");
            SetDynamicResource(DynamicPrimaryBtnNeutralBackColour, "DarkBtnNeutralBackColour");

            SetDynamicResource(DynamicInputTextColour, "DarkInputTextColour");
            SetDynamicResource(DynamicBigHeaderTextColour, "DarkBigHeaderTextColour");
            SetDynamicResource(DynamicMediumHeaderTextColour, "DarkMediumHeaderTextColour");

            SetDynamicResource(DynamicThemeTextColour, "DarkThemeTextColour");
            SetDynamicResource(DynamicThemeBorderColour, "DarkThemeTextColour");
            SetDynamicResource(DynamicThemeDarkPinkColour, "DarkThemeDarkPinkColour");
        }
    }
}