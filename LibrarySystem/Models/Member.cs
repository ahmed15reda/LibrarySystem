namespace LibrarySystem.Models
{
    public enum MembershipType
    {
        Gold = 1,
        Silver = 2,
        Bronze = 3
    }

    public class Member : User
    {
        public Member(string? id, string? name, MembershipType membershipType) : base(id, name)
        {
            MembershipType = membershipType;
        }

        public MembershipType MembershipType { get; set; }

    }
}
