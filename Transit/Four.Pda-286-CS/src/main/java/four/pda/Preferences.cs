using Org.Androidannotations.Annotations.Sharedpreferences;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by asavinova on 13/04/15.
    /// </summary>
    public interface Preferences
    {
        bool IsFirstRun();
        long ProfileId();
        string ProfileLogin();
        string ProfilePhoto();
        int TextZoom();
        bool IsAcceptedCommentRules();
    }
}