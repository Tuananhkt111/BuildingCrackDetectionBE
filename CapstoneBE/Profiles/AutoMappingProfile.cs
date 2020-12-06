using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.LocationHistories;
using CapstoneBE.Models.Custom.Locations;
using CapstoneBE.Models.Custom.MaintenaceWorkers;
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
            //LocationHistory entity mapper
            CreateMap<LocationHistoryCreate, LocationHistory>();
            CreateMap<LocationHistory, LocationHistoryInfo>()
                .ForMember(dest =>
                    dest.LocationName,
                    opt => opt.MapFrom(src => src.Location.Name));
        }
    }
}