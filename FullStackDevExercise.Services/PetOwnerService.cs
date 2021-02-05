using FullStackDevExercise.DAL.Repository;
using FullStackDevExercise.ViewModels;
using FullStackDevExercise.ViewModels.Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackDevExercise.Services
{
  public class PetOwnerService : IPetOwnerService
  {
    private readonly IPetRepository _petRepo;
    private readonly IOwnerRepository _ownerRepo;
    private readonly IOwnerMapper _ownerMapper;
    private readonly IPetMapper _petMapper;

    public PetOwnerService(IPetRepository petRepo, IOwnerRepository ownerRepo, IOwnerMapper ownerMapper, IPetMapper petMapper)
    {
      _petRepo = petRepo;
      _ownerRepo = ownerRepo;
      _ownerMapper = ownerMapper;
      _petMapper = petMapper;
    }

    public async Task<IEnumerable<OwnerViewModel>> GetOwnersAsync()
    {
      var owners = await _ownerRepo.GetAsync();

      return owners?.Select(r => _ownerMapper.Encode(r));
    }

    public async Task<OwnerViewModel> GetOwnerAsync(long id) => _ownerMapper.Encode(await _ownerRepo.GetAsync(id));

    public async Task<bool> SaveOwnerAsync(OwnerViewModel model)
    {
      if (model.Id == 0)
      {
        var id = await _ownerRepo.InsertAsync(_ownerMapper.Decode(model));
        model.Id = id;
        return model.Id > 0;
      }
      else
      {
        var isSuccess = await _ownerRepo.UpdateAsync(_ownerMapper.Decode(model));
        return isSuccess;
      }
    }

    public async Task<bool> DeleteOwnerAsync(long id) => (await _ownerRepo.DeleteAsync(id)) == 1;

    public IList<PetViewModel> GetPetsByOwnerIdAsync(long ownerId) => _petMapper.ForOwnerId(ownerId).Encode(_petRepo.GetByOwnerIdAsync(ownerId));

    public async Task<PetViewModel> GetPetByIdAsync(long ownerId, long id) => _petMapper.ForOwnerId(ownerId).Encode(await _petRepo.GetAsync(id));

    public async Task<bool> SavePetAsync(long ownerId, PetViewModel model)
    {
      if (model.Id == 0)
      {
        model.Id = await _petRepo.InsertAsync(_petMapper.ForOwnerId(ownerId).Decode(model));
        return model.Id > 0;
      }
      else
      {
        var isSuccess = await _petRepo.UpdateAsync(_petMapper.ForOwnerId(ownerId).Decode(model));
        return isSuccess;
      }
    }

    public async Task<bool> DeletePetAsync(object ownerId, long id) => (await _petRepo.DeleteAsync(id)) == 1;
  }
}