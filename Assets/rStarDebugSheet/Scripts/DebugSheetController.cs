#region

using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class DebugSheetController : MonoBehaviour
    {
    #region Unity events

        private void Start()
        {
            // Get or create the root page.
            var rootPage = DebugSheet.Instance.GetOrCreateInitialPage();

            // Add a link transition to the ExampleDebugPage.
            rootPage.AddPageLinkButton<MyDebugPage>(nameof(MyDebugPage));
        }

    #endregion
    }
}