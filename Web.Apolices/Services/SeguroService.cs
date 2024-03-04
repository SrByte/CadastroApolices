
using Apolices.Web.Models;
using Apolices.Web.Services.IServices;
using MongoDB.Bson;

namespace Apolices.Web.Services;
public class SeguroService : ISeguroService
{

    private readonly HttpClient _client;
    public const string BasePath = "api/v1/apolice";

    public SeguroService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<SeguroModel>> FindAllSeguros()
    {
        try
        {
            //envia um request GET para a uri da API
            var response = await _client.GetAsync(BasePath);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
					return Enumerable.Empty<SeguroModel>().ToList();
                }
				//var lista= await response.Content.ReadAsAsync<List<SeguroModel>>();

				//return await Task.FromResult(lista);

				return await response.Content.ReadAsAsync<List<SeguroModel>>();

			}
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }




    public async Task<SeguroModel> FindSeguroById(long numeroApolice)
    {
        try
        {
            var response = await _client.GetAsync($"{BasePath}/{numeroApolice}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    // Retorna uma instância vazia de SeguroModel
                    return new SeguroModel();
                }
                return await response.Content.ReadAsAsync<SeguroModel>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<SeguroModel> CreateSeguro(SeguroModel model)
    {

        //var response = await _client.GetAsync($"api/apolice");

        var response = await _client.PostAsJsonAsync(BasePath, model);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsAsync<SeguroModel>();
        throw new Exception("Deu algum ruim na chamada da API.");
    }

    public async Task<bool> DeleteSeguroById(long id)
    {
        // Substitua o código abaixo pelo seu cenário real de exclusão
        var deleteResponse = await _client.DeleteAsync($"{BasePath}/{id}");

        if (!deleteResponse.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<SeguroModel> UpdateSeguro(SeguroModel model)
    {
        // Substitua o código abaixo pelo seu cenário real de atualização
        var updateResponse = await _client.PutAsJsonAsync(BasePath, model);

        if (updateResponse.IsSuccessStatusCode)
        {
            return await updateResponse.Content.ReadAsAsync<SeguroModel>();
        }

        throw new Exception("Erro na atualização do seguro.");
    }

    // Método fictício de exclusão (delete)
  
}


