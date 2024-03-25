using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class AboutEntites 
    {
        [Key]
        public int Id { get; set; }
        public string Contents { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        //    public void Mapping(Profile profile)
        //    {
        //        profile.CreateMap<AboutDTO, AboutEntites>().ForMember(abo => abo.Id, des => des.MapFrom(src => src.Id))
        //            .ForMember(abo => abo.Contents, des => des.MapFrom(src => src.Contents))
        //            .ForMember(abo => abo.Title, des => des.MapFrom(src => src.Title))
        //            .ForMember(abo => abo.Image, des => des.MapFrom(src => src.Image));
        //    }
        //}
    }
}
