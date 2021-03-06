using CapstoneBE.Data;
using CapstoneBE.Helpers;
using CapstoneBE.Models;
using CapstoneBE.Repositories.Cracks;
using CapstoneBE.Repositories.Flights;
using CapstoneBE.Repositories.LocationHistories;
using CapstoneBE.Repositories.Locations;
using CapstoneBE.Repositories.MaintenanceOrders;
using CapstoneBE.Repositories.MaintenanceWorkers;
using CapstoneBE.Repositories.PushNotifications;
using CapstoneBE.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CapstoneBE.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CapstoneDbContext _capstoneDbContext;
        private readonly UserManager<CapstoneBEUser> _userManager;
        private readonly SignInManager<CapstoneBEUser> _signInManager;
        private readonly IOptions<AppSettings> _appSettings;
        private UserRepository _userRepository;
        private MaintenanceWorkerRepository _maintenanceWorkerRepository;
        private LocationRepository _locationRepository;
        private LocationHistoryRepository _locationHistoryRepository;
        private CrackRepository _crackRepository;
        private MaintenanceOrderRepository _maintenanceOrderRepository;
        private NotificationRepository _notificationRepository;
        private FlightRepository _flightRepository;

        public UnitOfWork(CapstoneDbContext capstoneDbContext, UserManager<CapstoneBEUser> userManager,
            SignInManager<CapstoneBEUser> signInManager, IOptions<AppSettings> appSettings)
        {
            _capstoneDbContext = capstoneDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_capstoneDbContext, _appSettings, _signInManager, _userManager);
                return _userRepository;
            }
        }

        public MaintenanceWorkerRepository MaintenanceWorkerRepository
        {
            get
            {
                if (_maintenanceWorkerRepository == null)
                    _maintenanceWorkerRepository = new MaintenanceWorkerRepository(_capstoneDbContext);
                return _maintenanceWorkerRepository;
            }
        }

        public LocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                    _locationRepository = new LocationRepository(_capstoneDbContext);
                return _locationRepository;
            }
        }

        public LocationHistoryRepository LocationHistoryRepository
        {
            get
            {
                if (_locationHistoryRepository == null)
                    _locationHistoryRepository = new LocationHistoryRepository(_capstoneDbContext);
                return _locationHistoryRepository;
            }
        }

        public CrackRepository CrackRepository
        {
            get
            {
                if (_crackRepository == null)
                    _crackRepository = new CrackRepository(_capstoneDbContext);
                return _crackRepository;
            }
        }

        public MaintenanceOrderRepository MaintenanceOrderRepository
        {
            get
            {
                if (_maintenanceOrderRepository == null)
                    _maintenanceOrderRepository = new MaintenanceOrderRepository(_capstoneDbContext);
                return _maintenanceOrderRepository;
            }
        }

        public NotificationRepository NotificationRepository
        {
            get
            {
                if (_notificationRepository == null)
                    _notificationRepository = new NotificationRepository(_capstoneDbContext);
                return _notificationRepository;
            }
        }

        public FlightRepository FlightRepository
        {
            get
            {
                if (_flightRepository == null)
                    _flightRepository = new FlightRepository(_capstoneDbContext);
                return _flightRepository;
            }
        }

        public IDbContextTransaction GetTransaction()
        {
            return _capstoneDbContext.Database.BeginTransaction();
        }

        public async Task<int> Save()
        {
            return await _capstoneDbContext.SaveChangesAsync();
        }
    }
}