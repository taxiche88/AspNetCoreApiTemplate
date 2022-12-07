using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Repository.Dtos.Condition
{
    public class CardUpdateCondition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
    }
}
