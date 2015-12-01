using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL.InternalTypes
{

    public class User
    {
        public string membershipId { get; set; }
        public string uniqueName { get; set; }
        public string displayName { get; set; }
        public int profilePicture { get; set; }
        public int profileTheme { get; set; }
        public int userTitle { get; set; }
        public string successMessageFlags { get; set; }
        public bool isDeleted { get; set; }
        public string about { get; set; }
        public string firstAccess { get; set; }
        public string lastUpdate { get; set; }
        public string xboxDisplayName { get; set; }
        public bool showActivity { get; set; }
        public int followerCount { get; set; }
        public int followingUserCount { get; set; }
        public string locale { get; set; }
        public bool localeInheritDefault { get; set; }
        public bool showGroupMessaging { get; set; }
        public string profilePicturePath { get; set; }
        public string profileThemeName { get; set; }
        public string userTitleDisplay { get; set; }
        public string statusText { get; set; }
        public string statusDate { get; set; }
        public string psnDisplayName { get; set; }
    }

    public class Result
    {
        public User user { get; set; }
        public bool hasPendingApplication { get; set; }
        public bool hasRated { get; set; }
        public string approvalDate { get; set; }
        public string approvedByMembershipId { get; set; }
        public int rating { get; set; }
        public string groupId { get; set; }
        public int membershipType { get; set; }
        public string membershipId { get; set; }
        public bool isMember { get; set; }
        public int memberType { get; set; }
        public bool isOriginalFounder { get; set; }
    }

    public class Query
    {
        public string groupId { get; set; }
        public int memberType { get; set; }
        public int platformType { get; set; }
        public int sort { get; set; }
        public string nameSearch { get; set; }
        public int itemsPerPage { get; set; }
        public int currentPage { get; set; }
    }

    public partial class Response
    {
        public List<Result> results { get; set; }
        public string totalResults { get; set; }
        public bool hasMore { get; set; }
        public Query query { get; set; }
        public bool useTotalResults { get; set; }
    }

   

    public class ClanPlayersResults_RootObject
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public MessageData MessageData { get; set; }
    }

}
