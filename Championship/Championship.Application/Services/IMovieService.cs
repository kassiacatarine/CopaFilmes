﻿using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public interface IMovieService
    {
        Task<Response<IEnumerable<MovieViewModel>>> GetMoviesAsync();
    }
}
