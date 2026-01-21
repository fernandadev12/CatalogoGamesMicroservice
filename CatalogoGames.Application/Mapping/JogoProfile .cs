using CatalogoGames.Application.Games.ViewModels;
using AutoMapper;
using CatalogoGames.Domain.Entity;

namespace CatalogoGames.Application.Mapping
{

    public class JogoProfile : Profile
    {
        public JogoProfile()
        {
            CreateMap<Game, JogoVm>();
            CreateMap<CriarJogoVm, Game>()
                .ConstructUsing(src => new Game(src.Titulo, src.Plataforma, src.Preco, src.Lancamento, src.Descricao,null));
            CreateMap<AtualizarJogoVm, Game>()
                .ConstructUsing(src => new Game(src.Titulo, src.Plataforma, src.Preco, src.Lancamento, src.Descricao,null ));
        }
    }

}
