using Emot.OpinionCollecting.Interfaces;

namespace Emot.OpinionCollecting.Collectors.Citilink.Uris
{
    public class CitilinkOpinionsPageUri : IUri
    {
        public string Get() => "https://www.citilink.ru/catalog/mobile/cell_phones/1090252/otzyvy/";

        public CitilinkOpinionsPageUri(string category, string id)
        {

        }
    }
}
