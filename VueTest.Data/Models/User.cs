using System.Collections.Generic;
using System.Runtime.Serialization;
using VueTest.Data.Enumerations;

namespace VueTest.Data.Models
{
    [DataContract(Namespace = "")]
    public class User
    {
        [DataMember(Name = "id", IsRequired = false, EmitDefaultValue = true)]
        public int Id { get; set; }
        [DataMember(Name = "firstname", IsRequired = false, EmitDefaultValue = true)]
        public string Firstname { get; set; }
        [DataMember(Name = "lastname", IsRequired = false, EmitDefaultValue = true)]
        public string Lastname { get; set; }
        [DataMember(Name = "email", IsRequired = false, EmitDefaultValue = true)]
        public string Email { get; set; }
        [DataMember(Name = "gender", IsRequired = false, EmitDefaultValue = true)]
        public Genders Gender { get; set; }
        [DataMember(Name = "age", IsRequired = false, EmitDefaultValue = true)]
        public int Age { get; set; }
    }

    public struct UserGetOptions
    {
        public int? Id { get; set; }
        public IReadOnlyList<int> Ids { get; set; }
        public string Search { get; set; }
        public string NormalizedSearch => !string.IsNullOrEmpty(Search) ? $"%{Search}%" : string.Empty;
    }
}