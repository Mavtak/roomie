
namespace Roomie.Web.Website.ViewModels
{
    public class RoomieBot
    {
        public static string HappyMood = "happy";
        public static string UneasyMood = "uneasy";
        public static string StunnedMood = "";

        public string Mood { get; set; }

        public RoomieBot()
        {
            Mood = HappyMood;
        }
    }
}