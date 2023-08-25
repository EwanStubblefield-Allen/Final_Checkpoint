using Microsoft.Extensions.Configuration.UserSecrets;

namespace Final_Checkpoint.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;

  public KeepsService(KeepsRepository keepsRepository)
  {
    _keepsRepository = keepsRepository;
  }

  internal List<Keep> GetKeeps()
  {
    return _keepsRepository.GetKeeps();
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _keepsRepository.GetKeepById(keepId);
    return keep ?? throw new Exception($"[NO KEEP MATCHES THE ID: {keepId}]");
  }

  internal Keep CreateKeep(Keep keepData)
  {
    int keepId = _keepsRepository.CreateKeep(keepData);
    return GetKeepById(keepId);
  }

  internal Keep UpdateKeep(Keep keepData)
  {
    Keep originalKeep = HandleData(keepData.Id, keepData.CreatorId);
    originalKeep.Name = keepData.Name ?? originalKeep.Name;
    originalKeep.Description = keepData.Description ?? originalKeep.Description;
    originalKeep.Img = keepData.Img ?? originalKeep.Img;
    _keepsRepository.UpdateKeep(originalKeep);
    return originalKeep;
  }

  internal Keep RemoveKeep(int keepId, string userId)
  {
    Keep keepToDelete = HandleData(keepId, userId);
    _keepsRepository.RemoveKeep(keepId);
    return keepToDelete;
  }

  internal Keep HandleData(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception($"[YOU ARE NOT THE CREATOR OF {keep.Name}]");
    }
    return keep;
  }
}
