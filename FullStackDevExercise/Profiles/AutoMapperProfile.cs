using AutoMapper;
using FullStackDevExercise.DataAccess;
using FullStackDevExercise.Models;

namespace FullStackDevExercise.Profiles
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<OwnerModel, Owners>().ReverseMap();
      CreateMap<PetModel, Pets>().ReverseMap();
      CreateMap<AppointmentModel, Appointments>().ReverseMap();
      CreateMap<VetModel, Vets>().ReverseMap();
    }
  }
}