using AutoMapper;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.BLL.Services.Abstract
{
    public abstract class BaseService
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
