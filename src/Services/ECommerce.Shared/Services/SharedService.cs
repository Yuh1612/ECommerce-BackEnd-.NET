using ECommerce.Shared.Interfaces;
using ECommerce.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Services
{
    public abstract class SharedService
    {
        protected readonly IServiceProvider ServiceProvider;
        protected IUnitOfWork UnitOfWork { get; }

        private IUserInfo? _user;
        protected IUserInfo User => _user ??= ServiceProvider.GetRequiredService<IUserInfo>();

        public SharedService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
        {
            UnitOfWork = unitOfWork;
            ServiceProvider = serviceProvider;
        }
    }
}