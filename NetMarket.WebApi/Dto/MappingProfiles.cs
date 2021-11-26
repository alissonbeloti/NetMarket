using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using NetMarket.Core.Entities;
using NetMarket.WebApi.Dto.Login;

namespace NetMarket.WebApi.Dto
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(p => p.CategoriaNome, x => x.MapFrom(a => a.Categoria.Nome))
                .ForMember(p => p.MarcaNome, x => x.MapFrom(a => a.Marca.Nome))
                .ReverseMap();
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
