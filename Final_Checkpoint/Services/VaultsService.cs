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
}