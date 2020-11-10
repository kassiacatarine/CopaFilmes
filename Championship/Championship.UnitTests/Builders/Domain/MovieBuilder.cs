using Championship.Domain.AggregatesModel;
using System.Collections.Generic;

namespace Championship.UnitTests.Builders.Domain
{
    public class MovieBuilder
    {
        private Movie _movie;
        public List<string> MoviesIds => new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };
        public string Id => "123456";
        public string Titulo => "Movie";
        public int Ano => 1999;
        public decimal Nota => 6.9M;

        public MovieBuilder()
        {
            _movie = WithDefaultValues();
        }

        public Movie WithDefaultValues()
        {
            _movie = new Movie()
            {
                Id = Id,
                Titulo = Titulo,
                Ano = Ano,
                Nota = Nota
            };
            return _movie;
        }

        public MovieBuilder WithId(string id)
        {
            _movie.Id = id;
            return this;
        }

        public MovieBuilder WithNota(decimal nota)
        {
            _movie.Nota = nota;
            return this;
        }

        public MovieBuilder WithTitulo(string titulo)
        {
            _movie.Titulo = titulo;
            return this;
        }

        public Movie Build()
        {
            return _movie;
        }
    }
}
