namespace Final_Checkpoint.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
      SELECT v.*, a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @vaultId;";
    return _db.Query<Vault, Profile, Vault>(
      sql,
      (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      },
      new { vaultId }).FirstOrDefault();
  }

  internal int CreateVault(Vault vaultData)
  {
    string sql = @"
      INSERT INTO vaults(name, description, img, isPrivate, creatorId)
      VALUE(@Name, @Description, @Img, @IsPrivate, @CreatorId);";
    return _db.ExecuteScalar<int>(sql, vaultData);
  }
}