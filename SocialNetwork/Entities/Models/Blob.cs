namespace SocialNetwork.Entities.Models
{
    public class Blob : ParentModel
    {
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
