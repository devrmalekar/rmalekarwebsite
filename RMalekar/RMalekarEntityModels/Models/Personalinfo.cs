using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace RMalekarEntityModels;

public partial class Personalinfo
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [IgnoreDataMember]
    [JsonIgnore]
    public string ContactRawJson { get; set; } = null!;

    public string Summary { get; set; } = null!;
    [IgnoreDataMember]
    [JsonIgnore]
    public string? SocialLinksRawJson { get; set; }

    [NotMapped]
    public JsonObject? Contact => JsonSerializer.Deserialize<JsonObject>(ContactRawJson);
    [NotMapped]
    public JsonObject? SocialLinks => JsonNode.Parse(SocialLinksRawJson) as JsonObject;

    public int ExperienceYears { get; set; }
}
