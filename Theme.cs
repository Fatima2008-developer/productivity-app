using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Productivity_App
{
    class Theme
    {

        public static void ChangeTheme(Uri themeUri)
        {
            try
            {
                // Toggle between dark and light themes based on the current flag
                ResourceDictionary Theme = new ResourceDictionary()
                {
                    Source = themeUri,
                };
                // Remove existing dictionaries and apply the new one
                App.Current.Resources.Clear();
                App.Current.Resources.MergedDictionaries.Add(Theme);
                // Toggle the flag for next click

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error Showing.." + ex.Message);

            }


        }





    }
}
