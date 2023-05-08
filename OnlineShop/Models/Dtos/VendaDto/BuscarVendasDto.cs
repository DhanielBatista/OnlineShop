using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.Dtos.VendaDto
{
    public class BuscarVendaDto
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DataFim { get; set; }

        public override string ToString()
        {
            return $"{{ \"dataInicio\": \"{DataInicio:dd-MM-yyyy}\", \"dataFim\": \"{DataFim:dd-MM-yyyy}\" }}";
        }
    }
}