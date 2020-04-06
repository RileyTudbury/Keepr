using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    // Get Public Vaults
    internal IEnumerable<Vault> Get()
    {
      string sql = "SELECT * FROM vaults";
      return _db.Query<Vault>(sql);
    }

    //Get Users Vaults
    internal IEnumerable<Vault> GetUserVaults(string UserId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { UserId });
    }

    //Get One Vault
    internal Vault Get(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { Id = id });
    }

    //Create Vault
    internal Vault Create(Vault vaultData)
    {
      string sql = @"
            INSERT INTO Vaults
            (userId, name, description)
            VALUES
            (@UserId, @Name, @Description);
            SELECT LAST_INSERT_ID()          
            ";
      vaultData.Id = _db.ExecuteScalar<int>(sql, vaultData);
      return vaultData;
    }

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM Vaults WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }
  }
}