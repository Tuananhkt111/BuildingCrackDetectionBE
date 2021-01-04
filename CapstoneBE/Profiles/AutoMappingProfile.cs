using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.Locations;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
using CapstoneBE.Models.Custom.MaintenanceOrders;
using CapstoneBE.Models.Custom.Users;

namespace CapstoneBE.Profiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //User entity mapper
            CreateMap<CapstoneBEUser, UserInfo>()
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<UserCreate, CapstoneBEUser>();
            //MaintenanceWorker entity mapper
            CreateMap<MaintenanceWorkerBasicInfo, MaintenanceWorker>();
            CreateMap<MaintenanceWorker, MaintenanceWorkerInfo>();
            //Location entity mapper
            CreateMap<LocationBasicInfo, Location>();
            CreateMap<Location, LocationInfo>();
            //Crack entity mapper
            CreateMap<CrackCreate, Crack>();
            CreateMap<CrackBasicInfo, Crack>();
            CreateMap<Crack, CrackInfo>()
                .ForMember(dest =>
                    dest.LocationName,
                    opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest =>
                    dest.ReporterName,
                    opt => opt.MapFrom(src => src.Reporter.Name));
            CreateMap<Crack, CrackSubInfo>();
            //MaintenanceOrder entity mapper
            CreateMap<MaintenanceOrder, MaintenanceOrderInfo>()
                .ForMember(dest =>
                    dest.AssessorName,
                    opt => opt.MapFrom(src => src.Assessor.Name))
                .ForMember(dest =>
                    dest.Cracks,
                    opt => opt.MapFrom(src => src.Cracks))
                .ForMember(dest =>
                    dest.MaintenanceWorkerName,
                    opt => opt.MapFrom(src => src.MaintenanceWorker.Name));
        }
    }
}