using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.InternalTypes
{
    internal class PlayerSearchResult
    {
        public string Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public object MessageData { get; set; }
    }
}

/*
{"Response":"4611686018430931367","ErrorCode":1,"ThrottleSeconds":0,"ErrorStatus":"Success","Message":"Ok","MessageData":{}}


    */
