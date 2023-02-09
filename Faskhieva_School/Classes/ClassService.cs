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
        public string DiscountServ
        {
            get
            {
                return "* скидка " + (Discount * 100).ToString() + "%";
            }
        }
        public SolidColorBrush ColorDisc //услуга со скидкой
        {
            get
            {
                if (Discount != null)
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
        public string Time //продолжительность занятий в минутах
        {
            get
            {
                return string.Format($"за {DurationInSeconds / 60} минут");
            }
        }
        public double Price
        {
            get
            {
                if (Discount != null)
                {
                    double price = (double)Cost - (double)Cost / 100 * (double)Discount;
                    return price;
                }
                else
                {
                    return Convert.ToDouble(Cost);
                }                
            }
        }
    }
}
