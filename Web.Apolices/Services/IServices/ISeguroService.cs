using Apolices.Web.Models;
namespace Apolices.Web.Services.IServices
{
    public interface ISeguroService
    {
        Task<IEnumerable<SeguroModel>> FindAllSeguros();
        Task<SeguroModel> FindSeguroById(string id);
        Task<SeguroModel> CreateSeguro(SeguroModel model);
        Task<SeguroModel> UpdateSeguro(SeguroModel model);
        Task<bool> DeleteSeguroById(string id);
    }
}
