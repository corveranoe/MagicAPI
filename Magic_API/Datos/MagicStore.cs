using Magic_API.Modelos.Dto;

namespace Magic_API.Datos
{
    public static class MagicStore
    {
        public static List<MagicDto> MagicList = new List<MagicDto>
        {
            new MagicDto{Id=1, Nombre="Noe Corvera", Ocupantes=3, MetrosCuadrados=50},
            new MagicDto{Id=2, Nombre="Danely Alas", Ocupantes=4, MetrosCuadrados=80}
        };
    }
}
