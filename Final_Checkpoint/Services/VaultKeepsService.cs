namespace Final_Checkpoint.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
    _vaultsService = vaultsService;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
    return vaultKeep ?? throw new Exception($"[NO VAULT KEEP MATCHES THE ID: {vaultKeepId}]");
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception($"[YOU ARE NOT THE CREATOR OF {vault.Name}]");
    }
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