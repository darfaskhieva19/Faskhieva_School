using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Faskhieva_School
{
    public partial class Service
    {
        public SolidColorBrush ColorDisc //услуга со скидкой
        {
            get
            {
                if(Discount != null)
                {
                    SolidColorBrush Discount = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                    return Discount;
                }
                else
                {
                    SolidColorBrush NoDiscount = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    return NoDiscount;
                }
            }
        }
        public float Time //продолжительность занятий в минутах
        {
            get
            {
                float time = DurationInSeconds / 60;
                return time;
            }
        }
    }
}
