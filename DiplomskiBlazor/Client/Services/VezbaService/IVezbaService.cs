namespace DiplomskiBlazor.Client.Services.VezbaService
{
    public interface IVezbaService
    {
        List<Vezba> Vezbe { get; set; }

        List<DeoTela> DeloviTela { get; set; }
        Task GetVezbe();
        Task GetDeloviTela();
        //Task SetujDeoTela();
        Task<Vezba> GetSingleVezba(int id);

        Task CreateVezba(Vezba k);
        Task UpdateVezba(VezbaDto k);
        Task DeleteVezba(int id);
    }
}