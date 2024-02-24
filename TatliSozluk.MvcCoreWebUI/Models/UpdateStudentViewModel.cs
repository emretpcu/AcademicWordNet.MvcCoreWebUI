using EntityLayer.Concreate;

namespace HakinSozluk.MvcCoreWebUI.Models
{
	public class UpdateStudentViewModel
	{
        public User User { get; set; }	
		public StudentInformation StudentInformation { get; set; }	
		public bool isAccountActive { get; set; }	
	}
}
