using Apolices.Web.Models;
namespace Apolices.Web.Services.IServices
{
    public interface ISeguroService
    {
        Task<IEnumerable<SeguroModel>> FindAllSeguros();
        Task<SeguroModel> FindSeguroById(long id);
        Task<SeguroModel> CreateSeguro(SeguroModel model);
        Task<SeguroModel> UpdateSeguro(SeguroModel model);
        Task<bool> DeleteSeguroById(long id);
    }
}
