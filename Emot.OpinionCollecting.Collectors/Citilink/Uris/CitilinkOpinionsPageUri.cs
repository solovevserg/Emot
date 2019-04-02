using Emot.OpinionCollecting.Interfaces;

namespace Emot.OpinionCollecting.Collectors.Citilink.Uris
{
    public class CitilinkOpinionsPageUri : IUri
    {
        private readonly string _category;
        private readonly string _id;

        public string Get() => $"https://www.citilink.ru/catalog/{_category}/{_id}/otzyvy/";

        public CitilinkOpinionsPageUri(string category, string id)
        {
            _category = category;
            _id = id;
        }
    }
}
