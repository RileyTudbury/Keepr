using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;
    public VaultsController(VaultsService vs, KeepsService ks)
    {
      _vs = vs;
      _ks = ks;
    }
    //Get User Vaults
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Vault>> GetUserVaults()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.GetUserVaults(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //Get One Vault
    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      try
      {
        Vault vault = _vs.Get(id);
        var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user != null && user.Value == vault.UserId)
        {
          return Ok(vault);
        }
        return Unauthorized("You do not have access to this Vault.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //Get Keeps by VaultId
    [HttpGet("{id}/keeps")]
    [Authorize]
    public ActionResult<IEnumerable<VaultKeepViewModel>> GetKeepsByVaultId(int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.GetKeepsByVaultId(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Create Vault
    [HttpPost]
    [Authorize]
    public ActionResult<Vault> Create([FromBody] Vault newVault)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newVault.UserId = userId;
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Delete Vault
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Vault> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}