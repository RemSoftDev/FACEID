using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACEID
{


    class LoginResponse
    {
        public int status { get; set; }

        public DATA data { get; set; }

        [JsonProperty("has_faceid")]
        public int has_faceid { get; set; }

        [JsonProperty("is_model_created")]
        public int is_model_created { get; set; }

        [JsonProperty("identityID")]
        public string identityID { get; set; }

        [JsonProperty("member_id")]
        public string member_id { get; set; }

        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("dob")]
        public string dob { get; set; }

        [JsonProperty("user_type")]
        public string user_type { get; set; }

        [JsonProperty("type_id")]
        public string type_id { get; set; }

    }

    public class DATA
    {
        public int ID { get; set; }

        public string user_login { get; set; }

        public string user_pass { get; set; }

        public string user_nicename { get; set; }

        public string user_email { get; set; }

        public string user_url { get; set; }

        public string user_registered { get; set; }

        public string user_activation_key { get; set; }

        public string user_status { get; set; }

        public string display_name { get; set; }

    }

}
