using AutoMapper;
using SnackMachineApp.Application.Atms;
using SnackMachineApp.Application.SnackMachines;
using SnackMachineApp.Domain.Atms;
using SnackMachineApp.Domain.SnackMachines;

namespace SnackMachineApp.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AtmDto, Atm>();
            CreateMap<SnackMachineDto, SnackMachine>();
            //CreateMap<HeadOfficeDto, HeadOffice>();
        }
    }
}