using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Cracks;
using CapstoneBE.Models.Custom.Flights;
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
                    dest.Locations,
                    opt => opt.MapFrom(src => src.LocationHistories))
                .ForMember(dest =>
                    dest.UserId,
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<UserCreate, CapstoneBEUser>();
            //LocationHistory entity mapper
            //CreateMap<LocationHistory, int>()
            //    .ConstructUsing(src => src.LocationId);
            CreateMap<LocationHistory, LocationSubInfo>()
                .ForMember(dest =>
                    dest.LocationId,
                    opt => opt.MapFrom(src => src.LocationId))
                .ForMember(dest =>
                    dest.Name,
                    opt => opt.MapFrom(src => src.Location.Name));
            //MaintenanceWorker entity mapper
            CreateMap<MaintenanceWorkerBasicInfo, MaintenanceWorker>();
            CreateMap<MaintenanceWorker, MaintenanceWorkerInfo>();
            //Location entity mapper
            CreateMap<LocationBasicInfo, Location>();
            CreateMap<Location, LocationInfo>();
            CreateMap<Location, LocationSubInfo>();
            //Crack entity mapper
            CreateMap<CrackCreate, Crack>();
            CreateMap<CrackBasicInfo, Crack>();
            CreateMap<Crack, CrackInfo>()
                .ForMember(dest =>
                    dest.LocationName,
                    opt => opt.MapFrom(src => src.Flight.Location.Name))
                .ForMember(dest =>
                    dest.CensorName,
                    opt => opt.MapFrom(src => src.Censor.Name))
                .ForMember(dest =>
                        dest.UpdateUserName,
                        opt => opt.MapFrom(src => src.UpdateUser.Name));

            CreateMap<Crack, CrackSubDetailsInfo>();
            CreateMap<Crack, CrackSubInfo>();
            //MaintenanceOrder entity mapper
            CreateMap<MaintenanceOrder, MaintenanceOrderInfo>()
                .ForMember(dest =>
                    dest.AssessorName,
                    opt => opt.MapFrom(src => src.Assessor.Name))
                .ForMember(dest =>
                    dest.CreateUserName,
                    opt => opt.MapFrom(src => src.CreateUser.Name))
                .ForMember(dest =>
                    dest.UpdateUserName,
                    opt => opt.MapFrom(src => src.UpdateUser.Name))
                .ForMember(dest =>
                    dest.Cracks,
                    opt => opt.MapFrom(src => src.Cracks))
                .ForMember(dest =>
                    dest.LocationName,
                    opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest =>
                    dest.MaintenanceWorkerName,
                    opt => opt.MapFrom(src => src.MaintenanceWorker.Name));
            //Flight entity mapper
            CreateMap<Flight, FlightInfo>()
                .ForMember(dest =>
                    dest.DataCollectorName,
                    opt => opt.MapFrom(src => src.DataCollector.Name))
                .ForMember(dest =>
                    dest.DeleteVideoUserName,
                    opt => opt.MapFrom(src => src.DeleteVideoUser.Name))
                .ForMember(dest =>
                    dest.Cracks,
                    opt => opt.MapFrom(src => src.Cracks))
                .ForMember(dest =>
                    dest.LocationName,
                    opt => opt.MapFrom(src => src.Location.Name));
            CreateMap<Flight, FlightBasicInfo>();
        }
    }
}