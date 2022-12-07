using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Repository.Dtos.Condition
{
    public class CardCondition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
        public string Card_Type { get; set; }
    }
}
