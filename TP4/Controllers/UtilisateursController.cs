﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP4.Models.DataManager;
using TP4.Models.EntityFramework;
using TP4.Models.Repository;

namespace TP4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IDataRepository<Utilisateur> dataRepository;
        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            dataRepository = dataRepo;
        }
    // GET: api/Utilisateurs
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await dataRepository.GetAllAsync();
        }
        // GET: api/Utilisateurs/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            var utilisateur = await dataRepository.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur.Value == null)
            {
                return NotFound();
            }
            return utilisateur;
        }
        
        // GET: api/Utilisateurs/toto@titi.fr
        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            var utilisateur = await dataRepository.GetByStringAsync(email);
            if (utilisateur.Value == null)
            {
                return NotFound();
            }
            return utilisateur;
        }
        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest();
            }
            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, utilisateur);
                return NoContent();
            }
        }
        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(utilisateur);
            return CreatedAtAction("GetById", new { id = utilisateur.UtilisateurId }, utilisateur); // GetById : nom de l’action
        }
        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await dataRepository.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(utilisateur.Value);
            return NoContent();
        }
         //private bool UtilisateurExists(int id)
         //{
         // return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
         //}
         }
}
