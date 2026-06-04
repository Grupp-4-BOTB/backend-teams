using System.Net.Http; 
using System.Net.Http.Json;
using BackendTeams.Application.Interfaces;
using BackendTeams.Domain.Entities;
using BackendTeams.DTOs;
using BackendTeams.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]  // Denna kräver JWT (som gabriel fixar på inlogg sidan) för alla nedan, och att användaren är inloggad. Säkerhet.
                    // STÄNG AV SÅLÄNGE TILLS GABRIEL FIXAT SAMMA. ANNARS KRÅNGLAR SWAGGER.
    public class MembersController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public MembersController(IApplicationDbContext context)
        {
            _context = context;
        }




        // POST
        // 1. TA EMOT SVARET FRPN MOTTAGAREN AV MAIL (När de klickar Approve i mailet)
        [HttpPost("respond")]
        public async Task<IActionResult> RespondToInvite([FromBody] RespondInviteDTO response)
        {
            if (response.Accept)
            {

                try
                { 
                var newMember = new MemberEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    GroupId = response.GroupId,
                    UserId = response.UserId
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                }

                catch
                {
                    return BadRequest("Database currently missing. Not able to add member to a group.");
                }
            }

            return Ok(new { message = response.Accept ? "User successfully added to the group" : "Denied" }); //STATUSKOD I WEBBLÄSAREN
        }






        // GET
        // HÄMTAR MEDLEMMAR ( useEffect i frontenden)
        [HttpGet("groups/{groupId}")]
        public async Task<IActionResult> GetMembers(string groupId)
        {

            //HÄMTAR ALLA MEDLEMMAR MED DEN HÅRDKODADE PROFILBILDEN
            try
            {
                var members = await _context.Members
                .Where(m => m.GroupId == groupId)
                .Select(m => new {
                    id = m.Id,
                    name = m.UserId,     // UserId skickas som 'name' till frontenden så länge
                    role = "Student",     // HÅRDKODAD "STUDENT" SÅLÄNGE, OM TID FINNS KOMMER DEN FIXAS SÅ ANVÄNDARE KANSKE KAN VÄLJA DETTA I MAILET SOM SKICKAS UT ELLER I GABRIELS DEL ELLER I EMILS PROFILE DEL. OSÄKER JUST NU PÅ OM DEN SKA FINNAS REDAN I IDENTITY SKAPNING AV KONTO ELLER SENARE VAL AV ANVÄNDARE.
                    profilePic = "/defaultprofile.svg" //Alla medlemmar får exakt samma profilbild: "/defaultprofile.svg" - deta ändras nedan när vi anropar Emils API.
                })
                .ToListAsync();


                // HÄMTAR RÄTT PROFILBILD FRÅN EMILS API
                using var client = new HttpClient();


                // *** LÖSENORD/ API KEY TILL EMILS API SKRIVS IN HÄR - MEN HAN HAR INGEN ÄN SÅ INGET INLAGT HÄR ELLER I USER SECRETS ***


                //Startar loop
                var UpdatedProfilePic = new List<object>();

                foreach (var member in members)
                {

                    // DEN HÅRDKODADE BILDEN
                    string DefaultPic = member.profilePic;

                    // DENNA ÄR BILDEN SOM HÄMTAR FRÅN EMILS API
                    string EmilsAPIpicture= "";


                    try
                    {
                        var res = await client.GetFromJsonAsync<ProfileDTO>($"https://webapp-photoservice-emil-b7h6anhxdsamgzfx.germanywestcentral-01.azurewebsites.net/api/images/{member.name}");
                        if (!string.IsNullOrEmpty(res?.ProfilePic))
                        {
                            EmilsAPIpicture = res.ProfilePic;
                        }
                    }
                    catch 
                    {
                        /* Om Emils api inte funkar > behåll den hårdkodade profilbilden */ 
                    }


                    // OM API-ANROPET ÄR TOMT ELLER INNEHÅLLER EN NY PROFILBILD:
                    string updatedProfilePic;

                    if (!string.IsNullOrEmpty(EmilsAPIpicture))
                    {
                        updatedProfilePic = EmilsAPIpicture; // Välj den nya profilbilden från Emils API
                    }
                    else
                    {
                        updatedProfilePic = DefaultPic; // Använd den hårdkodade bilden, då ingen ny profilbild hittades i API anropet 
                    }


                    // SPARAR DEN UPPDATERADE PROFILBILDEN
                    UpdatedProfilePic.Add(new
                    {
                        id = member.id,
                        name = member.name,
                        role = member.role,
                        profilePic = updatedProfilePic
                    });
                }

                return Ok(UpdatedProfilePic);
            }
            catch
            {
                return BadRequest("Database currently missing. Unable to fetch members.");
            }
        }








        // DELETE
        // RADERA MEDLEM I GRUPPEN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
            if (member == null) return NotFound();

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return Ok();

            }
            catch
            {
                return BadRequest("Database currently missing. Unable to delete member.");
            }
        }
    }
}