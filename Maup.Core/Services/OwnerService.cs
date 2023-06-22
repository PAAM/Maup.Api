using Maup.Core.Entities;
using Maup.Core.Filters;
using Maup.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Services
{
    public class OwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IFileStore _fileStore;
        public OwnerService(IUnitOfWork unitOfWork, IFileStore fileStore)
        {
            _unitOfWork = unitOfWork;
            _fileStore = fileStore;
        }

        public async Task<Owner> GetLoginByCredentials(Owner owner)
        {
            return await _unitOfWork.OwnerRepository.GetLoginByCredentials(owner);
        }

        public async Task CreateOwner(Owner owner, IFormFile formFile)
        {
            if (formFile != null)
            {
                var path = await _fileStore.SaveFile(formFile);
                owner.Photo = path;
            }
            await _unitOfWork.OwnerRepository.Add(owner);
            await _unitOfWork.SaveChangesAsync();
        }



        //public IEnumerable<Owner> GetOwnerAsync()
        //{
        //    var response = _unitOfWork.OwnerRepository.GetAll();
        //    return response;
        //}

        //public Task<Owner> GetOwnerById(int Id)
        //{
        //    var response = _unitOfWork.OwnerRepository.GetById(Id);
        //    return response;
        //}

        //public async Task<bool> UpdateOwner(Owner owner, IFormFile formFile)
        //{
        //    if (formFile != null)
        //    {
        //        var path = await _fileStore.SaveFile(formFile);
        //        owner.Photo = path;
        //    }

        //    _unitOfWork.OwnerRepository.Update(owner);
        //    await _unitOfWork.SaveChangesAsync();
        //    return true;
        //}
    }
}
