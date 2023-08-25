namespace Final_Checkpoint.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
      SELECT
      k.*,
      COUNT(vk.id) AS kept,
      a.*
      FROM
        keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vk ON k.id = vk.keepId
      GROUP BY k.id;";
    return _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
      SELECT
        k.*,
        COUNT(vk.id) AS kept,
        a.*
      FROM
        keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vk ON k.id = vk.keepId
      WHERE k.id = @keepId
      GROUP BY k.id;";
    return _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      },
      new { keepId }).FirstOrDefault();
  }

  internal List<KeepVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
      SELECT k.*, a.*, vk.*
      FROM
        keeps k
        JOIN accounts a ON a.id = k.creatorId
        RIGHT JOIN vaultKeeps vk ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId;";
    return _db.Query<KeepVault, Profile, VaultKeep, KeepVault>(
      sql,
      (keep, profile, vaultKeep) =>
      {
        keep.Creator = profile;
        keep.VaultKeepId = vaultKeep.Id;
        return keep;
      },
      new { vaultId }).ToList();
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    string sql = @"
      SELECT * FROM keeps WHERE creatorId = @profileId;";
    return _db.Query<Keep>(sql, new { profileId }).ToList();
  }

  internal int CreateKeep(Keep keepData)
  {
    string sql = @"
      INSERT INTO keeps(name, description, img, creatorId)
      VALUES(@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();";
    return _db.ExecuteScalar<int>(sql, keepData);
  }

  internal void UpdateKeep(Keep keepData)
  {
    string sql = @"
      UPDATE keeps SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
      WHERE id = @Id LIMIT 1;";
    _db.Execute(sql, keepData);
  }

  internal void RemoveKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    _db.Execute(sql, new { keepId });
  }
}
