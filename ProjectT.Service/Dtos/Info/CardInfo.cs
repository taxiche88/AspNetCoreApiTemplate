using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Service.Dtos.Info
{
    public class CardInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
    }
}
