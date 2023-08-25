using System.Reflection.Metadata;

namespace Final_Checkpoint.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepository;

  public VaultsService(VaultsRepository vaultsRepository)
  {
    _vaultsRepository = vaultsRepository;
  }

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _vaultsRepository.GetVaultById(vaultId);
    return vault ?? throw new Exception($"[NO VAULT MATCHES THE ID: {vaultId}]");
  }

  internal Vault CreateVault(Vault vaultData)
  {
    int vaultId = _vaultsRepository.CreateVault(vaultData);
    return GetVaultById(vaultId);
  }

  internal Vault UpdateVault(Vault vaultData)
  {
    Vault originalVault = HandleData(vaultData.Id, vaultData.CreatorId);
    originalVault.Name = vaultData.Name ?? originalVault.Name;
    originalVault.Description = vaultData.Description ?? originalVault.Description;
    originalVault.Img = vaultData.Img ?? originalVault.Img;
    originalVault.IsPrivate = vaultData.IsPrivate ?? originalVault.IsPrivate;
    _vaultsRepository.UpdateVault(originalVault);
    return originalVault;
  }

  internal Vault RemoveVault(int vaultId, string userId)
  {
    Vault vaultToRemove = HandleData(vaultId, userId);
    _vaultsRepository.RemoveVault(vaultId);
    return vaultToRemove;
  }

  internal Vault HandleData(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId)
    {
      throw new Exception($"[YOU ARE NOT THE CREATOR OF {vault.Name}]");
    }
    return vault;
  }
}