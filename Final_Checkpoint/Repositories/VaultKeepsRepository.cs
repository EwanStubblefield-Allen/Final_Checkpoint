namespace Final_Checkpoint.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
      SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }

  internal int CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
      INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
      VALUES(@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
    return _db.ExecuteScalar<int>(sql, vaultKeepData);
  }

  internal void RemoveVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;"; ;
    _db.Execute(sql, new { vaultKeepId });
  }
}
