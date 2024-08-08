using Domain.Common;
using Sieve.Attributes;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        [Sieve(CanSort =true)]
        public int ID { get; set; }
        [Sieve(CanSort = true, CanFilter =true)]
        public string FirstMidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? EnrollmentDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
