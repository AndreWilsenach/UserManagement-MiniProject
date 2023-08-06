using System.Text.RegularExpressions;

namespace MiniProject_UserManagement.Models
{

    //Please note should this be a major project much more detail wil be captured. to ensure we have enough data to get the specific person.
    //DOB
    //IDnumber
    //Surname
    // an adtional table can be create for communication and we can link miltiple communication types to the user
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string? ContactDetail {get;set;}

        // Foreign key for Group
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
