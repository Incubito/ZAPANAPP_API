using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleScreen
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Design { get; set; }
        public string FontFamily { get; set; }
        public string FontRed { get; set; }
        public string FontGreen { get; set; }
        public string FontBlue { get; set; }
        public string LineRed { get; set; }
        public string LineGreen { get; set; }
        public string LineBlue { get; set; }
        public string BackgroundRed { get; set; }
        public string BackgroudGreen { get; set; }
        public string BackgroundBlue { get; set; }
        public string Updated { get; set; }
    }
}