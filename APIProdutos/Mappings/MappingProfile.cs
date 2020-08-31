using System;
using AutoMapper;
using APIProdutos.Models;
using APIProdutos.Documents;

namespace APIProdutos.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CadastroProduto, ItemCatalogoDocument>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(p => p.CodigoBarras))
                .ForMember(dest => dest.Tipo, m => m.MapFrom(p => "PRODUTO"))
                .ForMember(dest => dest.UltimaAtualizacao, m => m.MapFrom(p => DateTime.Now));

            CreateMap<ItemCatalogoDocument, Produto>()
                .ForMember(dest => dest.CodigoBarras, m => m.MapFrom(i => i.Codigo))            
                .ForMember(dest => dest.DataAtualizacao, m => m.MapFrom(i => i.UltimaAtualizacao));            
        }
    }
}