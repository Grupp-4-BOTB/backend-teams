using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackendTeams.Domain.Entities;

public class MemberEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string GroupId { get; set; } = string.Empty; 

    [Required]
    public string UserId { get; set; } = string.Empty; // Den gemensamma nyckeln (id på användarkontot)

}
