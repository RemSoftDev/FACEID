using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACEID
{
    class Response
    {
        public string model_id { get; set; }
        public string name { get; set; }
        public string created { get; set; }
        public string modality { get; set; }
        public string nb_faces { get; set; }

        public List<faces> faces { get; set; }

    }

    public class faces
    {
        public string face_id { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string w { get; set; }
        public string h { get; set; }
        public string image_url { get; set; }
        public string checksum { get; set; }
    }


    public class GetIdentityId
    {
        public string MyProperty { get; set; }

        [JsonProperty("account_status")]
        public IList<string> status { get; set; }

        [JsonProperty("first_name")]
        public IList<string> first_name { get; set; }

        [JsonProperty("last_name")]
        public IList<string> last_name { get; set; }

        [JsonProperty("dob")]
        public IList<string> dob { get; set; }

        [JsonProperty("member_id")]
        public IList<string> identity_id { get; set; }

        public string message { get; set; }
    }


    public class IdResponse
    {
        public GetIdentityId response { get; set; }
    }


    public class faceIDMemberData
    {
        public string business { get; set; }
        public Member member { get; set; }
    }

    public class Member
    {
        // Common values
        public string user_id { get; set; }
        public string user_level { get; set; }

        // Business = 1
        public string parent_account_id { get; set; }
        public string start_date { get; set; }
        public string expiry_date { get; set; }

        // Business = 2
        public string user_address1 { get; set; }
        public string user_address2 { get; set; }
        public string user_town { get; set; }
        public string user_county { get; set; }
        public string user_postcode { get; set; }
        public string user_data { get; set; }
    }

    public class GetUid
    {
        public string uid { get; set; }
    }


    public class Authetication
    {
        public string authenticated { get; set; }
        public string closed { get; set; }
    }


    public class ModelCreatedRespose
    {
        public string identity_id { get; set; }
        public string name { get; set; }
        public string nb_models { get; set; }
        public List<Models> models { get; set; }
    }

    public class Models
    {
        public string model_id { get; set; }
        public string name { get; set; }
        public string created { get; set; }
        public string modality { get; set; }
        public string nb_faces { get; set; }
        public List<faces> faces { get; set; }
    }

    //public class faces
    //{
    //public string face_id  { get; set; }
    //public string x { get; set; }
    //public string y { get; set; }
    //public string w { get; set; }
    //public string h { get; set; }
    //public string image_url { get; set; }
    //public string  checksum { get; set; }
    //}
}
