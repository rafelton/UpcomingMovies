using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcomingMovies.CustomControls
{
    public class BaseFFCacheImage : FFImageLoading.Forms.CachedImage
    {
        public BaseFFCacheImage()
        {
            this.Aspect = Xamarin.Forms.Aspect.AspectFit;
            CacheDuration = new TimeSpan(90, 0, 0, 0);
            DownsampleToViewSize = true;
        }
    }
}
