using AutoMapper;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.BLL.Services.Interfaces
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
