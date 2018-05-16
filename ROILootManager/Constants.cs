
namespace ROILootManager
{
  public class Constants
  {
    public const string PROGRAM_VERSION = "0.241";

    public static string CLIENT_ID = "454647371816-r8gv4nnmfqbe8ujk3t0mc9g04uk5voh6.apps.googleusercontent.com";

    // This is the OAuth 2.0 Client Secret retrieved
    // above.  Be sure to store this value securely.  Leaking this
    // value would enable others to act on behalf of your application!
    public static string CLIENT_SECRET = "XXX";

    // OAUTH endpoint
    public static string AUTH_ENDPOINT = "https://accounts.google.com/o/oauth2/v2/auth";
    public static string TOKEN_REQUEST_URI = "https://www.googleapis.com/oauth2/v4/token";

    // Application name
    public static string APPLICATION_NAME = "ROI LOOT";

    // Scope
    public static string SCOPE = "openid profile https://www.googleapis.com/auth/spreadsheets";

    // document IDs
    public static string ITEMS_ID = "13_qG0syQGgK7-yT06r5MJ3HNrWmVh0872ou-FhXjjDM";
    public static string LOOT_ID = "1fGMG78HVN8iLO43zoi8Ermt_nhfkqBHcQkdQLQ7BqnI";
    public static string ROSTER_ID = "1J3Io-COBeCaAQ_jTiJS9kmdP8gqpFNr2_5-gfY_c5cg";

    public const string RAID_ATTENDANCE_URL = "http://forum.roiguild.org/dkp/viewmembers.php";
  }
}
