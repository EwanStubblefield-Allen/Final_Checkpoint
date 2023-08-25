namespace Final_Checkpoint.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
    return vaultKeep ?? throw new Exception($"[NO VAULT KEEP MATCHES THE ID: {vaultKeepId}]");
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    int vaultKeepId = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
    return GetVaultKeepById(vaultKeepId);
  }

  internal VaultKeep RemoveVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeepToDelete = GetVaultKeepById(vaultKeepId);
    if (vaultKeepToDelete.CreatorId != userId)
    {
      throw new Exception($"[YOU ARE NOT THE CREATOR OF THIS VAULT KEEP]");
    }
    _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
    return vaultKeepToDelete;
  }
}
