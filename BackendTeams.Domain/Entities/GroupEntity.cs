using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackendTeams.Domain.Entities;

public class GroupEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; } = string.Empty;


    // RELATION: En grupp kan ha många medlemmar och inbjudningar, inte bara en
    public List<MemberEntity> Members { get; set; } = new();

}
