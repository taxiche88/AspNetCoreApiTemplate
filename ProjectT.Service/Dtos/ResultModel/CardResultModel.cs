using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Service.Dtos.ResultModel
{
    public class CardResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
    }
}
