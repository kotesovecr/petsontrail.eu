﻿using Storage.Models;

namespace Storage.Interfaces;

internal interface IStorageService<T> where T: IRecord
{
    public Task<T> AddAsync(T request, CancellationToken cancellationToken);

    public Task<T> UpdateAsync(T request, CancellationToken cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken);

    public Task<T> GetAsync(string id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<T>> GetByFilterAsync(IList<(string key, string value)> filterList, CancellationToken cancellationToken);

    public Task<IReadOnlyList<T>> GetByFilterBeLikeAsync(IList<(string key, string likeValue)> filterList, CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
}