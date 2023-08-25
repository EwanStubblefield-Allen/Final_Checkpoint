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
      SELECT k.*, a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;";
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
      SELECT k.*, a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @keepId;";
    return _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      },
      new { keepId }).FirstOrDefault();
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
        img = @Img
      WHERE id = @Id LIMIT 1;";
    _db.Execute(sql, keepData);
  }

  internal void RemoveKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    _db.Execute(sql, new { keepId });
  }
}
