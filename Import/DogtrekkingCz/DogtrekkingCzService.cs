using System.Text.Json;
using Import.DogtrekkingCz.SrcEntities;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Storage.Entities.Actions;
using Storage.Interfaces;

namespace Import.DogtrekkingCz;

internal class DogtrekkingCzService : IDogtrekkingCzService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DogtrekkingCzService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task RunImportAsync(IServiceProvider serviceProvider, string importUrl)
    {
        var actions = await LoadDataAsync(importUrl);
        
        using (var scope = serviceProvider.CreateScope())
        {
            var actionsRepositoryService = scope.ServiceProvider.GetRequiredService<IActionsRepositoryService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            
            foreach (var action in actions)
            {
                await actionsRepositoryService.AddActionAsync(mapper.Map<CreateActionInternalStorageRequest>(action), CancellationToken.None);
            }
        }
    }

    private async Task<IEnumerable<FullActionModel>?> LoadDataAsync(string importUrl)
    {
        if (string.IsNullOrWhiteSpace(importUrl))
            return new List<FullActionModel>();
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, importUrl);

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            
            Console.WriteLine(contentStream.ToString());
            
            return await JsonSerializer.DeserializeAsync<IEnumerable<FullActionModel>>(contentStream);
        }

        return new List<FullActionModel>();
    }
}