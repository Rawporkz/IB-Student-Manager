namespace IB_Student_Manager.Models
{
	public class Email
	{
		public string SenderName { get; set; }
		public string SenderEmail { get; set; } 
		public string SenderPassword { get; set; } 
		public string RecipientName { get; set; }
		public string RecipientEmail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

	}
}
