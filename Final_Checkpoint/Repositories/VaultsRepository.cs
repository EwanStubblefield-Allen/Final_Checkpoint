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
      FROM
        vaults v
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

  internal List<Vault> GetVaultsByUserId(string userId)
  {
    string sql = @"
      SELECT v.*, a.*
      FROM
        vaults v
        JOIN accounts a ON a.id = v.creatorId
      WHERE creatorId = @userId;";
    return _db.Query<Vault, Profile, Vault>(sql,
      (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      },
      new { userId }).ToList();
  }

  internal int CreateVault(Vault vaultData)
  {
    string sql = @"
      INSERT INTO vaults(name, description, img, isPrivate, creatorId)
      VALUE(@Name, @Description, @Img, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID()";
    return _db.ExecuteScalar<int>(sql, vaultData);
  }

  internal void UpdateVault(Vault vaultData)
  {
    string sql = @"
      UPDATE vaults SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @isPrivate
      WHERE id = @Id LIMIT 1;";
    _db.Execute(sql, vaultData);
  }

  internal void RemoveVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
    _db.Execute(sql, new { vaultId });
  }
}