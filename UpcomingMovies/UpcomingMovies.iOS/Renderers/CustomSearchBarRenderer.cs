using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using UpcomingMovies.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace UpcomingMovies.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {                
                //Control.BackgroundColor = Color.FromHex("#081c24").ToUIColor();                
                Control.SearchBarStyle = UISearchBarStyle.Minimal;
                Control.BackgroundImage = new UIImage();
            }
        }
    }
}