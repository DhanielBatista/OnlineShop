using System.Runtime.Serialization;

namespace OnlineShop.Models.Enums
{
    public enum OrdenacaoEnum
    {
        [EnumMember(Value = "crescente")]
        crescente,
        [EnumMember(Value = "decrescente")]
        decrescente,
    }
}
