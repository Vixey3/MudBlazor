namespace DiplomskiBlazor.Client.Services.DeoTelaService
{
    public interface IDeoTelaService
    {
        List<DeoTela> DeloviTela { get; set; }
        Task GetDeoTela();
        Task<DeoTela> GetDeoTela(int id);

        Task CreateDeoTela(DeoTela k);
        Task UpdateDeoTela(DeoTela k);
        Task DeleteDeoTela(int id);
    }
}