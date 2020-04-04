using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // Get Public Keeps
    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }

    //Get One Keep
    internal Keep Get(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { Id = id });
    }

    //Create Keep
    internal Keep Create(Keep KeepData)
    {
      string sql = @"
            INSERT INTO keeps
            (userId, name, description, img, isPrivate, views, shares, keeps)
            VALUES
            (@UserId, @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID()          
            ";
      KeepData.Id = _db.ExecuteScalar<int>(sql, KeepData);
      return KeepData;
    }

    internal Keep Edit(Keep updatedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
          name = @Name,
          description = @Description,
          img = @Img,
          isPrivate = @IsPrivate
      WHERE id = @Id
      ";
      _db.Execute(sql, updatedKeep);
      return updatedKeep;
    }

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }
  }
}