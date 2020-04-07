using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    internal IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    internal Keep Get(int id)
    {
      Keep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }


    internal IEnumerable<Keep> GetUserKeeps(string userId)
    {
      return _repo.GetUserKeeps(userId);
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep updatedKeep)
    {
      Keep found = Get(updatedKeep.Id);
      if (found.UserId != updatedKeep.UserId)
      {
        throw new Exception("Invalid Request");
      }
      found.Name = updatedKeep.Name;
      found.Description = updatedKeep.Description;
      found.Img = updatedKeep.Img;
      found.IsPrivate = updatedKeep.IsPrivate;

      return _repo.Edit(found);
    }

    internal Keep EditKeepCounts(Keep updatedKeep)
    {
      Keep found = Get(updatedKeep.Id);
      found.Keeps = updatedKeep.Keeps;
      found.Views = updatedKeep.Views;
      found.Shares = updatedKeep.Shares;

      return _repo.EditKeepCounts(found);
    }

    internal Keep Delete(int id, string userId)
    {
      Keep found = Get(id);
      if (found.UserId != userId)
      {
        throw new Exception("Invalid Request");
      }
      if (_repo.Delete(id))
      {
        return found;
      }
      throw new Exception("How did you hit this error?");
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
      return _repo.GetKeepsByVaultId(vaultId, userId);
    }


  }
}